using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MyExamApp.Pages.Subjects
{
    class MathSpec : Constructor
    {
        public static ushort[] Time = {3, 54 };
        public static WrapPanel CreateVariant()
        {
            
            BaseOfTasks.isVariantOpen = true;
            var panel = new WrapPanel();
            
            
            for (int i = 0; i < BaseOfTasks.TaskCollection.Count; i++)
            {
                var images = LoadImages(i, BaseOfTasks.Image, "img");
                panel.Children.Add(AddLabel(double.NaN, 30, $"Задание", FontWeights.Bold, 15));
                panel.Children.Add(new TextBlock(new Hyperlink(new Run($"#{i + 1}"))) { Height = 30, Width = double.NaN, FontSize = 15, Padding = new Thickness(0, 5, 0, 0)}); // Заголовок варианта
                panel.Children.Add(AddTaskText(images, BaseOfTasks.Image, i, BaseOfTasks.TaskCollection[i], "img"));
                panel.Children.Add(AddLabel(1407, 10, "", FontWeights.Bold, 1));
                if(i < 12)
                {
                    panel.Children.Add(AddTextBox(24, 458, 15));
                    panel.Children.Add(AddLabel(1087, 30, "", FontWeights.Bold, 1));
                }

                panel.Children.Add(AddBorder());

            }

            return panel;
        }

        public static WrapPanel CreateExpPart()
        {
            var points = new List<int[]>
            {
                new int[] {0, 1, 2 }, new int[] { 0, 1, 2}, new int[] {0, 1, 2 },
                new int[] { 0, 1, 2, 3}, new int[] {0, 1, 2, 3 },
                new int[] {0, 1, 2, 3, 4 },  new int[] { 0, 1, 2, 3, 4}
            };
            var panel = new WrapPanel();
            var count = 0;
            for (int i = BaseOfTasks.CorrectAnswer.Count; i < BaseOfTasks.Description.Count; i++)
            {
                var images = LoadImages(i, BaseOfTasks.DescriptionImage, "desc_img");
                panel.Children.Add(AddLabel(1790, 30, $"Задание №{i + 1}", FontWeights.Bold, 15));

                panel.Children.Add(AddTaskText(images, BaseOfTasks.DescriptionImage, i, BaseOfTasks.Description[i], "desc_img"));
                panel.Children.Add(AddLabel(1907, 10, "", FontWeights.Bold, 1));
                panel.Children.Add(AddLabel(double.NaN, 30, "Ваша оценка (баллов):", FontWeights.Normal, 15));
                panel.Children.Add(AddComboBox(25, 45, points[count]));
                panel.Children.Add(AddBorder());
                count++;
            }
            
            return panel;
        }
        public static WrapPanel CreateAnswerPage()
        {
            var panel = new WrapPanel();
            for (int i = BaseOfTasks.CorrectAnswer.Count; i < BaseOfTasks.Description.Count; i++)
            {
                var images = LoadImages(i, BaseOfTasks.DescriptionImage, "desc_img");
                panel.Children.Add(AddLabel(1790, 30, $"Задание №{i + 1}", FontWeights.Bold, 15));
                panel.Children.Add(AddTaskText(images, BaseOfTasks.DescriptionImage, i, BaseOfTasks.Description[i], "desc_img"));
                panel.Children.Add(AddLabel(1907, 10, "", FontWeights.Bold, 1));
                panel.Children.Add(AddBorder());
            }
            return panel;

        }
        private static ArrayList LoadImages(int i, List<List<string>> TaskImages, string type)
        {
            var images = new ArrayList();

            for (int j = 0; j < TaskImages[i].Count; j++)
            {
                var png_image = new List<BitmapImage>();
                var svg_image = new Dictionary<string, double[]>();
                var path = $@"{Directory.GetCurrentDirectory()}/{BaseOfTasks.Subject}/Варианты/{type}_{BaseOfTasks.Variant}/{TaskImages[i][j]}";

                if (path.EndsWith(".svg"))
                {
                    svg_image.Add(path, BaseOfTasks.ReadImageSize(path));
                    images.Add(svg_image);
                }

                else
                {
                    png_image.Add(new BitmapImage(new Uri(path)));
                    images.Add(png_image);
                }
            }
            return images;
        }
        private static WrapPanel AddTaskText(ArrayList images, List<List<string>> TaskImages, int i, string text, string type)
        {
            var panel = new WrapPanel();
            bool isBigImage = false;
            if (images.Count > 1)
            {
                string[] words = text.Split(new char[] { '<', '>' });
                int indexOfImage = 0;
                for (int j = 0; j < words.Length; j++)
                {
                    if (words[j] is "i" || words[j] is "b" || words[j] is "sup" || words[j] is "sub")
                    {
                        switch (words[j])
                        {
                            case "i":
                                panel.Children.Add(AddTextBlock(double.NaN, double.NaN, words[j + 1], FontVariants.Normal, FontWeights.Normal, FontStyles.Italic));
                                break;
                            case "b":
                                panel.Children.Add(AddTextBlock(double.NaN, double.NaN, words[j + 1], FontVariants.Normal, FontWeights.Bold, FontStyles.Normal));
                                break;
                            case "sup":
                                panel.Children.Add(AddTextBlock(double.NaN, double.NaN, words[j + 1], FontVariants.Superscript, FontWeights.Normal, FontStyles.Normal));
                                break;
                            case "sub":
                                panel.Children.Add(AddTextBlock(double.NaN, double.NaN, words[j + 1], FontVariants.Subscript, FontWeights.Normal, FontStyles.Normal));
                                break;
                        }
                        words[j + 1] = "";
                        words[j + 2] = "";
                    
                    }
                    else if(words[j] is "/new")
                        panel.Children.Add(AddLabel(1790, 20, "", FontWeights.Normal, 10));
                    else if(words[j] is "/new/new")
                        panel.Children.Add(AddLabel(1790, 40, "", FontWeights.Normal, 10));
                    else if (words[j].Contains("i") is false && words[j].Contains("sub") is false && words[j].Contains("sup") is false)
                    {
                        panel.Children.Add(AddTextBlock(double.NaN, double.NaN, words[j], FontVariants.Normal, FontWeights.Normal, FontStyles.Normal));
                    }
                    if (words[j].Contains("img") && indexOfImage < images.Count)
                    {
                        if (images[indexOfImage].GetType() == typeof(Dictionary<string, double[]>))
                        {
                            var image = images[indexOfImage] as Dictionary<string, double[]>;
                            var path = $"{Directory.GetCurrentDirectory()}/{BaseOfTasks.Subject}/Варианты/{type}_{BaseOfTasks.Variant}/{TaskImages[i][indexOfImage]}";
                            if (image[path][0] < 45)
                                panel.Children.Add(AddImageSVG(image[path], new Thickness(0, 0, 0, 0), path));
                            else
                                isBigImage = true;

                        }
                        else
                        {
                            var image = images[indexOfImage] as List<BitmapImage>;
                            if (image[0].Height < 45)
                                panel.Children.Add(AddImagePNG(image[0].Width, image[0].Height, new Thickness(0, 0, 0, 0), image[0]));
                            else
                                isBigImage = true;
                        }
                        indexOfImage++;
                    }
                }
                if (isBigImage is true)
                {
                    if (images[0].GetType() == typeof(Dictionary<string, double[]>))
                    {
                        var path = $"{Directory.GetCurrentDirectory()}/{BaseOfTasks.Subject}/Варианты/{type}_{BaseOfTasks.Variant}/{TaskImages[i][0]}";
                        var image = images[0] as Dictionary<string, double[]>;
                        panel.Children.Add(AddImageSVG(image[path], new Thickness(0, 0, 0, 30), path));
                    }
                    else
                    {
                        var image = images[0] as List<BitmapImage>;
                        panel.Children.Add(AddImagePNG(image[0].Width, image[0].Height, new Thickness(0, 0, 0, 0), image[0]));
                    }
                    isBigImage = false;
                }

            }
            else if (images.Count is 1)
            {
                string[] words = text.Split(new char[] { '<', '>' });

                for (int j = 0; j < words.Length; j++)
                {
                    if (words[j] is "i" || words[j] is "b" || words[j] is "sup" || words[j] is "sub")
                    {
                        switch (words[j])
                        {
                            case "i":
                                panel.Children.Add(AddTextBlock(double.NaN, double.NaN, words[j + 1], FontVariants.Normal, FontWeights.Normal, FontStyles.Italic));
                                break;
                            case "b":
                                panel.Children.Add(AddTextBlock(double.NaN, double.NaN, words[j + 1], FontVariants.Normal, FontWeights.Bold, FontStyles.Normal));
                                break;
                            case "sup":
                                panel.Children.Add(AddTextBlock(double.NaN, double.NaN, words[j + 1], FontVariants.Superscript, FontWeights.Normal, FontStyles.Normal));
                                break;
                            case "sub":
                                panel.Children.Add(AddTextBlock(double.NaN, double.NaN, words[j + 1], FontVariants.Subscript, FontWeights.Normal, FontStyles.Normal));
                                break;
                            case "/new":
                                panel.Children.Add(AddLabel(1790, 20, "", FontWeights.Normal, 10));
                                break;
                            case "/new/new":
                                panel.Children.Add(AddLabel(1790, 40, "", FontWeights.Normal, 10));
                                break;
                        }
                        words[j + 1] = "";
                        words[j + 2] = "";
                    }
                    else if (words[j] is "/new")
                        panel.Children.Add(AddLabel(1790, 20, "", FontWeights.Normal, 10));
                    else if (words[j] is "/new/new")
                        panel.Children.Add(AddLabel(1790, 40, "", FontWeights.Normal, 10));
                    else if (words[j].Contains("i") is false && words[j].Contains("sub") is false && words[j].Contains("sup") is false)
                    {
                        panel.Children.Add(AddTextBlock(double.NaN, double.NaN, words[j], FontVariants.Normal, FontWeights.Normal, FontStyles.Normal));
                    }
                    if (words[j].Contains("img"))
                    {
                        if (images[0].GetType() == typeof(Dictionary<string, double[]>))
                        {
                            var path = $"{Directory.GetCurrentDirectory()}/{BaseOfTasks.Subject}/Варианты/{type}_{BaseOfTasks.Variant}/{TaskImages[i][0]}";
                            var image = images[0] as Dictionary<string, double[]>;
                            if (image[path][0] < 45)
                                panel.Children.Add(AddImageSVG(image[path], new Thickness(0, 0, 0, 0), path));
                            else
                                isBigImage = true;
                        }
                        else
                        {
                            var image = images[0] as List<BitmapImage>;
                            if (image[0].Height < 45)
                                panel.Children.Add(AddImagePNG(image[0].Width, image[0].Height, new Thickness(0, 0, 0, 0), image[0]));
                            else
                                isBigImage = true;
                        }
                    }
                }
                if (isBigImage is true)
                {
                    if (images[0].GetType() == typeof(Dictionary<string, double[]>))
                    {
                        var path = $"{Directory.GetCurrentDirectory()}/{BaseOfTasks.Subject}/Варианты/{type}_{BaseOfTasks.Variant}/{TaskImages[i][0]}";
                        var image = images[0] as Dictionary<string, double[]>;
                        panel.Children.Add(AddImageSVG(image[path], new Thickness(0, 0, 0, 0), path));
                    }
                    else
                    {
                        var image = images[0] as List<BitmapImage>;
                        panel.Children.Add(AddImagePNG(image[0].Width, image[0].Height, new Thickness(0, 0, 0, 0), image[0]));
                    }
                    isBigImage = false;
                }
            }
            else
            {
                string[] words = text.Split(new char[] { '<', '>' });

                for (int j = 0; j < words.Length; j++)
                {
                    if (words[j] is "i" || words[j] is "b" || words[j] is "sup" || words[j] is "sub")
                    {
                        switch (words[j])
                        {
                            case "i":
                                panel.Children.Add(AddTextBlock(double.NaN, double.NaN, words[j + 1], FontVariants.Normal, FontWeights.Normal, FontStyles.Italic));
                                break;
                            case "b":
                                panel.Children.Add(AddTextBlock(double.NaN, double.NaN, words[j + 1], FontVariants.Normal, FontWeights.Bold, FontStyles.Normal));
                                break;
                            case "sup":
                                panel.Children.Add(AddTextBlock(double.NaN, double.NaN, words[j + 1], FontVariants.Superscript, FontWeights.Normal, FontStyles.Normal));
                                break;
                            case "sub":
                                panel.Children.Add(AddTextBlock(double.NaN, double.NaN, words[j + 1], FontVariants.Subscript, FontWeights.Normal, FontStyles.Normal));
                                break;
                        }
                        words[j + 1] = "";
                        words[j + 2] = "";
                    }
                    else if (words[j] is "/new")
                        panel.Children.Add(AddLabel(1790, 20, "", FontWeights.Normal, 10));
                    else if (words[j] is "/new/new")
                        panel.Children.Add(AddLabel(1790, 40, "", FontWeights.Normal, 10));
                    else if (words[j].Contains("i") is false && words[j].Contains("sub") is false && words[j].Contains("sup") is false)
                        panel.Children.Add(AddTextBlock(double.NaN, double.NaN, words[j], FontVariants.Normal, FontWeights.Normal, FontStyles.Normal));

                }
            }
            return panel;
        }
    }
}
