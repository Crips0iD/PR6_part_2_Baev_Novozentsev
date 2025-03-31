using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfAppBaev
{
    public partial class MaterialsPage : Page
    {
        public MaterialsPage()
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
                DataGridMaterials.ItemsSource = EducationSystemKyrsachEntities.GetContext()
                    .Materials.Include(m => m.Courses)
                    .OrderBy(m => m.MaterialID)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки материалов: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditMaterialsPage());
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridMaterials.SelectedItem == null)
            {
                MessageBox.Show("Выберите материал для редактирования", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            NavigationService.Navigate(new AddEditMaterialsPage(DataGridMaterials.SelectedItem as Materials));
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridMaterials.SelectedItem == null) return;

            if (MessageBox.Show("Удалить материал?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    var material = DataGridMaterials.SelectedItem as Materials;
                    var context = EducationSystemKyrsachEntities.GetContext();
                    context.Materials.Remove(material);
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