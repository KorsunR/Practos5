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

namespace calendaric.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        DateTime currentDate;
        CalendarDay[] days;
        int daysCount;
        public MainPage()
        {
            InitializeComponent();
        }

        public void PrintMonth()
        {
            daysCount = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            days = new CalendarDay[daysCount];

            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(@"\Resources\1.png", UriKind.Relative);
            img.EndInit();


            CurrentDateLabel.Content = currentDate.ToString("MMMM yyyy");

            int row = 0;
            int col = 0;
            for (int i = 0; i < daysCount; i++)
            {
                days[i] = new CalendarDay(i+1);
                days[i].Width = WindowWidth / 7.5;
                days[i].Height = CalendarGrid.ActualHeight / 7;
                days[i].Margin = new Thickness(col * (WindowWidth / 7.5 + 5), row * (CalendarGrid.ActualHeight / 7 + 5), 0, 0);
                days[i].Caption.Content = (i + 1).ToString();
                days[i].VerticalAlignment = VerticalAlignment.Top;
                days[i].HorizontalAlignment = HorizontalAlignment.Left;
                days[i].SetValue(Grid.RowProperty, 1);
                days[i].SetValue(Grid.ColumnProperty, 0);

                days[i].MouseLeftButtonDown += ParticularDay_Click;

                SetImage(days[i]);

                CalendarGrid.Children.Add(days[i]);
                col++;
                if (col == 7)
                {
                    row++;
                    col = 0;
                }
            }
        }

        private void SetImage(CalendarDay calendarDay)
        {
            DateTime tmpDate = new DateTime(currentDate.Year, currentDate.Month, calendarDay.DayNumber);
            DayEdit tmpDayEdit = new DayEdit(currentDate);

            var gt = MainWindow.activitiesDays.Find(x => x.date == tmpDate);

            if (gt != null)
            {
                if (gt.activities[0] == true)
                    calendarDay.Picture.Source = new BitmapImage(new Uri((string)tmpDayEdit.rb1.Content, UriKind.Relative));
                else if (gt.activities[1] == true)
                    calendarDay.Picture.Source = new BitmapImage(new Uri((string)tmpDayEdit.rb2.Content, UriKind.Relative));
                else if (gt.activities[2] == true)
                    calendarDay.Picture.Source = new BitmapImage(new Uri((string)tmpDayEdit.rb3.Content, UriKind.Relative));
                else if (gt.activities[3] == true)
                    calendarDay.Picture.Source = new BitmapImage(new Uri((string)tmpDayEdit.rb4.Content, UriKind.Relative));
                else if (gt.activities[4] == true)
                    calendarDay.Picture.Source = new BitmapImage(new Uri((string)tmpDayEdit.rb5.Content, UriKind.Relative));
            }
        }

        private void ParticularDay_Click(object sender, RoutedEventArgs e)
        {
            currentDate = new DateTime(currentDate.Year, currentDate.Month, ((CalendarDay)sender).DayNumber);
            DayEdit de = new DayEdit(currentDate);
            de.ShowsNavigationUI = false;
            NavigationService.Navigate(de);
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < daysCount; i++)
                CalendarGrid.Children.Remove(days[i]);

            currentDate = currentDate.AddMonths(-1);

            PrintMonth();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < daysCount; i++)
                CalendarGrid.Children.Remove(days[i]);

            currentDate = currentDate.AddMonths(1);

            PrintMonth();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            currentDate = DateTime.Now;
            PrintMonth();
        }
    }
}
