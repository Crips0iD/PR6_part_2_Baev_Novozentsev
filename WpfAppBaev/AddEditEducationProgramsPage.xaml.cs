using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppBaev
{
    public partial class AddEditEducationProgramsPage : Page
    {
        private EducationPrograms _currentProgram = new EducationPrograms();

        public AddEditEducationProgramsPage(EducationPrograms selectedProgram = null)
        {
            InitializeComponent();

            if (selectedProgram != null)
            {
                _currentProgram = selectedProgram;
                Title = "Редактирование программы";
            }
            else
            {
                Title = "Добавление программы";
                _currentProgram.CreatedAt = DateTime.Now;
            }

            DataContext = _currentProgram;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var context = EducationSystemKyrsachEntities.GetContext();

            if (_currentProgram.ProgramID == 0)
                context.EducationPrograms.Add(_currentProgram);

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