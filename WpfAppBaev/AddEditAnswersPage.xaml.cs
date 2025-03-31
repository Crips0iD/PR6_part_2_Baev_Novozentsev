using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppBaev
{
    public partial class AddEditAnswersPage : Page
    {
        private Answers _currentAnswer = new Answers();

        public AddEditAnswersPage(Answers selectedAnswer = null)
        {
            InitializeComponent();

            if (selectedAnswer != null)
            {
                _currentAnswer = selectedAnswer;
                Title = "Редактирование ответа";
            }
            else
            {
                Title = "Добавление ответа";
            }

            DataContext = _currentAnswer;
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            try
            {
                QuestionIdComboBox.ItemsSource = EducationSystemKyrsachEntities.GetContext().Questions.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки вопросов: {ex.Message}");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var context = EducationSystemKyrsachEntities.GetContext();

            if (_currentAnswer.AnswerID == 0) // Новый ответ
                context.Answers.Add(_currentAnswer);

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