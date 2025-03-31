using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfAppBaev
{
    public partial class EducationProgramsPage : Page
    {
        public EducationProgramsPage()
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
                ListViewEducationPrograms.ItemsSource = EducationSystemKyrsachEntities.GetContext()
                    .EducationPrograms
                    .OrderBy(p => p.ProgramID)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки программ: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditEducationProgramsPage());
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewEducationPrograms.SelectedItem == null)
            {
                MessageBox.Show("Выберите программу для редактирования", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            NavigationService.Navigate(new AddEditEducationProgramsPage(
                ListViewEducationPrograms.SelectedItem as EducationPrograms));
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewEducationPrograms.SelectedItem == null) return;

            if (MessageBox.Show("Удалить программу и все связанные курсы?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    var program = ListViewEducationPrograms.SelectedItem as EducationPrograms;
                    var context = EducationSystemKyrsachEntities.GetContext();

                    var courses = context.Courses.Where(c => c.ProgramID == program.ProgramID).ToList();
                    foreach (var course in courses)
                    {
                        context.Materials.RemoveRange(
                            context.Materials.Where(m => m.CourseID == course.CourseID));
                    }
                    context.Courses.RemoveRange(courses);
                    context.EducationPrograms.Remove(program);
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