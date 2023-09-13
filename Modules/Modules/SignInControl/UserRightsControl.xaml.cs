using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    /// UserRightsControl.xaml 的互動邏輯
    /// </summary>
    public partial class UserRightsControl : UserControl, INotifyPropertyChanged
    {
        private static readonly DependencyProperty AccountProperty = DependencyProperty.Register(nameof(Account), typeof(UserAccount), typeof(UserRightsControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
       
        public UserRightsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 使用者資訊
        /// </summary>
        public UserAccount Account { get => (UserAccount)GetValue(AccountProperty); set => SetValue(AccountProperty, value); }

        /// <summary>
        /// 登入使用者名稱
        /// </summary>
        public string UserName { get; set; } = "";

        /// <summary>
        /// 使用者管理
        /// </summary>
        public ICommand UserManagerCommand => new RelayCommand(() =>
        {
            if (Account == null) return;

            UserManagerWindow win = new UserManagerWindow(Account);

            win.ShowDialog();
        });

        /// <summary>
        /// 登入登出
        /// </summary>
        public ICommand LogoutCommand => new RelayCommand(() =>
        {
            if (Account == null)
            {
                UserName = "";
                return; 
            }

            Account.Logout();
            SignInWindow win = new SignInWindow(Account);
            win.ShowDialog();
            Account = win.Account;
            UserName = win.Account.CurrentAccount.Name;
        });

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

    [ValueConversion(typeof(Enum), typeof(bool))]
    public class EnumToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null || !(value is Enum))
                return Visibility.Collapsed;

            var currentState = value.ToString();
            var stateStrings = parameter.ToString();
            var found = false;

            foreach (var state in stateStrings.Split(','))
            {
                found = (currentState == state.Trim());

                if (found)
                    break;
            }

            return found;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
