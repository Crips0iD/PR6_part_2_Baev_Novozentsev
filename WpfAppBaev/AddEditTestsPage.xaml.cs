using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppBaev
{
    public partial class AddEditTestsPage : Page
    {
        private Tests _currentTest = new Tests();

        public AddEditTestsPage(Tests selectedTest = null)
        {
            InitializeComponent();

            if (selectedTest != null)
            {
                _currentTest = selectedTest;
                Title = "Редактирование теста";
            }
            else
            {
                Title = "Добавление теста";
            }

            DataContext = _currentTest;
            LoadCourses();
        }

        private void LoadCourses()
        {
            try
            {
                CourseIdComboBox.ItemsSource = EducationSystemKyrsachEntities.GetContext().Courses.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки курсов: {ex.Message}");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var context = EducationSystemKyrsachEntities.GetContext();

            if (_currentTest.TestID == 0)
                context.Tests.Add(_currentTest);

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