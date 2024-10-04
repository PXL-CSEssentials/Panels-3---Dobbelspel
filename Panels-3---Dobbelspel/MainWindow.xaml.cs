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
using System.Windows.Threading;

namespace Panels_3___Dobbelspel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random RandomNumber = new Random();
        int gainPlayer = 0;
        int gainComputer = 0;
        int End = 0;
        RadioButton RadioButtonSelected;
        DispatcherTimer Timer;

        public MainWindow()
        {
            InitializeComponent();

            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLabel.Content = $"{DateTime.Today.ToLongDateString()} {DateTime.Now.ToLongTimeString()}";
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            Button sndr = (Button)sender;
            switch (sndr.Content)
            {
                case "Dobbelen":
                    int player = RandomNumber.Next(1, 7);
                    int computer = RandomNumber.Next(1, 7);
                    playerTextBox.Text += player + "\r\n";
                    computerTextBox.Text += computer + "\r\n";
                    if (player == computer)
                    {
                        winnerLabel.Background = new SolidColorBrush(Colors.Blue);
                        winnerLabel.Content = "Gelijkspel";
                        gainImage.Source = new BitmapImage(new Uri(@"\Vuist.png", UriKind.RelativeOrAbsolute));
                    }
                    else if (player > computer)
                    {
                        gainPlayerTextBlock.Text = $"{++gainPlayer} keer gewonnen";
                        winnerLabel.Background = new SolidColorBrush(Colors.Green);
                        winnerLabel.Content = "Jij wint";
                        gainImage.Source = new BitmapImage(new Uri(@"\Wijsvinger_links.jpg", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        gainComputerTextBlock.Text = $"{++gainComputer} keer gewonnen";
                        winnerLabel.Background = new SolidColorBrush(Colors.Red);
                        winnerLabel.Content = "Computer wint";
                        gainImage.Source = new BitmapImage(new Uri(@"\Wijsvinger_rechts.jpg", UriKind.RelativeOrAbsolute));
                    }
                    CheckFinalScore();
                    break;
                case "Eindscore":
                    string winner = gainPlayer > gainComputer ? "speler" : "computer";
                    MessageBox.Show($"De winnaar is de {winner}!");
                    ResetGame();
                    break;
            }

        }

        private void CheckFinalScore()
        {
            if (gainPlayer == End || gainComputer == End)
            {
                playButton.Content = "Eindscore";
            }
        }

        private void ResetGame()
        {
            playButton.IsEnabled = false;
            RadioButtonSelected.IsChecked = false;
            infoLabel.Content = "";
            gainImage.Source = null;
        }

        private void StartGame()
        {
            playButton.Content = "Dobbelen";
            playButton.IsEnabled = true;
            gainComputer = 0;
            gainPlayer = 0;
            winnerLabel.Content = "";
            gainPlayerTextBlock.Text = "";
            gainComputerTextBlock.Text = "";
            playerTextBox.Clear();
            computerTextBox.Clear();
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            StartGame();
            RadioButtonSelected = (RadioButton)sender;
            if (RadioButtonSelected.Name == "fiveRadioButton")
            {
                End = 5;
            }
            else if (RadioButtonSelected.Name == "tenRadioButton")
            {
                End = 10;
            }
            if (RadioButtonSelected.Name == "fifteenRadioButton")
            {
                End = 15;
            }
            infoLabel.Content = Title = $"Eerste van {End} wint";
        }
    }
}
