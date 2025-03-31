using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppBaev
{
    public partial class AddEditCoursesPage : Page
    {
        private Courses _currentCourse = new Courses();

        public AddEditCoursesPage(Courses selectedCourse = null)
        {
            InitializeComponent();

            if (selectedCourse != null)
            {
                _currentCourse = selectedCourse;
                Title = "Редактирование курса";
            }
            else
            {
                Title = "Добавление курса";
            }

            DataContext = _currentCourse;
            LoadPrograms();
        }

        private void LoadPrograms()
        {
            try
            {
                ProgramIdComboBox.ItemsSource = EducationSystemKyrsachEntities.GetContext().EducationPrograms.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки программ: {ex.Message}");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var context = EducationSystemKyrsachEntities.GetContext();

            if (_currentCourse.CourseID == 0)
                context.Courses.Add(_currentCourse);

            try
            {
                context.SaveChanges();
                MessageBox.Show("Данные сохранены!");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}