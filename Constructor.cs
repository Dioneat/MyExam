using SharpVectors.Converters;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyExamApp.Pages.Subjects
{
    class Constructor
    {
        public static Label AddLabel(double width, double height, string content, FontWeight weight, int fontSize)
        {
            return new Label
            {
                Content = content,
                FontWeight = weight,
                Width = width,
                Height = height,
                FontSize = fontSize,
            }; 
        }
        public static Label AddLabel(double width, double height, string content, Brush brush)
        {
            return new Label
            {
                Content = content,
                FontWeight = FontWeights.Bold,
                Width = width,
                Height = height,
                FontSize = 15,
                Background = brush
            };
        }
        public static Image AddImagePNG(double width, double height, Thickness margin, BitmapImage image)
        {
            return new Image
            {
                Width = width,
                Height = height,
                Margin = margin,
                Source = image
            };
        }
        public static SvgViewbox AddImageSVG(double[] sizes, Thickness margin, string path)
        {
            return new SvgViewbox
            {
                Width = sizes[1],
                Height = sizes[0],
                Source = new Uri(path),
                Margin = margin,
                
            }; 
        }
        public static Expander AddExpander(string text)
        {
            return new Expander
            {
                MaxWidth = 1700,
                Width = double.NaN,
                Height = double.NaN,
                Header = "Текст",
                Content = new TextBlock
                {
                    Width = double.NaN,
                    TextWrapping = TextWrapping.Wrap,
                    Text = text,
                    FontStyle = FontStyles.Italic,
                    FontSize = 13
                    
                }
            };
        }
        public static Border AddBorder()
        {
            return new Border 
            {
                Height = 2,
                Width = 1710,
                BorderBrush = new BrushConverter().ConvertFrom("#006FFD") as SolidColorBrush,
                BorderThickness = new Thickness(1),
                RenderTransformOrigin = new Point(0.5, 0.5),
                Margin = new Thickness(0, 10, 0, 10),
                CornerRadius = new CornerRadius(10)
            };
        }
        public static Button AddButton(double height, double width, string content, Thickness margin)
        {
            return new Button
            {
                Height = height,
                Width = width,
                Content = content,
                Margin = margin
            };
        }
        public static TextBlock AddTextBlock(double height, double width, string text, FontVariants fontVariant, FontWeight weight, FontStyle style)
        {
            var tBlock = new TextBlock
            {
                Height = height,
                Width = width,
                Text = text.Replace("/b", ""),
                FontSize = 15,
                TextWrapping = TextWrapping.Wrap,
                FontStyle = style,
                FontWeight = weight,
            };
            tBlock.Typography.Variants = fontVariant;
            return tBlock;
        }
        public static TextBlock AddTextBlock(double height, double width, string text, bool addHyperlink)
        {
            var tBlock = new TextBlock
            {
                Height = height,
                Width = width,
                FontSize = 15
            };
            return tBlock;
        }



        public static TextBox AddTextBox(double height, double width, int maxLength)
        {
            return new TextBox
            {
                Height = height,
                Width = width,
                MaxLength = maxLength
            };
        }
   
        public static ComboBox AddComboBox(double height, double width, int[] scores)
        {
            return new ComboBox
            {
                Height = height, 
                Width = width,
                ItemsSource = scores
            };
        }
    }
}
