using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfAppBaev
{
    public partial class CoursesPage : Page
    {
        public CoursesPage()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigated += NavigationService_Navigated;
            NavigationService.Navigating += NavigationService_Navigating;
            LoadData();
        }

        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content == this) LoadData();
        }

        private void NavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New || e.NavigationMode == NavigationMode.Back)
            {
                NavigationService.Navigated -= NavigationService_Navigated;
                NavigationService.Navigating -= NavigationService_Navigating;
            }
        }

        private void LoadData()
        {
            try
            {
                DataGridCourses.ItemsSource = EducationSystemKyrsachEntities.GetContext()
                    .Courses.Include(c => c.EducationPrograms)
                    .OrderBy(c => c.CourseID)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки курсов: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditCoursesPage());
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridCourses.SelectedItem == null)
            {
                MessageBox.Show("Выберите курс для редактирования", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            NavigationService.Navigate(new AddEditCoursesPage(DataGridCourses.SelectedItem as Courses));
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridCourses.SelectedItem == null) return;

            if (MessageBox.Show("Удалить курс и все связанные материалы?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    var course = DataGridCourses.SelectedItem as Courses;
                    var context = EducationSystemKyrsachEntities.GetContext();

                    context.Materials.RemoveRange(
                        context.Materials.Where(m => m.CourseID == course.CourseID));
                    context.Courses.Remove(course);
                    context.SaveChanges();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}