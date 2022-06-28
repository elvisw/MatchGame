using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly DispatcherTimer _timer = new DispatcherTimer();
        int _tenthsOfSecondsElapsed;
        int _matchesFound;
        public MainWindow()
        {
            InitializeComponent();

            _timer.Interval = TimeSpan.FromSeconds(.1);
            _timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            _tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (_tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (_matchesFound == 8)
            {
                _timer.Stop();
                timeTextBlock.Text += " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> anamalEmoji = new List<string>()
            {
                "🐙", "🐙",
                "🐡", "🐡",
                "🐘", "🐘",
                "🐳", "🐳",
                "🐪", "🐪",
                "🦕", "🦕",
                "🦘", "🦘",
                "🦔", "🦔",
            };
            Random random = new Random();
            foreach (TextBlock textBlock in mainGird.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(anamalEmoji.Count);
                    string nextEmoji = anamalEmoji[index];
                    textBlock.Text = nextEmoji;
                    anamalEmoji.RemoveAt(index);
                }
            }
            _timer.Start();
            _tenthsOfSecondsElapsed = 0;
            _matchesFound = 0;
        }

        TextBlock _lastTextBlockClicked;
        bool _findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock? textBlock = e.Source as TextBlock;
            if (_findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                _lastTextBlockClicked = textBlock;
                _findingMatch = true;
            }
            else if (textBlock.Text == _lastTextBlockClicked.Text)
            {
                _matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                _findingMatch = false;
            }
            else
            {
                _lastTextBlockClicked.Visibility = Visibility.Visible;
                _findingMatch = false;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
