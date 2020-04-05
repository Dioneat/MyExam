using MyExamApp.Pages.Other;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyExamApp.Pages.Subjects
{
    public partial class CustomVariant : Page
    {
        
        
        public CustomVariant()
        {
            InitializeComponent();
            BaseOfTasks.ReadTasksFromBase();
            switch(BaseOfTasks.Subject)
            {
                case "Русский язык":
                    ConstructorRusVariant();
                    break;
                case "Математика/Базовый уровень":
                    ConstructorBasicMathVariant();
                    break;
                case "Математика/Профильный уровень":
                    ConstructorSpecialMath();
                    break;
                case "Информатика":
                    ConstructorInfVariant();
                    break;
                case "Физика":
                    ConstructorPhysVariant();
                    break;
                case "Итоговое сочинение":
                    ConstructorWrittingVariant();
                    break;
            }
            
            BaseOfTasks.TaskCollection.Clear();
            BaseOfTasks.Image.Clear();
            BaseOfTasks.Description.Clear();
            BaseOfTasks.CorrectAnswer.Clear();
            BaseOfTasks.UserAnswer.Clear();

        }

        public static bool CheckerOfActivateVariant()
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите прекратить выполнения заданий?\nВариант не будет учитываться в статистике, а формы будут сброшены","Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                return true;
            else
                return false;
        }
        private void ButtonAnswer_Click(object sender, RoutedEventArgs e)
        {
            var page = new ExpandendPart();
            MessageBoxResult result;
            switch (BaseOfTasks.Subject)
            {
                case "Русский язык":
                    MessageBoxResult choice = MessageBox.Show("Вы будете писать сочинение в тетради или в программе?\n\nДа - в тетради(вы сразу перейдете к проверке 2-й части)\nНет - в программе(вы перейдете в соответствующий раздел и будете там писать сочинение с последующей программной проверкой)", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if(choice == MessageBoxResult.Yes)
                    {
                        foreach (TextBox textBox in WPExamples.Children.OfType<TextBox>())
                        {
                            BaseOfTasks.UserAnswer.Add(textBox.Text.ToLower());
                        }
                        this.NavigationService.Navigate(page);
                    }
                    else
                    {
                        var essay = new ExpPartEssay();
                        foreach (TextBox textBox in WPExamples.Children.OfType<TextBox>())
                        {
                            BaseOfTasks.UserAnswer.Add(textBox.Text.ToLower());
                        }
                        this.NavigationService.Navigate(essay);
                    }
                    
                    break;
                case "Физика":
                    result = MessageBox.Show("Вы уверены, что готовы перейти к проверке части с развернутым ответом?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (TextBox textBox in WPExamples.Children.OfType<TextBox>())
                        {
                            BaseOfTasks.UserAnswer.Add(textBox.Text.ToLower());
                        }
                        this.NavigationService.Navigate(page);
                    }
                    break;
                case "Математика/Базовый уровень":
                    var answerPage = new AnswerPage();
                    result = MessageBox.Show("Вы уверены, что готовы перейти к ответам?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (TextBox textBox in WPExamples.Children.OfType<TextBox>())
                        {
                            BaseOfTasks.UserAnswer.Add(textBox.Text.ToLower());
                        }
                        this.NavigationService.Navigate(answerPage);
                    }
                    break;
                case "Математика/Профильный уровень":
                    result = MessageBox.Show("Вы уверены, что готовы перейти к проверке части с развернутым ответом?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (TextBox textBox in WPExamples.Children.OfType<TextBox>())
                        {
                            BaseOfTasks.UserAnswer.Add(textBox.Text.ToLower());
                        }
                        this.NavigationService.Navigate(page);
                    }
                    break;
                case "Итоговое сочинение":
                    
                    break;
                case "Информатика":

                    break;
            }
        }
        #region ---Конструкторы вариантов---
        public void ConstructorRusVariant()
        {
            var j = 0;

            while (j < BaseOfTasks.TaskCollection.Count)
            {

                var label = new Label();
                label.Content = $"Задание №{j + 1}";
                label.FontWeight = FontWeights.Bold;
                label.Width = 1712;
                label.Height = 30;
                WPExamples.Children.Add(label);

                var textBlock = new TextBlock
                {
                    Text = BaseOfTasks.TaskCollection[j],
                    Width = double.NaN,
                    FontSize = 15,
                    Height = double.NaN,
                    TextWrapping = TextWrapping.Wrap,
                };

                WPExamples.Children.Add(textBlock);
                var splitter = new Label();
                splitter.Height = 3;
                splitter.Width = 1719;
                WPExamples.Children.Add(splitter);
                if (j < 26)
                {
                    var textBox = new TextBox
                    {
                        Height = 24,
                        Width = 458
                    };
                    WPExamples.Children.Add(textBox);
                }



                var EndLabel = new Label
                {
                    Height = 30,
                    Width = 1087
                };
                WPExamples.Children.Add(EndLabel);

                var border = new Border
                {
                    Height = 2,
                    Width = 1710,
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black,
                    RenderTransformOrigin = new Point(0.5, 0.5),
                    Margin = new Thickness(0, 10, 0, 10)
                };
                WPExamples.Children.Add(border);
                j++;
            }

            var ButtonAnswer = new Button
            {
                Width = 162,
                Content = "Ответить",
                Height = 28,
                Margin = new Thickness(750, 0, 0, 0)
            };
            ButtonAnswer.Click += ButtonAnswer_Click;
            WPExamples.Children.Add(ButtonAnswer);

            for (int i = 0; i < 5; i++)
            {
                var SubLabel = new Label
                {
                    Height = 30,
                    Width = 1087
                };
                WPExamples.Children.Add(SubLabel);
            }

        }
        public void ConstructorWrittingVariant()
        {

        }
        public void ConstructorBasicMathVariant()
        {

            var j = 0;
            BaseOfTasks.ReadTaskImages();

            while (j < BaseOfTasks.TaskCollection.Count)
            {
                var image_source = new List<string>();
                for (int i = 0; i < BaseOfTasks.Image[j].Count; i++) // путь для изображений определенного задания
                {

                    string s = $@"{Directory.GetCurrentDirectory()}/{BaseOfTasks.Subject}/Варианты/img_{BaseOfTasks.Variant}/{BaseOfTasks.Image[j][i]}";

                    image_source.Add(s);

                }

                var label = new Label();
                label.Content = $"Задание №{j + 1}";
                label.FontWeight = FontWeights.Bold;
                label.Width = 1790;
                label.FontSize = 15;
                label.Height = 30;
                WPExamples.Children.Add(label);



                if (image_source.Count > 1 == false)
                {
                    var textBlock = new TextBlock
                    {
                        Text = BaseOfTasks.TaskCollection[j],
                        MaxWidth = 1719,
                        MaxHeight = 340,
                        Height = double.NaN,
                        FontSize = 15,
                        TextWrapping = TextWrapping.Wrap,
                        Width = double.NaN,
                    };
                    var width = textBlock.Width;
                    WPExamples.Children.Add(textBlock);


                    if ((j == 0 || j == 1) && BaseOfTasks.Image[j].Count > 0)
                    {
                        var bit_image = new BitmapImage();
                        bit_image.BeginInit();
                        bit_image.UriSource = new Uri(image_source[0]);
                        bit_image.EndInit();
                        var image = new Image();
                        image.Width = bit_image.Width;
                        image.Height = 30;
                        image.Margin = new Thickness(10, 0, 0, 15);

                        image.Source = bit_image;

                        WPExamples.Children.Add(image);
                    }
                    else if (BaseOfTasks.Image[j].Count == 1)
                    {
                        var bit_image = new BitmapImage();
                        try
                        {

                            bit_image.BeginInit();
                            bit_image.UriSource = new Uri(image_source[0]);
                            bit_image.EndInit();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        if (bit_image.Height > 40) // Если изображение не является членом класса "tex"
                        {
                            var image = new Image();
                            image.Width = bit_image.Width;
                            image.Height = bit_image.Height;
                            image.Source = bit_image;
                            WPExamples.Children.Add(image);
                            var label1 = new Label();
                            label1.Width = 300;
                            label1.Height = 30;
                            WPExamples.Children.Add(label1);
                        }
                        else
                        {
                            var image = new Image();
                            image.Width = bit_image.Width;
                            image.Height = 30;
                            image.Margin = new Thickness(2, 0, 0, 5);
                            image.Source = bit_image;
                            WPExamples.Children.Add(image);
                            var label1 = new Label();
                            label1.Width = 1100;
                            label1.Height = 30;
                            WPExamples.Children.Add(label1);
                        }
                    }

                }
                //else
                //{
                //    string text = ParserReplacer.HTMLtoText(BaseOfTasks.TaskCollection[j], j);
                //    var words = text.Split(new string[] { "{img}" }, StringSplitOptions.None).ToList();
                //    var count = 0;
                //    var image = new List<string>();
                //    var tex_image = new List<string>();
                //    for (int i = 0; i < image_source.Count; i++)
                //    {
                //        var bit_image = new BitmapImage();
                //        bit_image.BeginInit();
                //        bit_image.UriSource = new Uri(image_source[i]);
                //        bit_image.EndInit();
                //        if (bit_image.Height > 40)
                //            image.Add(bit_image.ToString().Substring(8));
                //        else
                //            tex_image.Add(bit_image.ToString().Substring(8));

                //    }
                //    for (int i = 0; i < words.Count; i++)
                //    {
                //        var textBlock = new TextBlock
                //        {
                //            Text = $"{words[i]} ",
                //            Height = double.NaN,
                //            FontSize = 15,
                //            TextWrapping = TextWrapping.Wrap,
                //            Width = double.NaN,
                //        };
                //        WPExamples.Children.Add(textBlock);

                //        words[i].Replace(words[i], "");
                //            var bit_image = new BitmapImage();
                //            bit_image.BeginInit();
                //            bit_image.UriSource = new Uri(tex_image[count]);
                //            bit_image.EndInit();
                //            count++;

                //            if (bit_image.Height > 40) // Если изображение не является членом класса "tex"
                //            {
                //                var img = new Image();
                //                img.Width = bit_image.Width;
                //                img.Height = bit_image.Height;
                //                img.Source = bit_image;
                //                WPExamples.Children.Add(img);
                //            }
                //            else
                //            {
                //                var img = new Image();
                //                img.Width = bit_image.Width;
                //                img.Height = 30;
                //                img.Margin = new Thickness(2, 0, 0, 5);
                //                img.Source = bit_image;
                //                WPExamples.Children.Add(img);
                //            }
                //    }
                //    for (int i = 0; i < image.Count; i++)
                //    {
                //        var bit_image = new BitmapImage();
                //        bit_image.BeginInit();
                //        bit_image.UriSource = new Uri(image[i]);
                //        bit_image.EndInit();
                //        var img = new Image();
                //        img.MaxWidth = 230;
                //        img.MaxHeight = 210;
                //        img.Source = bit_image;
                //        WPExamples.Children.Add(img);
                //    }

                //    count = 0;
                //}

                var splitter = new Label();
                splitter.Height = 10;
                splitter.Width = 1719;
                WPExamples.Children.Add(splitter);

                var textBox = new TextBox
                {

                    Height = 24,
                    Width = 458

                };
                WPExamples.Children.Add(textBox);



                var EndLabel = new Label
                {
                    Height = 30,
                    Width = 1087
                };
                WPExamples.Children.Add(EndLabel);

                var border = new Border
                {
                    Height = 2,
                    Width = 1710,
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black,
                    RenderTransformOrigin = new Point(0.5, 0.5),
                    Margin = new Thickness(0, 10, 0, 10)
                };
                WPExamples.Children.Add(border);
                j++;
            }

            var ButtonAnswer = new Button
            {
                Width = 162,
                Content = "Ответить",
                Height = 28,
                Margin = new Thickness(750, 0, 0, 0)
            };
            ButtonAnswer.Click += ButtonAnswer_Click;
            WPExamples.Children.Add(ButtonAnswer);

            for (int i = 0; i < 5; i++)
            {
                var SubLabel = new Label
                {
                    Height = 30,
                    Width = 1087
                };
                WPExamples.Children.Add(SubLabel);
            }

        }

        public void ConstructorPhysVariant()
        {
            var j = 0;
            BaseOfTasks.ReadTaskImages();

            while (j < BaseOfTasks.TaskCollection.Count)
            {
                List<string> image_source = new List<string>();
                for (int i = 0; i < BaseOfTasks.Image[j].Count; i++) // путь для изображений определенного задания
                {

                    string s = $@"{Directory.GetCurrentDirectory()}/{BaseOfTasks.Subject}/Варианты/img_{BaseOfTasks.Variant}/{BaseOfTasks.Image[j][i]}";

                    image_source.Add(s);

                }

                Label label = new Label();
                label.Content = $"Задание №{j + 1}";
                label.FontWeight = FontWeights.Bold;
                label.Width = 1590;
                label.FontSize = 15;
                label.Height = 30;
                WPExamples.Children.Add(label);

                Button openCalc = new Button();
                openCalc.Content = "Open";
                openCalc.Height = 30;
                openCalc.Width = 20;
                openCalc.Click += B_OpenCalc;
                WPExamples.Children.Add(openCalc);

                Label upper_splitter = new Label();
                upper_splitter.Height = 10;
                upper_splitter.Width = 1729;
                WPExamples.Children.Add(upper_splitter);

                if (image_source.Count > 1 == false)
                {
                    TextBlock textBlock = new TextBlock
                    {
                        Text = BaseOfTasks.TaskCollection[j],
                        MaxWidth = 1719,
                        MaxHeight = 340,
                        Height = double.NaN,
                        FontSize = 15,
                        TextWrapping = TextWrapping.Wrap,
                        Width = double.NaN,
                    };
                    var width = textBlock.Width;
                    WPExamples.Children.Add(textBlock);


                    if ((j == 0 || j == 1) && BaseOfTasks.Image[j].Count > 0)
                    {
                        BitmapImage bit_image = new BitmapImage();
                        bit_image.BeginInit();
                        bit_image.UriSource = new Uri(image_source[0]);
                        bit_image.EndInit();
                        Image image = new Image();
                        image.Width = bit_image.Width;
                        image.Height = 30;
                        image.Margin = new Thickness(10, 0, 0, 15);

                        image.Source = bit_image;

                        WPExamples.Children.Add(image);
                    }
                    else if (BaseOfTasks.Image[j].Count == 1)
                    {
                        BitmapImage bit_image = new BitmapImage();
                        try
                        {

                            bit_image.BeginInit();
                            bit_image.UriSource = new Uri(image_source[0]);
                            bit_image.EndInit();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        if (bit_image.Height > 40) // Если изображение не является членом класса "tex"
                        {
                            Image image = new Image();
                            image.Width = bit_image.Width;
                            image.Height = bit_image.Height;
                            image.Source = bit_image;
                            WPExamples.Children.Add(image);
                            Label label1 = new Label();
                            label1.Width = 300;
                            label1.Height = 30;
                            WPExamples.Children.Add(label1);
                        }
                        else
                        {
                            Image image = new Image();
                            image.Width = bit_image.Width;
                            image.Height = 30;
                            image.Margin = new Thickness(2, 0, 0, 5);
                            image.Source = bit_image;
                            WPExamples.Children.Add(image);
                            Label label1 = new Label();
                            label1.Width = 1100;
                            label1.Height = 30;
                            WPExamples.Children.Add(label1);
                        }
                    }

                }
                //else
                //{

                //    List<string> words = text.Split(new string[] { "{img}" }, StringSplitOptions.None).ToList();
                //    var count = 0;
                //    List<string> image = new List<string>();
                //    List<string> tex_image = new List<string>();
                //    for (int i = 0; i < image_source.Count; i++)
                //    {
                //        BitmapImage bit_image = new BitmapImage();
                //        bit_image.BeginInit();
                //        bit_image.UriSource = new Uri(image_source[i]);
                //        bit_image.EndInit();
                //        if (bit_image.Height > 40)
                //            image.Add(bit_image.ToString().Substring(8));
                //        else
                //            tex_image.Add(bit_image.ToString().Substring(8));
                        
                //    }

                //    for (int i = 0; i < words.Count; i++)
                //    {
                //            TextBlock textBlock = new TextBlock
                //            {
                //                Text = $"{words[i]} ",
                //                Height = double.NaN,
                //                FontSize = 15,
                //                TextWrapping = TextWrapping.Wrap,
                //                Width = double.NaN,
                //            };
                //            WPExamples.Children.Add(textBlock);
                //        BitmapImage bit_image = new BitmapImage();
                //        bit_image.BeginInit();
                //        bit_image.UriSource = new Uri(tex_image[count]);
                //        bit_image.EndInit();
                //        count++;

                //        if (bit_image.Height > 40) // Если изображение не является членом класса "tex"
                //        {
                //            Image img = new Image();
                //            img.Width = bit_image.Width;
                //            img.Height = bit_image.Height;
                //            img.Source = bit_image;
                //            WPExamples.Children.Add(img);
                //        }
                //        else
                //        {
                //            Image img = new Image();
                //            img.Width = bit_image.Width;
                //            img.Height = 30;
                //            img.Margin = new Thickness(2, 0, 0, 5);
                //            img.Source = bit_image;
                //            WPExamples.Children.Add(img);
                //        }
                //    }
                //    for (int i = 0; i < image.Count; i++)
                //    {
                //        BitmapImage bit_image = new BitmapImage();
                //        bit_image.BeginInit();
                //        bit_image.UriSource = new Uri(image[i]);
                //        bit_image.EndInit();
                //        Image img = new Image();
                //        img.MaxWidth = 230;
                //        img.MaxHeight = 210;
                //        img.Source = bit_image;
                //        WPExamples.Children.Add(img);
                //    }

                //    count = 0;
                //}

                Label splitter = new Label();
                splitter.Height = 10;
                splitter.Width = 1719;
                WPExamples.Children.Add(splitter);

                TextBox textBox = new TextBox
                {

                    Height = 24,
                    Width = 458

                };
                WPExamples.Children.Add(textBox);



                Label EndLabel = new Label
                {
                    Height = 30,
                    Width = 1087
                };
                WPExamples.Children.Add(EndLabel);

                Border border = new Border
                {
                    Height = 2,
                    Width = 1710,
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black,
                    RenderTransformOrigin = new Point(0.5, 0.5),
                    Margin = new Thickness(0, 10, 0, 10)
                };
                WPExamples.Children.Add(border);
                j++;
            }

            Button ButtonAnswer = new Button
            {
                Width = 162,
                Content = "Ответить",
                Height = 28,
                Margin = new Thickness(750, 0, 0, 0)
            };
            ButtonAnswer.Click += ButtonAnswer_Click;
            WPExamples.Children.Add(ButtonAnswer);

            for (int i = 0; i < 5; i++)
            {
                Label SubLabel = new Label
                {
                    Height = 30,
                    Width = 1087
                };
                WPExamples.Children.Add(SubLabel);
            }
        }
        private void B_OpenCalc(object sender, RoutedEventArgs e)
        {
            PhysCalc calc = new PhysCalc();
            calc.Show();
        }
        public void ConstructorSpecialMath()
        {
            
        }
        public void ConstructorInfVariant()
        {
            var j = 0;

            while (j < BaseOfTasks.TaskCollection.Count)
            {

                var label = new Label();
                label.Content = $"Задание №{j + 1}";
                label.FontWeight = FontWeights.Bold;
                label.Width = 1712;
                label.Height = 30;
                WPExamples.Children.Add(label);

                TextBlock textBlock = new TextBlock
                {
                    Text = BaseOfTasks.TaskCollection[j],
                    Width = double.NaN,
                    Height = double.NaN,
                    TextWrapping = TextWrapping.Wrap,
                };

                WPExamples.Children.Add(textBlock);

                Label splitter = new Label();
                splitter.Height = 10;
                splitter.Width = 1719;
                WPExamples.Children.Add(splitter);

                if (j < 25)
                {
                    TextBox textBox = new TextBox
                    {
                        Height = 24,
                        Width = 458
                    };
                    WPExamples.Children.Add(textBox);
                }

                Label EndLabel = new Label
                {
                    Height = 30,
                    Width = 1087
                };
                WPExamples.Children.Add(EndLabel);

                Border border = new Border
                {
                    Height = 2,
                    Width = 1710,
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black,
                    RenderTransformOrigin = new Point(0.5, 0.5),
                    Margin = new Thickness(0, 10, 0, 10)
                };
                WPExamples.Children.Add(border);
                j++;
            }

            Button ButtonAnswer = new Button
            {
                Width = 162,
                Content = "Ответить",
                Height = 28,
                Margin = new Thickness(750, 0, 0, 0)
            };
            ButtonAnswer.Click += ButtonAnswer_Click;
            WPExamples.Children.Add(ButtonAnswer);

            for (int i = 0; i < 5; i++)
            {
                Label SubLabel = new Label
                {
                    Height = 30,
                    Width = 1087
                };
                WPExamples.Children.Add(SubLabel);
            }
        }
        #endregion

    }
}
