using MyExamApp.Pages.Other;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;

namespace MyExamApp.Pages.Subjects
{
    public partial class CustVariant : Page
    {

        private ushort _hours;
        private ushort _minute;
        private ushort _second = 59;

        private ushort _hoursPassed;
        private ushort _minutePassed;
        private ushort _secondPassed = 1;
        public CustVariant(WrapPanel wrapPanel, ushort hours, ushort minute)
        {
            _hours = hours;
            _minute = minute;
            InitializeComponent();
            WrapVariant.Children.Add(wrapPanel);
            var button = Constructor.AddButton(28, 162, "Ответить", new Thickness(750, 0, 0, 0));
            var wrap = WrapVariant.Children[5] as WrapPanel;
            var panel = wrap.Children[0] as WrapPanel;
            foreach (TextBlock text in panel.Children.OfType<TextBlock>())
            {
                foreach (Hyperlink hyp in text.Inlines.OfType<Hyperlink>())
                {
                    hyp.Click += Hyp_Click;
                }
            }
            button.Click += ButtonAnswer_Click;
            button.Style = FindResource("butStyle") as Style;
            WrapVariant.Children.Add(button);
            StartClock();
        }
        private void StartClock()
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += Dt_Tick;
            dt.Start();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            if(_hours is 0 && _minute is 0 && _second is 0)
                NavigationService.Navigate(new AnswerPage());
            else
            {
                string minute;
                string second;

                string minutePassed;
                string secondPassed;
                _second--;
                _secondPassed++;
                if (_second is 0)
                {
                    _second = 59;
                    _minute--;
                }
                if (_minute is 0)
                {
                    _minute = 59;
                    _hours--;
                }
                if (_secondPassed > 59)
                {
                    _secondPassed = 1;
                    _minutePassed++;
                }
                if (_minutePassed > 59)
                {
                    _minutePassed = 0;
                    _hoursPassed++;
                }
                if (_second < 10)
                    second = $"0{_second}";
                else
                    second = _second.ToString();

                if (_minute < 10)
                    minute = $"0{_minute}";
                else
                    minute = _minute.ToString();
                if (_secondPassed < 10)
                    secondPassed = $"0{_secondPassed}";
                else
                    secondPassed = _secondPassed.ToString();
                if (_minutePassed < 10)
                    minutePassed = $"0{_minutePassed}";
                else
                    minutePassed = _minutePassed.ToString();
                TimeLeft.Text = $"{_hours}:{minute}:{second}";
                TimePassed.Text = $"{_hoursPassed}:{minutePassed}:{secondPassed}";
            }
                
        }
        private void Hyp_Click(object sender, RoutedEventArgs e)
        {
            var a = e.Source as Hyperlink;
            var b = a.Inlines.FirstInline as Run;
            var c = int.Parse(b.Text.Replace("#", ""));
            NavigationService.Navigate(new ViewTask(c-1));
            
        }

        private void ButtonAnswer_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что готовы перейти к ответам?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result is MessageBoxResult.Yes)
            {
                var wrap = WrapVariant.Children[5] as WrapPanel;
                var panel = wrap.Children[0] as WrapPanel;
                foreach (TextBox textBox in panel.Children.OfType<TextBox>())
                {
                    BaseOfTasks.UserAnswer.Add(textBox.Text.ToLower());
                }
                NavigationService.Navigate(new ExpandendPart());
            }
        }
    }
}
