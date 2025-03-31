using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfAppBaev
{
    public partial class AnswersPage : Page
    {
        public AnswersPage()
        {
            InitializeComponent();
            Loaded += AnswersPage_Loaded;
        }

        private void AnswersPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Подписываемся на события после загрузки страницы
            NavigationService.Navigated += NavigationService_Navigated;
            NavigationService.Navigating += NavigationService_Navigating;
            LoadData();
        }

        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            // Обновляем данные только если вернулись на эту страницу
            if (e.Content == this)
            {
                LoadData();
            }
        }

        private void NavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            // Отписываемся от событий при уходе со страницы
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
                DataGridAnswer.ItemsSource = EducationSystemKyrsachEntities.GetContext()
                    .Answers
                    .OrderBy(a => a.AnswerID)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditAnswersPage());
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridAnswer.SelectedItem == null)
            {
                MessageBox.Show("Выберите ответ", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            NavigationService.Navigate(new AddEditAnswersPage(DataGridAnswer.SelectedItem as Answers));
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridAnswer.SelectedItem == null) return;

            if (MessageBox.Show("Удалить ответ?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    var answer = DataGridAnswer.SelectedItem as Answers;
                    var context = EducationSystemKyrsachEntities.GetContext();
                    context.Answers.Remove(answer);
                    context.SaveChanges();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}