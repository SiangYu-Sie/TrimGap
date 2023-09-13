using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
    /// <summary>
    /// AxisSettingControl.xaml 的互動邏輯
    /// </summary>
    public partial class AxisSettingControl : UserControl, INotifyPropertyChanged
    {
        private static readonly DependencyProperty AxisGroupProperty = DependencyProperty.Register("AxisGroup", typeof(AxisSettingControlGroup[]), typeof(AxisSettingControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, GroupChange));
        private static readonly DependencyProperty ApplyCommandProperty = DependencyProperty.Register("ApplyCommand", typeof(ICommand), typeof(AxisSettingControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public AxisSettingControl()
        {
            InitializeComponent();
            control.DataContext = this;
        }

        /// <summary>
        /// 軸
        /// </summary>
        public AxisSettingControlGroup[] AxisGroup { get => (AxisSettingControlGroup[])GetValue(AxisGroupProperty); set => SetValue(AxisGroupProperty, value); }
        /// <summary>
        /// 套用
        /// </summary>
        public ICommand ApplyCommand { get => (ICommand)GetValue(ApplyCommandProperty); set => SetValue(ApplyCommandProperty, value); }
        /// <summary>
        /// 軸 顯示
        /// </summary>
        public ObservableCollection<AxisSettingUIUseClass> AxisSettingUIUseClasses { get; set; }

        private void LoadedCommand(object sender, RoutedEventArgs e)
        {
            if (AxisGroup != null) createAxisSettingUIUseClasses();

            ApplyCommand = new RelayCommand(ApplyAllAxis, null);
        }

        private void createAxisSettingUIUseClasses()
        {
            AxisSettingUIUseClass[] axisSettingUIUseClasses = AxisGroup.Select(axis =>
            {
                return new AxisSettingUIUseClass()
                {
                    Axis = axis.Axis,
                    AxisConfig = axis.AxisConfig,
                    HomeAction = axis.HomeAction,
                    IsReverse = axis.IsReverse,
                };
            }).ToArray();
            AxisSettingUIUseClasses = new ObservableCollection<AxisSettingUIUseClass>(axisSettingUIUseClasses);
        }

        private void ApplyAllAxis()
        {
            foreach (AxisSettingUIUseClass axisSettingUIUseClass in AxisSettingUIUseClasses)
            {
                if (axisSettingUIUseClass.LockAxis != null) axisSettingUIUseClass.LockAxis.Execute(null);
                int axisIndex = AxisSettingUIUseClasses.IndexOf(axisSettingUIUseClass);
                AxisGroup[axisIndex].AxisConfig = axisSettingUIUseClass.AxisConfig;
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

        private static void GroupChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as AxisSettingControl;
            if (e.NewValue != null) control.createAxisSettingUIUseClasses();
        }
    }

    public class AxisSettingUIUseClass : INotifyPropertyChanged
    {
        /// <summary>
        /// 軸
        /// </summary>
        public Axis Axis { get; set; }
        /// <summary>
        /// 軸資料
        /// </summary>
        public AxisConfig AxisConfig { get; set; }
        /// <summary>
        /// 原點事件
        /// </summary>
        public Action<(Axis, AxisConfig)> HomeAction { get; set; }
        /// <summary>
        /// 軸 反向
        /// </summary>
        public bool IsReverse { get; set; }
        /// <summary>
        /// 鎖
        /// </summary>
        public ICommand LockAxis { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged<T>(string name, T oldValue, T newValue)
        {
            // oldValue 和 newValue 目前沒有用到，代爾後需要再實作。
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class AxisSettingControlGroup : INotifyPropertyChanged
    {
        /// <summary>
        /// 軸
        /// </summary>
        public Axis Axis { get; set; }
        /// <summary>
        /// 軸 資料
        /// </summary>
        public AxisConfig AxisConfig { get; set; }
        /// <summary>
        /// 原點 事件
        /// </summary>
        public Action<(Axis, AxisConfig)> HomeAction { get; set; }
        /// <summary>
        /// 是否反向
        /// </summary>
        public bool IsReverse { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged<T>(string name, T oldValue, T newValue)
        {
            // oldValue 和 newValue 目前沒有用到，代爾後需要再實作。
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    internal class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute)
        {
            _execute = execute;
        }
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }


    internal class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute)
        {
            _execute = execute;
        }
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);

        public void Execute(object parameter) => _execute((T)parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
