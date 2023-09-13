using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules
{
    public partial class Axis
    {
        internal readonly AsyncLock asyncLock = new AsyncLock();
        internal AxisMotion motion = AxisMotion.CompletedMotion;

        private readonly Subject<MotionTypes> motionTypes = new Subject<MotionTypes>();
        private int pollingInterval = 0;
        private double unitFactor = 1;

        public Axis(MotionController controller, int id)
        {
            Controller = controller;
            Id = id;
        }

        #region Axis Information

        public string Name { get; set; }

        public MotionController Controller { get; }

        public int Id { get; }

        /// <summary>
        /// 取得或設定運動軸輪詢狀態的時間間隔，可設定範圍為 0 ~ 500ms(毫秒)。
        /// </summary>
        public int PollingInterval
        {
            get => pollingInterval;
            set
            {
                if (value < 0) value = 0;
                if (value > 500) value = 500;
                pollingInterval = value;
            }
        }

        #endregion


        #region States

        /// <summary>
        /// 取得運動軸是否已開啟。
        /// </summary>
        public bool IsOpen { get => Controller.IsAxisOpen(Id); }

        /// <summary>
        /// 取得運動軸是否覆歸完成。
        /// </summary>
        public bool IsHomeDone { get; private set; }

        /// <summary>
        /// 取得軸是否處於運動狀態。
        /// </summary>
        public bool IsRunning => MotionType != MotionTypes.None;

        public bool IsPausing => motion.IsPaused;

        /// <summary>
        /// 取得運動軸狀態。
        /// </summary>
        public Sensors Sensor => Controller.GetSensorStatus(Id);

        /// <summary>
        /// 取得運動軸的原始運動狀態。
        /// </summary>
        public AxisStatus Status => Controller.GetStatus(Id);

        /// <summary>
        /// 取得運動軸的運狀類型。
        /// </summary>
        public MotionTypes MotionType { get; private set; }

        #endregion

        #region Position

        /// <summary>
        /// 取得或設定運動軸位置，位置來源使用 PositionSource 定義為 Command 或 Feedback。
        /// </summary>
        public double Position
        {
            get => PositionSource == PositionSources.Command ? Command : Feedback;
            set
            {
                if (PositionSource == PositionSources.Command)
                    Command = value;
                else
                    Feedback = value;
            }
        }

        /// <summary>
        /// 取得或設定 Position 的來源為 Command 或 Feedback。
        /// </summary>
        public PositionSources PositionSource { get; set; } = PositionSources.Feedback;

        /// <summary>
        /// 取得或設定指令位置。
        /// </summary>
        public virtual double Command
        {
            get
            {
                return (double)((decimal)Controller.GetCommand(Id) * (decimal)unitFactor);
            }
            set
            {
                Controller.SetCommand(Id, (double)((decimal)value / (decimal)unitFactor));
            }
        }

        /// <summary>
        /// 取得或設定回饋位置。
        /// </summary>
        public virtual double Feedback
        {
            get => (double)((decimal)Controller.GetFeedback(Id) * (decimal)unitFactor);
            set => Controller.SetFeedback(Id, (double)((decimal)value / (decimal)unitFactor));
        }

        #endregion

        #region Velocity

        /// <summary>
        /// 取得或設定覆歸移動的速度設定。
        /// </summary>
        public VelocityParams HomeVelParams { get; set; }

        /// <summary>
        /// 取得或設定移動的速度設定。
        /// </summary>
        public VelocityParams MotionVelParams { get; set; }

        #endregion

        #region Configurations

        /// <summary>
        /// 取得或設定覆歸的模式。
        /// </summary>
        public HomeModes HomeMode { get; set; } = HomeModes.ORG;

        /// <summary>
        /// 取得或設定軟體負極限位置，設定後影響連續移動轉換為絕對位置移動至此設定值。
        /// </summary>
        public double? PositionNEL { get; set; }

        /// <summary>
        /// 取得或設定軟體極正限位置，設定後影響連續移動轉換為絕對位置移動至此設定值。
        /// </summary>
        public double? PositionPEL { get; set; }

        /// <summary>
        /// 取得或設定位置單位的因子數，軸的 1 位置單位 = 0.5 μm
        /// </summary>
        public double UnitFactor
        {
            get => unitFactor;
            set
            {
                if (!motion.IsCompleted)
                    throw new InvalidOperationException($"{this} is in motion, can not set command factor.");
                if (value <= 0) throw new InvalidOperationException("Command factor Cannot be less than or equal to 0.");

                // 因為型別精度問題 Unit factor 必須可以整除 1，否則會造成對軸卡下了 command 後 feedback 卻回出不可的值。
                if (!1.0d.IsDivisibleBy(value)) throw new InvalidOperationException("1 must be divisable by unit factor.");

                unitFactor = value;
            }
        }

        #endregion

        public virtual void Open()
        {
            try
            {
                Controller.AxisOpen(Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"{Name} open failed.", ex);
            }
        }

        public virtual void Close()
        {
            try
            {
                Controller.AxisClose(Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"{Name} close failed.", ex);
            }
        }

        #region Move Methods

        public virtual async Task HomeAsync(MotionDirections dir = MotionDirections.Backward)
        {
            if (HomeVelParams is null) throw new ArgumentNullException(nameof(HomeVelParams));
            ThrowIfAxisIsNotOpen();

            using (MotionSection(MotionTypes.Home))
            {
                ThrowIfAxisIsRunningOrPaused();
                using (await asyncLock.LockAsync())
                {
                    motion = new AxisMotion(() => Home(HomeMode, dir), Stop, Wait);
                    motion.Run();
                }
                await motion.ConfigureAwait(false);
            }

            Controller.OnHomeCompleted(Id);
            IsHomeDone = true;
        }

        public Task MoveAsync(double distance)
            => MotionVelParams is null ? throw new ArgumentNullException(nameof(MotionVelParams)) : MoveAsync(distance, MotionVelParams);

        public async Task MoveAsync(double distance, VelocityParams velParams)
        {
            if (double.IsNaN(distance)) throw new ArgumentException("distance is NaN", nameof(distance));
            if (velParams is null) throw new ArgumentNullException(nameof(MotionVelParams));
            ThrowIfAxisIsNotOpen();

            if (distance == 0) return;
            ThrowIfPositionOutOfRange(Position + distance);

            using (MotionSection(MotionTypes.Relative))
            {
                using (await asyncLock.LockAsync())
                {
                    ThrowIfAxisIsRunningOrPaused();
                    motion = new AxisMotion(() => Move(distance, velParams), Stop, Wait);
                    motion.Run();
                }
                await motion.ConfigureAwait(false);
            }
        }

        public Task MoveAsync(MotionDirections dir)
            => MotionVelParams is null ? throw new ArgumentNullException(nameof(MotionVelParams)) : MoveAsync(dir, MotionVelParams);

        public async Task MoveAsync(MotionDirections dir, VelocityParams velParams)
        {
            if (velParams is null) throw new ArgumentNullException(nameof(velParams));
            ThrowIfAxisIsNotOpen();

            using (MotionSection(MotionTypes.Continuous))
            {
                using (await asyncLock.LockAsync())
                {
                    ThrowIfAxisIsRunningOrPaused();
                    if (dir == MotionDirections.Forward && PositionPEL.HasValue)
                        motion = new AxisMotion(() => MoveTo(PositionPEL.Value, velParams), Stop, Wait);
                    else if (dir == MotionDirections.Backward && PositionNEL.HasValue)
                        motion = new AxisMotion(() => MoveTo(PositionNEL.Value, velParams), Stop, Wait);
                    else
                        motion = new AxisMotion(() => Move(dir, velParams), Stop, Wait);
                    motion.Run();
                }
                await motion.ConfigureAwait(false);
            }
        }

        public Task MoveToAsync(double position)
            => MotionVelParams is null ? throw new ArgumentNullException(nameof(MotionVelParams)) : MoveToAsync(position, MotionVelParams);

        public async Task MoveToAsync(double position, VelocityParams velParams)
        {
            if (double.IsNaN(position)) throw new ArgumentException("position is NaN.", nameof(position));
            if (velParams is null) throw new ArgumentNullException(nameof(MotionVelParams));
            ThrowIfAxisIsNotOpen();

            if (position == Command) return;
            ThrowIfPositionOutOfRange(position);

            using (MotionSection(MotionTypes.Absolute))
            {
                using (await asyncLock.LockAsync())
                {
                    ThrowIfAxisIsRunningOrPaused();
                    motion = new AxisMotion(() => MoveTo(position, velParams), Stop, Wait);
                    motion.Run();
                }
                await motion.ConfigureAwait(false);
            }

            // 若沒有加 ConfigureAwait(false) ，出 using 區間時若呼叫 MoveToAsync 的執行緒被卡住，因為 motion 執行完成想把控制權還給呼叫執行緒，
            // 但此時呼叫執行緒卻因為某些原因被占用，直到占用完畢 await motion 才會執行完，就會造成 motion 其實早就完成了但 MotionType 卻被延遲修改為 None，
            // 以下的範例程式就會造成第二次呼叫 MoveToAsync 跳出 motion 沒有完成的例外：
            // x.MoveToAsync(1000)
            // Thread.Sleep(3000) 此 Sleep 已經足夠上面的移動完成
            // await x.MoveToAsync(2000) 此時即使移動到 1000 已經完成，也會跳出 motion 尚未完成的錯誤訊息
        }

        public async Task StopAsync()
        {
            using (await asyncLock.LockAsync())
            {
                await motion.DisposeAsync();
            }
        }

        public async Task PauseAsync()
        {
            using (await asyncLock.LockAsync())
            {
                await motion.PauseAsync();
            }
        }

        public void Resume()
        {
            using (asyncLock.Lock())
            {
                motion.Run();
            }
        }

        #endregion

        #region API Wrappers

        internal protected virtual void Home(HomeModes mode, MotionDirections direction)
        {
            Controller.HomeCommand(Id, mode, direction, HomeVelParams / unitFactor);
        }

        internal protected virtual void Move(MotionDirections direction, VelocityParams velParams)
        {
            Controller.MoveCommand(Id, direction, velParams / unitFactor);
        }

        internal protected virtual void Move(double distance, VelocityParams velParams)
        {
            Controller.MoveCommand(Id, (double)((decimal)distance / (decimal)UnitFactor), velParams / unitFactor);
        }

        internal protected virtual void MoveTo(double position, VelocityParams velParams)
        {
            Controller.MoveToCommand(Id, (double)((decimal)position / (decimal)UnitFactor), velParams / unitFactor);
        }

        internal protected virtual void Stop() => Controller.StopCommand(Id);

        public virtual void Wait()
        {
            Controller.Wait(Id);
            ThrowIfAlarmOrEmgOccurred();
        }

        #endregion

        private void ThrowIfAxisIsNotOpen()
        {
            if (!IsOpen) throw new InvalidOperationException($"{this} is not open.");
        }

        internal void ThrowIfPositionOutOfRange(double destination)
        {

            if (PositionPEL.HasValue && (destination > PositionPEL.Value))
                throw new Exception($"{Name} Position out of limit {PositionPEL.Value}.");
            if (PositionNEL.HasValue && (destination < PositionNEL.Value))
                throw new Exception($"{Name} Position out of limit {PositionNEL.Value}.");
        }

        internal void ThrowIfAxisIsRunningOrPaused()
        {
            if (!motion.IsCompleted) throw new InvalidOperationException($"{this} can not move,current motion is not completed.");
            if (motion.IsPaused) throw new InvalidOperationException($"{this} was paused.");
        }

        private void ThrowIfAlarmOrEmgOccurred()
        {
            Sensors status = Sensor;
            if (status.HasFlag(Sensors.EMG)) throw new Exception($"{Name} Emergency Stop.");
            if (status.HasFlag(Sensors.ALM)) throw new Exception($"{Name} Alarm Occurred.");
        }

        internal IDisposable MotionSection(MotionTypes type)
        {
            MotionType = type;
            motionTypes.OnNext(type);
            return Disposable.Create(() =>
            {
                MotionType = MotionTypes.None;
                motionTypes.OnNext(MotionTypes.None);
            });
        }


    }

    public static partial class ModuleExtension
    {
        public static void Pause(this Axis axis) => Task.Run(axis.PauseAsync).Wait();

        public static void Stop(this Axis axis) => Task.Run(axis.StopAsync).Wait();

        //public static IDisposable StopWhen(this Axis axis, Func<bool> condition, int pollingTime = 0)
        //{
        //    if (axis.IsRunning) throw new InvalidOperationException($"Axis is running, Can not call this method.");

        //    var subscription = axis.MotionStarting
        //        .Take(1)
        //        .ObserveOn(TaskPoolScheduler.Default)
        //        .Subscribe(ax =>
        //        {
        //            var whileTask = WhileTask.Run(() =>
        //            {
        //                if (condition()) Stop(axis);
        //            },
        //            pollingTime);
        //        });

        //    return subscription;
        //}

        /// <summary>
        /// 將運動軸移動至指定位置，移動期間監測到停止條件成立則立即停止運動。
        /// </summary>        
        /// <param name="position">指定的位置。</param>
        /// <param name="stopCondition">運動期間若停止條件成立則立即停止運動。</param>
        /// <param name="stopDescription">停止條件的訊息描述，運動軸因停止條件成立而停止時的錯誤訊息內將包含此描述。</param>
        /// <returns></returns>
        public static async Task MoveToAsync(this Axis axis, double position, Func<bool> stopCondition, string stopDescription = null)
        {
            using (var whileTask = WhileTask.Run(stopCondition, 1))
            {
                var moveTask = axis.MoveToAsync(position);
                var task = await Task.WhenAny(whileTask, moveTask);
                if (task != moveTask)
                {
                    await axis.StopAsync();
                    throw new InvalidOperationException($"{axis} stopped due to {stopDescription ?? "the stopping condition meet"}.");
                }
            }
        }
    }
    public static class BoolExtain
    {
        public static bool IsDivisibleBy(this double a, double b)
            => (decimal)a == (decimal)(a / b) * (decimal)b;
    }
}
