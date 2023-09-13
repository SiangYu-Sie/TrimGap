using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Modules;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ETELControlTest.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
            Initialize();
            RefreshAxisPosTimer.Tick += new EventHandler(RefreshAxiPosition);
            RefreshAxisPosTimer.Interval = TimeSpan.FromMilliseconds(100);
            RefreshAxisPosTimer.Start();
        }
        public Axis X { get; set; }
        public Axis Y { get; set; }
        public Axis R { get; set; }
        public MotionController Etel { get; set; }
        public double? StageDistance { get; set; }
        public double? StageRDistance { get; set; }
        public double XPosition { get; set; }
        public double YPosition { get; set; }
        public double AxisXPosition { get; set; }
        public double AxisYPosition { get; set; }
        public double AxisRPosition { get; set; }
        public DispatcherTimer RefreshAxisPosTimer = new DispatcherTimer();
        public ICommand HomingCommand => new RelayCommand(async () =>
        {
            try
            {
                await HomingAsync();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        public ICommand StageMoveToCommand => new RelayCommand(async () =>
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("確認是否移動到指定位置?", "確認訊息", MessageBoxButton.OKCancel);
                if (result != MessageBoxResult.OK) return;
                await Task.WhenAll(X.MoveToAsync(XPosition), Y.MoveToAsync(YPosition));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });
        public ICommand StageXForwardCommand => new RelayCommand(() =>
        {
            try
            {
                if (!StageDistance.HasValue)
                {
                    X.MoveAsync(MotionDirections.Forward);
                }
                else
                {
                    X.MoveAsync(StageDistance.Value);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        });
        public ICommand StageXBackwardCommand => new RelayCommand(() =>
        {
            try
            {
                if (!StageDistance.HasValue)
                {
                    X.MoveAsync(MotionDirections.Backward);
                }
                else
                {
                    X.MoveAsync(-StageDistance.Value);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        });
        public ICommand StageYForwardCommand => new RelayCommand(() =>
        {
            try
            {
                if (!StageDistance.HasValue)
                {
                    Y.MoveAsync(MotionDirections.Forward);
                }
                else
                {
                    Y.MoveAsync(StageDistance.Value);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        });
        public ICommand StageYBackwardCommand => new RelayCommand(() =>
        {
            try
            {
                if (!StageDistance.HasValue)
                {
                    Y.MoveAsync(MotionDirections.Backward);
                }
                else
                {
                    Y.MoveAsync(-StageDistance.Value);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        });
        public ICommand StageYStopCommand => new RelayCommand(async () =>
        {
            try
            {
                if (!StageDistance.HasValue)
                    await Y.StopAsync();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        });
        public ICommand StageXStopCommand => new RelayCommand(async () =>
        {
            try
            {
                if (!StageDistance.HasValue)
                    await X.StopAsync();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        });

        public ICommand StageRForwardCommand => new RelayCommand(() =>
        {
            try
            {
                if (!StageRDistance.HasValue)
                {
                    R.MoveAsync(MotionDirections.Forward);
                }
                else
                {
                    R.MoveAsync(StageRDistance.Value);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
   

        });
        public ICommand StageRBackwardCommand => new RelayCommand(() =>
        {
            try
            {
                if (!StageRDistance.HasValue)
                {
                    R.MoveAsync(MotionDirections.Backward);
                }
                else
                {
                    R.MoveAsync(-StageRDistance.Value);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        });
        public ICommand StageRStopCommand => new RelayCommand(async () =>
        {
            try
            {
                if (!StageRDistance.HasValue)
                    await R.StopAsync();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        });
        private void Initialize()
        {
            Etel = new ETEL();
            Etel.Initialize();
            R = Etel.Axes[0];
            X = Etel.Axes[1];
            Y = Etel.Axes[2];
            X.Open();
            Y.Open();
            R.Open();

            X.MotionVelParams = new VelocityParams(0.1, 1.0);
            Y.MotionVelParams = new VelocityParams(0.1, 1.0);
            R.MotionVelParams = new VelocityParams(0.1, 1.0);
        }
        private async Task HomingAsync()
        {
            //給軸賦歸速度，不能為null，但無法改變實際速度
            R.HomeVelParams = new VelocityParams(0.01);
            X.HomeVelParams = new VelocityParams(0.01);
            Y.HomeVelParams = new VelocityParams(0.01);
            await Task.WhenAll(X.HomeAsync(), Y.HomeAsync(), R.HomeAsync());
        }
        private void RefreshAxiPosition(object sender, EventArgs e)
        {
            if (Y != null) AxisYPosition = Y.Position;
            if (X != null) AxisXPosition = X.Position;
            if (R != null) AxisRPosition = R.Position;
        }
    }
}