using Nazv_orgsnizaciy.windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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


namespace Nazv_orgsnizaciy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    //
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<Service> _ServiceList;
        public List<Service> ServiceList
        {
            get {
                var FilteredServiceList = _ServiceList.FindAll(item =>
                item.DiscountFloat >= CurrentDiscountFilter.Item1 &&
                item.DiscountFloat < CurrentDiscountFilter.Item2);

                if (SearchFilter != "")
                    FilteredServiceList = FilteredServiceList.Where(item =>
                        item.Title.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) != -1 ||
                        item.DescriptionString.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) != -1).ToList();

                if (SortPriceAscending)
                    return FilteredServiceList
                        .OrderBy(item => Double.Parse(item.CostWithDiscount))
                        .ToList();
                else
                    return FilteredServiceList
                        .OrderByDescending(item => Double.Parse(item.CostWithDiscount))
                        .ToList();
            }
            set {
                _ServiceList = value;
              if (PropertyChanged != null)
               PropertyChanged(this, new PropertyChangedEventArgs("ServiceList"));
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            ServiceList = Core.DB.Service.ToList();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private Boolean _IsAdminMode = false;

        public event PropertyChangedEventHandler PropertyChanged;

        // публичный геттер, который меняет текущий режим (Админ/не Админ)
        public Boolean IsAdminMode
        {
            get { return _IsAdminMode; }
            set
            {
                _IsAdminMode = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AdminModeCaption"));
                    PropertyChanged(this, new PropertyChangedEventArgs("AdminVisibility"));
                }
            }
        }
        // этот геттер возвращает текст для кнопки в зависимости от текущего режима
        public string AdminModeCaption
        {
            get
            {
                if (IsAdminMode) return "Выйти из режима\nАдминистратора";
                return "Войти в режим\nАдминистратора";
            }
        }


        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            // если мы уже в режиме Администратора, то выходим из него 
            if (IsAdminMode) IsAdminMode = false;
            else
            {
                // создаем окно для ввода пароля
                var InputBox = new InputBoxWindow("Введите пароль Администратора");
                // и показываем его как диалог (модально)
                if ((bool)InputBox.ShowDialog())
                {
                    // если нажали кнопку "Ok", то включаем режим, если пароль введен верно
                    IsAdminMode = InputBox.InputText == "0000";
                }
            }
        }


        public string AdminVisibility
        {
            get
            {
                if (IsAdminMode) return "Visible";
                return "Collapsed";
            }
        }
        private Boolean _SortPriceAscending = true;
        public Boolean SortPriceAscending
        {
            get { return _SortPriceAscending; }
            set
            {
                _SortPriceAscending = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ServiceList"));
                    PropertyChanged(this, new PropertyChangedEventArgs("ServicesCount"));
                    PropertyChanged(this, new PropertyChangedEventArgs("FilteredServicesCount"));
                }
            }
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SortPriceAscending = (sender as RadioButton).Tag.ToString() == "1";
        }


        private List<Tuple<string, double, double>> FilterByDiscountValuesList =
        new List<Tuple<string, double, double>>() {
        Tuple.Create("Все записи", 0d, 1d),
        Tuple.Create("от 0% до 5%", 0d, 0.05d),
        Tuple.Create("от 5% до 15%", 0.05d, 0.15d),
        Tuple.Create("от 15% до 30%", 0.15d, 0.3d),
        Tuple.Create("от 30% до 70%", 0.3d, 0.7d),
        Tuple.Create("от 70% до 100%", 0.7d, 1d)
        };

        public List<string> FilterByDiscountNamesList
        {
            get
            {
                return FilterByDiscountValuesList
                    .Select(item => item.Item1)
                    .ToList();
            }
        }

        private Tuple<double, double> _CurrentDiscountFilter = Tuple.Create(double.MinValue, double.MaxValue);

        public Tuple<double, double> CurrentDiscountFilter
        {
            get
            {
                return _CurrentDiscountFilter;
            }
            set
            {
                _CurrentDiscountFilter = value;
                if (PropertyChanged != null)
                {
                    // при изменении фильтра список перерисовывается
                    PropertyChanged(this, new PropertyChangedEventArgs("ServiceList"));
                    PropertyChanged(this, new PropertyChangedEventArgs("ServicesCount"));
                    PropertyChanged(this, new PropertyChangedEventArgs("FilteredServicesCount"));
                }
            }
        }


        private string _SearchFilter = "";
        public string SearchFilter
        {
            get { return _SearchFilter; }
            set
            {
                _SearchFilter = value;
                if (PropertyChanged != null)
                {
                    // при изменении фильтра список перерисовывается
                    PropertyChanged(this, new PropertyChangedEventArgs("ServiceList"));
                    PropertyChanged(this, new PropertyChangedEventArgs("ServicesCount"));
                    PropertyChanged(this, new PropertyChangedEventArgs("FilteredServicesCount"));
                }
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            SearchFilter = SearchFilterTextBox.Text;
        }

        private void DiscountFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DiscountFilterComboBox.SelectedIndex >= 0)
                CurrentDiscountFilter = Tuple.Create(
                FilterByDiscountValuesList[DiscountFilterComboBox.SelectedIndex].Item2,
                FilterByDiscountValuesList[DiscountFilterComboBox.SelectedIndex].Item3
            );
        }

        public int ServicesCount
        {
            get
            {
                
                return _ServiceList.Count;

            }
            set
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("_ServiceList"));
                
            }
        }
        
        public int FilteredServicesCount
        {
            get
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ServiceList.Count"));
                return ServiceList.Count;
            }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var item = MainDataGrid.SelectedItem as Service;

            if (item.ClientService.Count > 0)
            {
                MessageBox.Show("Нельзя удалять услугу, она уже оказана");
                return;
            }

            Core.DB.Service.Remove(item);

            Core.DB.SaveChanges();

            ServiceList = Core.DB.Service.ToList();
        }

        private void EditButon_Click(object sender, RoutedEventArgs e)
        {
            var SelectedService = MainDataGrid.SelectedItem as Service;
            var EditServiceWindow = new ServiceWindow(SelectedService);
            if ((bool)EditServiceWindow.ShowDialog())
            {
                // при успешном завершении не забываем перерисовать список услуг
                PropertyChanged(this, new PropertyChangedEventArgs("ServiceList"));
                // и еще счетчики - их добавьте сами
            }
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            // создаем новую услугу
            var NewService = new Service();

            var NewServiceWindow = new ServiceWindow(NewService);
            if ((bool)NewServiceWindow.ShowDialog())
            {
                // список услуг нужно перечитать с сервера
                ServiceList = Core.DB.Service.ToList();
                PropertyChanged(this, new PropertyChangedEventArgs("FilteredProductsCount"));
                PropertyChanged(this, new PropertyChangedEventArgs("ProductsCount"));
            }
        }

        private void SubscrideButton_Click(object sender, RoutedEventArgs e)
        {
            var SelectedService = MainDataGrid.SelectedItem as Service;
            var SubscrideServiceWindow = new windows.ClientServiceWindow(SelectedService);
            SubscrideServiceWindow.ShowDialog();

        }
     }
}
