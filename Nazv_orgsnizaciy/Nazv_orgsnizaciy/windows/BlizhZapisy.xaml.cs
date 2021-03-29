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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Nazv_orgsnizaciy
{
    public partial class ClientService
    {
        public string EndTime
        {
            get
            {
                //return (DateTime.Now.Subtract(StartTime).ToString());
                var date1 = (StartTime - DateTime.Now);
                //return date1("hh:mm:ss");
                return $"часов: {(int)date1.TotalHours}, минуты:{date1.Minutes}";
            }
        }
    }
}


namespace Nazv_orgsnizaciy.windows
{
    /// <summary>
    /// Логика взаимодействия для BlizhZapisy.xaml
    /// </summary>
    public partial class BlizhZapisy : Window, INotifyPropertyChanged
    {

        private List<ClientService> _CLientList;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<ClientService> CLiientList
        {
            get
            {
                return _CLientList
                    .Where(
                        cli => cli.StartTime > DateTime.Now && cli.StartTime < DateTime.Now.AddDays(2)
                    ).OrderBy(cli => cli.StartTime).ToList();

            }
            set
            {
                _CLientList = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CLiientList"));
                }
            }
        }

        public BlizhZapisy()
        {
            InitializeComponent();
            DataContext = this;
            CLiientList = Core.DB.ClientService.ToList();
            var Timer = new DispatcherTimer();
            // задаем интервал 
            Timer.Interval = TimeSpan.FromMinutes(1);
            // задаем обработчик события таймера (их может быть несколько)
            Timer.Tick += onTimerEvent;
            // и запускаем таймер
            Timer.Start();
        }
        void BlizhZapisy_CLosing(object sender, CancelEventArgs e)
        {
            Globals.BlizhZapisy_Open = null;
        }

        private void onTimerEvent(object Sender, EventArgs ea)
        {
            CLiientList = Core.DB.ClientService.ToList();
        }

        
    }
}
