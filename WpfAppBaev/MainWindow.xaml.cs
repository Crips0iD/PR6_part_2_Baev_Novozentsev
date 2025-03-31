using System.Windows;
using System.Windows.Controls;

namespace WpfAppBaev
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Login(this));
        }

        private void EducationProgramsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new EducationProgramsPage());
        }

        private void CoursesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CoursesPage());
        }

        private void MaterialsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MaterialsPage());
        }

        private void TestsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TestsPage());
        }

        private void AnswersButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AnswersPage());
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HomePage());
        }
    }
}