using calendaric.Pages;
using System;
using System.Collections.Generic;
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

namespace calendaric
{
    /// <summary>
    /// Логика взаимодействия для DayEdit.xaml
    /// </summary>
    public partial class DayEdit : Page
    {
        public DateTime currentDate;
        public DayEdit(DateTime curDate)
        {
            InitializeComponent();
            currentDate = curDate;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage mp = new MainPage();
            mp.ShowsNavigationUI = false;
            NavigationService.Navigate(mp);

            CalendarBase cb = new CalendarBase();

            cb.date = currentDate;       

            cb.activities[0] = (bool)rb1.IsChecked;
            cb.activities[1] = (bool)rb2.IsChecked;
            cb.activities[2] = (bool)rb3.IsChecked;
            cb.activities[3] = (bool)rb4.IsChecked;
            cb.activities[4] = (bool)rb5.IsChecked;

            if (MainWindow.activitiesDays.Find(x => x.date == cb.date) == null)
                MainWindow.activitiesDays.Add(cb);
            else
                MainWindow.activitiesDays.Find(x => x.date == cb.date).activities = cb.activities;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DateLabel.Content = currentDate.ToString("D");

            var gt = MainWindow.activitiesDays.Find(x => x.date == currentDate);

            if (gt != null)
            {
                rb1.IsChecked = gt.activities[0];
                rb2.IsChecked = gt.activities[1];
                rb3.IsChecked = gt.activities[2];
                rb4.IsChecked = gt.activities[3];
                rb5.IsChecked = gt.activities[4];
            }
        }
    }
}
