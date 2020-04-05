using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyExamApp.Pages.Subjects
{
    public partial class CustomVariantHomepage : Page
    {
        public static string Folder { get; set; }
        public CustomVariantHomepage()
        {
            InitializeComponent();
            
            TaskCatalog taskCatalog = new TaskCatalog();
            Frame.NavigationService.Navigate(taskCatalog);
            const int MAX_VAR = 15;
            for (int i = 1; i <= MAX_VAR; i++)
            {
                Button b_Variant = new Button();
                b_Variant.Content = $"Вариант {i}";
                b_Variant.Name = $"variant{i}";
                b_Variant.Height = 45;
                b_Variant.Width = 195;
                
                b_Variant.Style = FindResource("butStyle") as Style; 
                b_Variant.Click += B_OpenVariant;
                FirstBtn.Style = FindResource("butStyle") as Style;
                SecondBtn.Style = FindResource("butStyle") as Style;
                ButMenu.Items.Add(b_Variant);
                
            }
            if(BaseOfTasks.Subject.Equals("Математика"))
            {
                Folder = "Базовый уровень";
                FirstBtn.Content = "Базовый уровень";
                SecondBtn.Content = "Профильный уровень";
                LabelVar.Content = "Тренировочные материалы (Базовый уровень)";
                CustomSubButtons.Visibility = Visibility.Visible;
            }
            else if(BaseOfTasks.Subject.Equals("Русский язык"))
            {
                Folder = "Русский язык";
                FirstBtn.Content = "Русский язык";
                SecondBtn.Content = "Итоговое сочинение";
                CustomSubButtons.Visibility = Visibility.Visible;
            }
        }
        private void B_OpenVariant(object sender, RoutedEventArgs e)
        {
            var name = e.Source as Button;
            BaseOfTasks.Variant = name.Name;
            if (BaseOfTasks.Subject.Equals("Математика"))
                BaseOfTasks.Subject = $"Математика/{Folder}";
            
            BaseOfTasks.LoadResources();
            ushort[] time = { };
            var variant = new WrapPanel();
            switch(BaseOfTasks.Subject)
            {
                case "Математика/Базовый уровень":
                    
                    break;
                case "Математика/Профильный уровень":
                    variant.Children.Add(MathSpec.CreateVariant());
                    time = MathSpec.Time;
                    break;
                case "Информатика":
                    break;
                case "Физика":
                    break;
                case "Обществознание":
                    break;
                case "Русский язык":
                    variant.Children.Add(Russian.CreateVariant());
                    time = Russian.Time;
                    break;
                case "География":
                    break;
                case "История":
                    break;
                case "Литература":
                   
                    break;
            }
            NavigationService.Navigate(new CustVariant(variant, time[0], time[1]));
            BaseOfTasks.ReleaseResources();
        }

        private void SecondBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = e.Source as Button;
            if(button.Content.ToString() is "Итоговое сочинение")
            {
            }
            else
            {
                LabelVar.Content = "Тренировочные материалы (Профильный уровень)";
                Folder = "Профильный уровень";
            }
        }

        private void FirstBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = e.Source as Button;
            if(button.Content.ToString() is "Базовый уровень")
            {
                LabelVar.Content = "Тренировочные материалы (Базовый уровень)";
                Folder = "Базовый уровень";
            }
        }
    }
}
