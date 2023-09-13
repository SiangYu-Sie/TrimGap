using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Modules
{
    public partial class AxisSetting : UserControl, INotifyPropertyChanged
    {
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;

        private static readonly DependencyProperty AxisProperty = DependencyProperty.Register("Axis", typeof(Axis), typeof(AxisSetting), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnAxiSet)));
        private static readonly DependencyProperty AxisConfigProperty = DependencyProperty.Register("AxisConfig", typeof(AxisConfig), typeof(AxisSetting), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnAxiSet)));
        private static readonly DependencyProperty LockAxisProperty = DependencyProperty.Register("LockAxis", typeof(ICommand), typeof(AxisSetting), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        private static readonly DependencyProperty HomeActionProperty = DependencyProperty.Register("HomeAction", typeof(Action<(Axis, AxisConfig)>), typeof(AxisSetting), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        private static readonly DependencyProperty IsReverseProperty = DependencyProperty.Register("IsReverse", typeof(bool), typeof(AxisSetting), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public AxisSetting()
        {
            InitializeComponent();

            Control.DataContext = this;
        }

        /// <summary>
        /// Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(RefreshAxiPosition);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);
            dispatcherTimer.Start();
        }

        /// <summary>
        /// UnLoaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (dispatcherTimer != null)
            {
                dispatcherTimer.Stop();
                dispatcherTimer.Tick -= new EventHandler(RefreshAxiPosition);
            }
        }

        /// <summary>
        /// 軸
        /// </summary>
        public Axis Axis { get => (Axis)GetValue(AxisProperty); set => SetValue(AxisProperty, value); }

        /// <summary>
        /// 軸 資料
        /// </summary>
        public AxisConfig AxisConfig { get => (AxisConfig)GetValue(AxisConfigProperty); set => SetValue(AxisConfigProperty, value); }

        /// <summary>
        /// 軟體極限 鎖
        /// </summary>
        public ICommand LockAxis { get => (ICommand)GetValue(LockAxisProperty); set => SetValue(LockAxisProperty, value); }

        /// <summary>
        /// 回原點事件
        /// </summary>
        public Action<(Axis, AxisConfig)> HomeAction { get => (Action<(Axis, AxisConfig)>)GetValue(HomeActionProperty); set => SetValue(HomeActionProperty, value); }

        /// <summary>
        /// 軸方向
        /// </summary>
        public bool IsReverse { get => (bool)GetValue(IsReverseProperty); set => SetValue(IsReverseProperty, value); }

        /// <summary>
        /// 軸名稱
        /// </summary>
        public string AxisName { get; set; }

        /// <summary>
        /// 軸 正極限
        /// </summary>
        public double? AxisPEL { get; set; }

        /// <summary>
        /// 軸 負極限
        /// </summary>
        public double? AxisNEL { get; set; }

        /// <summary>
        /// 軸位置
        /// </summary>
        public double AxisPosition { get; set; }

        /// <summary>
        /// 軸速度
        /// </summary>
        public double AxisVelocity { get; set; }

        /// <summary>
        /// 搖桿速度
        /// </summary>
        public double JoyStickVelocity { get; set; }

        /// <summary>
        /// 吋動 值
        /// </summary>
        public double? InchingDistant { get; set; }

        /// <summary>
        /// 加速度時間
        /// </summary>
        public double AccTime { get; set; }

        /// <summary>
        /// 減速度時間
        /// </summary>
        public double DecTime { get; set; }

        public bool IsAxisOn { get; set; }
        public bool IsELReleaseOn { get; set; }

        public Brush AxisSensorBackground { get; set; } = Brushes.Gray;

        public void OnAxisVelocityChanged() => AxisConfig.CommonWorkingVelocity = AxisVelocity;

        /// <summary>
        /// 軸搖桿 往後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackwardMove(object sender, MouseButtonEventArgs e)
        {
            Axis.MotionVelParams = new VelocityParams(0, JoyStickVelocity, AccTime, DecTime);
            if (!InchingDistant.HasValue)
            {
                if (!IsReverse) Axis.MoveAsync(MotionDirections.Backward);
                else Axis.MoveAsync(MotionDirections.Forward);
            }
            else
            {
                if (!IsReverse) Axis.MoveAsync(-InchingDistant.Value);
                else Axis.MoveAsync(InchingDistant.Value);
            }
        }

        /// <summary>
        /// 軸搖桿 往前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForWardMove(object sender, MouseButtonEventArgs e)
        {
            Axis.MotionVelParams = new VelocityParams(0, JoyStickVelocity, AccTime, DecTime);
            if (!InchingDistant.HasValue)
            {
                if (!IsReverse) Axis.MoveAsync(MotionDirections.Forward);
                else Axis.MoveAsync(MotionDirections.Backward);
            }
            else
            {
                if (!IsReverse) Axis.MoveAsync(InchingDistant.Value);
                else Axis.MoveAsync(-InchingDistant.Value);
            }
        }

        /// <summary>
        /// 軸 停止運行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AxiStop(object sender, MouseButtonEventArgs e)
        {
            if (!InchingDistant.HasValue) Axis.Stop();
        }

        /// <summary>
        /// 軸更新
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnAxiSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as AxisSetting;
            if (control.Axis != null)
            {
                control.EnalbleAxiTB.IsChecked = control.Axis.IsOpen;
                control.AxisName = control.Axis.Name;
            }

            if (control.Axis != null && control.AxisConfig != null)
            {
                control.Axis.PositionPEL = control.AxisConfig.AxisPEL;
                control.Axis.PositionNEL = control.AxisConfig.AxisNEL;
                control.AxisPEL = control.AxisConfig.AxisPEL;
                control.AxisNEL = control.AxisConfig.AxisNEL;
                if (control.AxisConfig.CommonWorkingVelocity != null)
                {
                    control.JoyStickVelocity = control.AxisConfig.CommonWorkingVelocity.FinalVel;
                    control.AccTime = control.AxisConfig.CommonWorkingVelocity.AccelerationTime;
                    control.DecTime = control.AxisConfig.CommonWorkingVelocity.DecelerationTime;
                }
                else
                {
                    control.JoyStickVelocity = 1000;
                    control.AccTime = 0.2;
                    control.DecTime = 0.2;
                }
            }
        }

        /// <summary>
        /// 刷軸狀態
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshAxiPosition(object sender, EventArgs e)
        {
            if (Axis != null)
            {
                AxisPosition = Axis.Position;

                switch (Axis.Sensor)
                {
                    case Sensors.None:
                    case Sensors.RDY:
                        AxisSensorBackground = Brushes.Gray;
                        break;
                    case Sensors.ALM:
                    case Sensors.EMG:
                        AxisSensorBackground = Brushes.Red;
                        break;
                    case Sensors.ORG:
                        AxisSensorBackground = Brushes.DarkGreen;
                        break;
                    case Sensors.NEL:
                    case Sensors.PEL:
                        AxisSensorBackground = Brushes.Red;
                        break;
                }
            }
        }

        /// <summary>
        /// 設定軟體負極限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetNELCommand(object sender, RoutedEventArgs e)
        {
            AxisNEL = Axis.Position;
        }

        /// <summary>
        /// 設定 軟體正極限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetPELPosition(object sender, RoutedEventArgs e)
        {
            AxisPEL = Axis.Position;
        }

        /// <summary>
        /// 軟極限 鎖 按鈕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LockLimitCommand(object sender, RoutedEventArgs e)
        {
            if (Axis != null)
            {
                if (!Tbtn.IsChecked.Value)
                {
                    Axis.PositionPEL = AxisPEL;
                    Axis.PositionNEL = AxisNEL;
                    AxisConfig.AxisPEL = AxisPEL;
                    AxisConfig.AxisNEL = AxisNEL;
                }

                if (Tbtn.IsChecked.Value)
                {
                    Axis.PositionPEL = null;
                    Axis.PositionNEL = null;
                }
            }
        }

        /// <summary>
        /// Axis Open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnalbleAxiCommad(object sender, RoutedEventArgs e)
        {
            if (Axis != null)
            {
                if (!EnalbleAxiTB.IsChecked.Value)
                {
                    Axis.Close();
                }
                else if (!Axis.IsOpen) Axis.Open();
            }
            else
            {
                EnalbleAxiTB.IsChecked = false;
            }
        }

        /// <summary>
        /// 軟極限 鎖
        /// </summary>
        private void Lock()
        {
            Tbtn.IsChecked = false;
            if (Axis != null)
            {
                Axis.PositionPEL = AxisPEL;
                Axis.PositionNEL = AxisNEL;
                AxisConfig.CommonWorkingVelocity = new VelocityParams(0, JoyStickVelocity, AccTime, DecTime);
                Axis.MotionVelParams = AxisConfig.CommonWorkingVelocity;
            }
        }

        /// <summary>
        /// Load 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            LockAxis = new RelayCommand(Lock, null);
            if (HomeAction == null) HomeButton.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 回原點事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeCommand(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dohoming = MessageBox.Show($"確定要將{AxisName}回原點嗎?", "確認是否回原點", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dohoming == MessageBoxResult.Yes)
            {
                HomeAction?.Invoke((Axis, AxisConfig));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            T oldValue = field;
            field = value;
            OnPropertyChanged(propertyName, oldValue, value);
        }
        protected virtual void OnPropertyChanged<T>(string name, T oldValue, T newValue)
        {
            // oldValue 和 newValue 目前沒有用到，代爾後需要再實作。
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
