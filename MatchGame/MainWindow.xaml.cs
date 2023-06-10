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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private int tenthsOfSecondsElapsed;
        private int matchesFound;
        
        
        private TextBlock lastTextBlockClicked;
        private bool findingMatch = false;
        
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            tbTimer.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 10)
            {
                timer.Stop();
                tbTimer.Text = tbTimer.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<String> emoji = new List<String>()
            {
                "😊", "😂", "🤣", "😎", "❤️", "😍", "😒", "👌", "😘", "💕",
                "😊", "😂", "🤣", "😎", "❤️", "😍", "😒", "👌", "😘", "💕",
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name == "tbTimer")
                    continue;

                textBlock.Visibility = Visibility.Visible;
                
                int index = random.Next(emoji.Count);
                string nextEmoji = emoji[index];
                textBlock.Text = nextEmoji;
                emoji.RemoveAt(index);
            }

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (!findingMatch)
            {
                findingMatch = true;
                lastTextBlockClicked = textBlock;
                textBlock.Visibility = Visibility.Hidden;
            }
            else if (!textBlock.Text.Equals(lastTextBlockClicked.Text))
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
            else
            {
                findingMatch = false;
                textBlock.Visibility = Visibility.Hidden;
                matchesFound++;
            }
        }

        private void tbTimer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 10)
            {
                SetUpGame();
            }
        }
    }
}
