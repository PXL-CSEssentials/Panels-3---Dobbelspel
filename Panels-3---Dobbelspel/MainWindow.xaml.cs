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
        Random _randomNumber = new Random();
        int _gainPlayer = 0;
        int _gainComputer = 0;
        int _end = 0;
        RadioButton _radioButtonSelected;
        DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
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
                    int player = _randomNumber.Next(1, 7);
                    int computer = _randomNumber.Next(1, 7);
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
                        gainPlayerTextBlock.Text = $"{++_gainPlayer} keer gewonnen";
                        winnerLabel.Background = new SolidColorBrush(Colors.Green);
                        winnerLabel.Content = "Jij wint";
                        gainImage.Source = new BitmapImage(new Uri(@"\Wijsvinger_links.jpg", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        gainComputerTextBlock.Text = $"{++_gainComputer} keer gewonnen";
                        winnerLabel.Background = new SolidColorBrush(Colors.Red);
                        winnerLabel.Content = "Computer wint";
                        gainImage.Source = new BitmapImage(new Uri(@"\Wijsvinger_rechts.jpg", UriKind.RelativeOrAbsolute));
                    }
                    CheckFinalScore();
                    break;
                case "Eindscore":
                    string winner = _gainPlayer > _gainComputer ? "speler" : "computer";
                    MessageBox.Show($"De winnaar is de {winner}!");
                    ResetGame();
                    break;
            }

        }

        private void CheckFinalScore()
        {
            if (_gainPlayer == _end || _gainComputer == _end)
            {
                playButton.Content = "Eindscore";
            }
        }

        private void ResetGame()
        {
            playButton.IsEnabled = false;
            _radioButtonSelected.IsChecked = false;
            infoLabel.Content = "";
            gainImage.Source = null;
        }

        private void StartGame()
        {
            playButton.Content = "Dobbelen";
            playButton.IsEnabled = true;
            _gainComputer = 0;
            _gainPlayer = 0;
            winnerLabel.Content = "";
            gainPlayerTextBlock.Text = "";
            gainComputerTextBlock.Text = "";
            playerTextBox.Clear();
            computerTextBox.Clear();
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            StartGame();
            _radioButtonSelected = (RadioButton)sender;
            if (_radioButtonSelected.Name == "fiveRadioButton")
            {
                _end = 5;
            }
            else if (_radioButtonSelected.Name == "tenRadioButton")
            {
                _end = 10;
            }
            if (_radioButtonSelected.Name == "fifteenRadioButton")
            {
                _end = 15;
            }
            infoLabel.Content = Title = $"Eerste van {_end} wint";
        }
    }
}
