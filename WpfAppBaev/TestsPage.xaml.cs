using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfAppBaev
{
    public partial class TestsPage : Page
    {
        public TestsPage()
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
                DataGridTests.ItemsSource = EducationSystemKyrsachEntities.GetContext()
                    .Tests.Include(t => t.Courses)
                    .OrderBy(t => t.TestID)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки тестов: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditTestsPage());
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridTests.SelectedItem == null)
            {
                MessageBox.Show("Выберите тест для редактирования", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            NavigationService.Navigate(new AddEditTestsPage(DataGridTests.SelectedItem as Tests));
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridTests.SelectedItem == null) return;

            if (MessageBox.Show("Удалить тест и все связанные вопросы?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    var test = DataGridTests.SelectedItem as Tests;
                    var context = EducationSystemKyrsachEntities.GetContext();

                    var questions = context.Questions.Where(q => q.TestID == test.TestID).ToList();
                    foreach (var question in questions)
                    {
                        context.Answers.RemoveRange(
                            context.Answers.Where(a => a.QuestionID == question.QuestionID));
                    }
                    context.Questions.RemoveRange(questions);
                    context.Tests.Remove(test);
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