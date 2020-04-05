using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;

namespace MyExamApp.Pages.Subjects
{
    class Russian : Constructor
    {
        public static ushort[] Time = { 3, 29 }; 
        public static WrapPanel CreateVariant()
        {
            var panel = new WrapPanel();
            for (int i = 0; i < BaseOfTasks.TaskCollection.Count; i++)
            {
                panel.Children.Add(AddLabel(double.NaN, 30, $"Задание", FontWeights.Bold, 15));
                panel.Children.Add(new TextBlock(new Hyperlink(new Run($"#{i + 1}"))) { Height = 30, Width = double.NaN, FontSize = 15, Padding = new Thickness(0, 5, 0,0) }); // Заголовок варианта
                panel.Children.Add(AddTaskText(BaseOfTasks.TaskCollection[i], i));
                panel.Children.Add(AddLabel(1790, 30, "", FontWeights.Normal, 15));
                if(i < 26)
                {
                    panel.Children.Add(AddTextBox(25, 458, 15));
                    panel.Children.Add(AddLabel(1790, 30, "", FontWeights.Normal, 15));
                }
                    
                panel.Children.Add(AddBorder());
               
            }
            return panel;
        }
        public static WrapPanel ExpandendPart()
        {
            var critery = new List<string> 
            {
                "Формулировка проблем исходного текста", "Комментарий к сформулированной проблеме исходного текста",
                "Отражение позиции автора исходного текста", "Отношение к позиции автора по проблеме исходного текста",
                "Смысловая цельность, речевая связность и последовательность изложения", "Точность и выразительность речи",
                "Соблюдение орфографических норм", "Соблюдение пунктуационных норм", "Соблюдение языковых норм", "Соблюдение речевых норм",
                "Соблюдение этических норм", "Соблюдение фактологической точности в фоновом материале"
            };
            var points = new List<int[]>
            {
                new int[]{ 0, 1 },new int[] {0, 1, 2, 3, 4, 5 }, new int[] {0,1 },
                new int[] {0, 1 }, new int[] {0, 1, 2 }, new int[] {0, 1, 2 },
                new int[] {0, 1, 2, 3 }, new int[] {0, 1, 2, 3 }, new int[] {0, 1, 2 },
                new int[] {0, 1, 2 }, new int[] {0, 1 }, new int[] {0, 1 }
            };
            var panel = new WrapPanel
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 415,
                Width = 1060,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(375, 0, 0, 0)
            };
            panel.Children.Add(AddLabel(165, 30, "", new BrushConverter().ConvertFrom("#bac2ff") as Brush));
            panel.Children.Add(AddLabel(10, 30, "", FontWeights.Normal, 1));
            panel.Children.Add(AddLabel(670, 30, "Критерий", new BrushConverter().ConvertFrom("#bac2ff") as Brush));
            panel.Children.Add(AddLabel(10, 30, "", FontWeights.Normal, 1));
            panel.Children.Add(AddLabel(175, 30, "Баллы", new BrushConverter().ConvertFrom("#bac2ff") as Brush));
            for (int i = 0; i < 12; i++)
            {
                panel.Children.Add(AddLabel(165, 30, $"K{i+1}", FontWeights.Bold, 15));
                panel.Children.Add(AddLabel(675, 30, critery[i], FontWeights.Normal, 15));
                panel.Children.Add(AddLabel(125, 30, "Ваша оценка:", FontWeights.Normal, 15));
                panel.Children.Add(AddComboBox(25, 45, points[i]));
                
            }
            return panel;
        }
        private static WrapPanel AddTaskText(string text, int i)
        {
            var panel = new WrapPanel();
            bool HasProbText = false;
            string task;
            string probtext = "";
            if (text.Contains("<probtext>"))
            {
                task = text.Split(new string[] { "<probtext>" }, System.StringSplitOptions.None)[0];
                probtext = text.Split(new string[] { "<probtext>" }, System.StringSplitOptions.None)[1];
                HasProbText = true;
            }
            else
                task = text;
            if (task.Contains("<") || task.Contains(">"))
            {
                string[] words = task.Split(new char[] { '<', '>' });

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
                    else if(words[j] is "table")
                    {
                        var table = BaseOfTasks.TaskCollection[i].Split(new string[] { "<table>", "</table>" }, System.StringSplitOptions.None)[1].Split(new char[] {'<', '>' });
                        for (int c = 0; c < table.Length; c++)
                        {
                            if(table[c] is "tr")
                            {
                                
                            }
                        }
                    }
                    else if (words[j] is "/new")
                        panel.Children.Add(AddLabel(1790, 20, "", FontWeights.Normal, 10));
                    else if (words[j] is "/new/new")
                        panel.Children.Add(AddLabel(1790, 40, "", FontWeights.Normal, 10));
                    else if (words[j].Contains("i") is false && words[j].Contains("sub") is false && words[j].Contains("sup") is false)
                    {
                        panel.Children.Add(AddTextBlock(double.NaN, double.NaN, words[j], FontVariants.Normal, FontWeights.Normal, FontStyles.Normal));
                    }

                }
            }
            else
                panel.Children.Add(AddTextBlock(double.NaN, double.NaN, task, FontVariants.Normal, FontWeights.Normal, FontStyles.Normal));
            if (HasProbText is true && probtext.Length > 2)
            {
                panel.Children.Add(AddExpander(probtext));
                HasProbText = false;
            }
                
            return panel;
        }
    }
}
