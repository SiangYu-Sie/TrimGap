using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ch.etel.edi.dmd.v40;
using ch.etel.edi.dsa.v40;

namespace Modules
{
    public abstract class MotionController : IDaqDevice, IDisposable
    {
        /// <summary>
        /// 當運動軸覆歸完成時的事件。
        /// </summary>
        public event EventHandler<int> HomeCompleted;
        
        /// <summary>
        /// 取得運動控制器初始化狀態。
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// 取得運動軸集合。
        /// </summary>
        public abstract IReadOnlyList<Axis> Axes { get; }

        /// <summary>
        /// 取得控制卡內所有的數位輸入通道。
        /// </summary>
        public virtual IReadOnlyList<IDigitalOutput> DoChannels { get; }

        /// <summary>
        /// 取得控制卡內所有的數位輸出通道。
        /// </summary>
        public virtual IReadOnlyList<IDigitalInput> DiChannels { get; }

        /// <summary>
        /// 釋放控制卡的資源。
        /// </summary>
        public void Dispose()
        {
            if (!IsInitialized) return;

            Task.Run(StopAsync).Wait();

            Close();

            IsInitialized = false;

        }

        /// <summary>
        /// 初始化運動控制卡。
        /// </summary>
        public void Initialize()
        {
            if (IsInitialized) return;

            try
            {

                Open();

                IsInitialized = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 停止控制卡內正在運動的軸。
        /// </summary>
        /// <returns></returns>
        public async Task StopAsync()
            => await Task.WhenAll(
                Axes
                .Where(ax => ax.IsOpen)
                .Select(axis => axis.StopAsync()));

        /// <summary>
        /// 暫停控制卡內正在運動的軸。
        /// </summary>
        /// <returns></returns>
        public async Task PauseAsync()
            => await Task.WhenAll(
                Axes
                .Where(ax => ax.IsOpen)
                .Select(ax => ax.PauseAsync()));

        /// <summary>
        /// 恢復已暫停的運動軸。
        /// </summary>
        public void Resume()
            => Parallel.ForEach(
                Axes.Where(ax => ax.IsOpen),
                ax => ax.Resume());

        #region Initialize 

        /// <summary>
        /// 初始化運動控制卡。
        /// </summary>
        protected abstract void Open();

        /// <summary>
        /// 關閉運動控制卡。
        /// </summary>
        protected abstract void Close();

        /// <summary>
        /// 初始化指定的運動軸。
        /// </summary>
        /// <param name="axisId"></param>
        public abstract void AxisOpen(int axisId);

        /// <summary>
        /// 關閉指定的運動軸。
        /// </summary>
        /// <param name="axisId"></param>
        public abstract void AxisClose(int axisId);
        #endregion

        #region Event Handlers

        /// <summary>
        /// 當運動軸完成覆歸時產生的事件。
        /// </summary>
        /// <param name="axisId"></param>
        internal protected virtual void OnHomeCompleted(int axisId) => HomeCompleted?.Invoke(this, axisId);

        #endregion

        #region Status / Position / Velocity

        /// <summary>
        /// 取得運動軸的運動狀態。
        /// </summary>
        /// <param name="axisId"></param>
        /// <returns></returns>
        public abstract AxisStatus GetStatus(int axisId);

        /// <summary>
        /// 取得運動軸的感測器狀態。
        /// </summary>
        /// <param name="axisId"></param>
        /// <returns></returns>
        public abstract Sensors GetSensorStatus(int axisId);

        /// <summary>
        /// 取得運動軸是否開啟。
        /// </summary>
        /// <param name="axisId"></param>
        /// <returns></returns>
        public abstract bool IsAxisOpen(int axisId);


        public abstract List<DsaDrive> GetDsaDrives();

        /// <summary>
        /// 取得運動軸當前的指令位置。
        /// </summary>
        /// <param name="axisId"></param>
        /// <returns></returns>
        public abstract double GetCommand(int axisId);

        /// <summary>
        /// 設定運動軸指令位置的值。
        /// </summary>
        /// <param name="axisId"></param>
        /// <param name="command"></param>
        public abstract void SetCommand(int axisId, double command);

        /// <summary>
        /// 取得運動軸當前的回饋位置。
        /// </summary>
        /// <param name="axisId"></param>
        /// <returns></returns>
        public abstract double GetFeedback(int axisId);

        /// <summary>
        /// 設定運動軸回饋位置的值。
        /// </summary>
        /// <param name="axisId"></param>
        /// <param name="feedback"></param>
        public abstract void SetFeedback(int axisId, double feedback);
        #endregion

        #region Moving Commands        

        /// <summary>
        /// 等待指定的運動軸停止。
        /// </summary>
        /// <param name="axisId"></param>
        /// <returns>正常停止回傳 true；異常停止則回傳 false。</returns>
        public virtual void Wait(int axisId)
        {
            AxisStatus status = AxisStatus.Moving;
            SpinWait.SpinUntil(() =>
            {
                if (Axes[axisId].PollingInterval > 0) Thread.Sleep(Axes[axisId].PollingInterval);
                return (status = GetStatus(axisId)) != AxisStatus.Moving;
            });
        }

        /// <summary>
        /// 運動軸覆歸運動。
        /// </summary>
        /// <param name="axisId"></param>
        /// <param name="mode"></param>
        /// <param name="direction"></param>
        /// <param name="vel"></param>
        public abstract void HomeCommand(int axisId, HomeModes mode, MotionDirections direction, VelocityParams vel);

        /// <summary>
        /// 運動軸往指定的方向連續移動。
        /// </summary>
        /// <param name="axisId"></param>
        /// <param name="direction"></param>
        /// <param name="vel"></param>
        public abstract void MoveCommand(int axisId, MotionDirections direction, VelocityParams vel);

        /// <summary>
        /// 運動軸移動指定的距離(相對距離移動)。
        /// </summary>
        /// <param name="axisId"></param>
        /// <param name="distance"></param>
        /// <param name="vel"></param>
        public abstract void MoveCommand(int axisId, double distance, VelocityParams vel);

        /// <summary>
        /// 運動軸移動至指定的位置(絕對位置移動)。
        /// </summary>
        /// <param name="axisId"></param>
        /// <param name="position"></param>
        /// <param name="vel"></param>
        public abstract void MoveToCommand(int axisId, double position, VelocityParams vel);

        /// <summary>
        /// 停止運動軸運動(下達停止指令後運動軸開始執行停止動作，並非完全停止)。
        /// </summary>
        /// <param name="axisId"></param>
        public abstract void StopCommand(int axisId);

        /// <summary>
        /// 
        /// </summary>
        public abstract List<DsaDrive> listDsaDrive();

        public virtual void LinearMove(int[] axes, double[] distances, VelocityParams vel)
        {
            throw new NotImplementedException();
        }

        public virtual void LinearMoveTo(int[] axes, double[] positions, VelocityParams vel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 運動軸在指定位置觸發CMP訊號。
        /// </summary>
        /// <param name="axisId"></param>
        /// <param name="position"></param>
        public abstract void CMPCommand(int axisId, double position,double distance);

        /// <summary>
        /// 異常排除。
        /// </summary>
        /// <param name="axisId"></param>
        public abstract void ResetError(int axisId);
        #endregion
        #region Digital I/O

        /// <summary>
        /// 讀取數位輸入通道的狀態。
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public abstract bool ReadDigitalInput(int groupId, int channel);

        /// <summary>
        /// 讀取數位輸出通道的狀態。
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public abstract bool ReadDigitalOutput(int groupId, int channel);

        /// <summary>
        /// 寫入數位輸出通道的狀態。
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="channel"></param>
        /// <param name="state"></param>
        public abstract void WriteDigitalOutput(int groupId, int channel, bool state);

        #endregion
    }
}
