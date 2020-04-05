using MyExamApp.MainWin;
using MyExamApp.Pages.MainWin;
using MyExamApp.Pages.MainWIn;
using MyExamApp.Pages.Other;
using MyExamApp.Pages.Subjects;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyExamApp
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) 
            => Frame.Content = new PageMain().Content;

        private void Click_On_Subject(object sender, RoutedEventArgs e)
        {
            if (BaseOfTasks.isVariantOpen is false)
            {
                var button = e.Source as Button;
                BaseOfTasks.Subject = button.Content.ToString();
                Frame.NavigationService.RemoveBackEntry();
                Frame.Navigate(new CustomVariantHomepage());
            }
            else
            {
                bool exit = BaseOfTasks.CheckerOfActivateVariant();
                if (exit is true)
                {
                    var button = e.Source as Button;
                    BaseOfTasks.Subject = button.Content.ToString();
                    Frame.Navigate(new CustomVariantHomepage());
                    BaseOfTasks.isVariantOpen = false;
                }
            }
            
        }
        private void B_PageMain(object sender, RoutedEventArgs e)
        {
            if (BaseOfTasks.isVariantOpen is false)
                Frame.Content = new PageMain().Content;
            else
            {
                bool exit = BaseOfTasks.CheckerOfActivateVariant();
                if (exit is true)
                {
                    Frame.Content = new PageMain().Content;
                    BaseOfTasks.isVariantOpen = false;
                }
            }
        }

        private void B_Update(object sender, RoutedEventArgs e)
        {
            try
            {
                new ParserWindow().Show();
            } 
            catch(Exception ex)
            {
                MessageBox.Show("Отряд обученных обезьян уже отправлен это исправлять.\nИзвините за технические шоколадки" + "\n\n" + $"Лог ошибки: {ex.Message}", "Успех!", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
        }
        

        private void Instruments_Click(object sender, RoutedEventArgs e)
        {
            if (BaseOfTasks.isVariantOpen is false)
                new PhysCalc().ShowDialog();
            else
            {
                bool exit = BaseOfTasks.CheckerOfActivateVariant();
                if (exit is true)
                {
                    new PhysCalc().ShowDialog();
                    BaseOfTasks.isVariantOpen = false;
                }
            }
            }

        private void Quit_Click(object sender, RoutedEventArgs e) => Close();

        private void AboutOfExam_Click(object sender, RoutedEventArgs e)
        {
            if (BaseOfTasks.isVariantOpen is false)
                Frame.Navigate(new AboutExam());
            else
            {
                bool exit = BaseOfTasks.CheckerOfActivateVariant();
                if (exit is true)
                {
                    Frame.Navigate(new AboutExam());
                    BaseOfTasks.isVariantOpen = false;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (BaseOfTasks.isVariantOpen is false)
            {

            }
            else
            {
                bool exit = BaseOfTasks.CheckerOfActivateVariant();
                if (exit is true)
                {

                }
            }
        }

        private void Videos_Click(object sender, RoutedEventArgs e)
        {
            if (BaseOfTasks.isVariantOpen is false)
            {

            }
            else
            {
                bool exit = BaseOfTasks.CheckerOfActivateVariant();
                if (exit is true)
                {

                }
            }
        }

        private void RefMaterials_Click(object sender, RoutedEventArgs e)
        {
            if (BaseOfTasks.isVariantOpen == false)
                Frame.Navigate(new RefMaterials());
            else
            {
                bool exit = BaseOfTasks.CheckerOfActivateVariant();
                if (exit is true)
                {
                    Frame.Navigate(new RefMaterials());
                    BaseOfTasks.isVariantOpen = false;
                }
            }
        }

        private void Theory_Click(object sender, RoutedEventArgs e)
        {
            if (BaseOfTasks.isVariantOpen is false)
                Frame.Navigate(new Theory());
            else
            {
                bool exit = BaseOfTasks.CheckerOfActivateVariant();
                if (exit is true)
                {
                    Frame.Navigate(new Theory());
                    BaseOfTasks.isVariantOpen = false;
                }
            }
            
        }

        private void MyStats_Click(object sender, RoutedEventArgs e)
        {
            if (BaseOfTasks.isVariantOpen is false)
                Frame.Navigate(new MyStats());
            else
            {
                bool exit = BaseOfTasks.CheckerOfActivateVariant();
                if (exit is true)
                {
                    Frame.Navigate(new MyStats());
                    BaseOfTasks.isVariantOpen = false;
                }
            }
        }
        
        private void Window_Closed(object sender, EventArgs e) => Application.Current.Shutdown();

        private void Settings_Click(object sender, RoutedEventArgs e) => new Settings().Show();
        private void InterDict_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new InterDict());
    }
}
