using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppBaev
{
    public partial class AddEditMaterialsPage : Page
    {
        private Materials _currentMaterial = new Materials();

        public AddEditMaterialsPage(Materials selectedMaterial = null)
        {
            InitializeComponent();

            if (selectedMaterial != null)
            {
                _currentMaterial = selectedMaterial;
                Title = "Редактирование материала";
            }
            else
            {
                Title = "Добавление материала";
            }

            DataContext = _currentMaterial;
            LoadCourses();
            LoadMaterialTypes();
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

        private void LoadMaterialTypes()
        {
            MaterialTypeComboBox.ItemsSource = new string[] { "Видео", "Документ", "Презентация", "Ссылка" };
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var context = EducationSystemKyrsachEntities.GetContext();

            if (_currentMaterial.MaterialID == 0)
                context.Materials.Add(_currentMaterial);

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