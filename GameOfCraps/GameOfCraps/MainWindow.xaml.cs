using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameOfCraps
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int die1, die2, dieSum, point, curPlayerWins, curHouseWins;
        private double currentBet;
        public double bankBalance;
        private bool isFirstRoll, canPlay, gameStarted, betting;

        public MainWindow()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);

            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/GameOfCraps;component/Resources/goldButt.png"));
            rollBtn.Background = brush;

            curPlayerWins = 0;
            curHouseWins = 0;
            currentBet = 0;

            isFirstRoll = false;
            canPlay = false;
            gameStarted = true;
            betting = false;

            bettingGroupBox.Visibility = Visibility.Hidden;

            rollBtn.IsEnabled = false;
            playAgainBtn.IsEnabled = false;
            betBtn.IsEnabled = false;
            bankBtn.IsEnabled = false;
        }

        private int RollDice()
        {
            Random random = new Random();

            die1 = random.Next(1, 7);
            die2 = random.Next(1, 7);

            dieSum = die1 + die2;

            die1Roll.Text = die1.ToString();
            die2Roll.Text = die2.ToString();

            ShowDice(die1, die2);

            dieTotal.Text = dieSum.ToString();

            return dieSum;
        }

        private void PlayGame()
        {
            bankBtn.IsEnabled = false;
            gameStarted = false;

            int total = RollDice();

            if (isFirstRoll)
            {
                if (total == 7 || total == 11)
                    PlayerWins();
                else if (total == 2 || total == 3 || total == 12)
                    HouseWins();
                else
                {
                    isFirstRoll = false;
                    point = total;
                    pointTextBlock.Text = point.ToString();
                }
            }
            else
            {
                if (total == 7)
                    HouseWins();
                else if (total == point)
                    PlayerWins();
            }
        }

        private void ShowDice(int die1, int die2)
        {
            BitmapImage dieImage = new BitmapImage();

            dieImage.BeginInit();
            GetDieImage(die1, dieImage);
            dieImage.EndInit();
            this.die1pic.Source = dieImage;
            
            dieImage = new BitmapImage();

            dieImage.BeginInit();
            GetDieImage(die2, dieImage);
            dieImage.EndInit();
            this.die2pic.Source = dieImage;
        }

        private void GetDieImage(int die, BitmapImage dieImage)
        {
            switch (die)
            {
                case 1:
                    dieImage.UriSource = new Uri("pack://application:,,,/GameOfCraps;component/Resources/dice-1.png");
                    break;
                case 2:
                    dieImage.UriSource = new Uri("pack://application:,,,/GameOfCraps;component/Resources/dice-2.png");
                    break;
                case 3:
                    dieImage.UriSource = new Uri("pack://application:,,,/GameOfCraps;component/Resources/dice-3.png");
                    break;
                case 4:
                    dieImage.UriSource = new Uri("pack://application:,,,/GameOfCraps;component/Resources/dice-4.png");
                    break;
                case 5:
                    dieImage.UriSource = new Uri("pack://application:,,,/GameOfCraps;component/Resources/dice-5.png");
                    break;
                default:
                    dieImage.UriSource = new Uri("pack://application:,,,/GameOfCraps;component/Resources/dice-6.png");
                    break;
            }
        }

        private void PlayerWins()
        {
            if (betting)
            {
                bankBalance += currentBet;
                balance.Text = bankBalance.ToString();
            }

            rollBtn.IsEnabled = false;
            playAgainBtn.IsEnabled = true;
            canPlay = false;

            winner.Foreground = Brushes.Gold;
            winner.Text = "WINNER!";
            curPlayerWins++;
            playerWinTextBlock.Text = curPlayerWins.ToString();
        }

        private void HouseWins()
        {
            balance.Text = bankBalance.ToString();

            rollBtn.IsEnabled = false;

            playAgainBtn.IsEnabled = true;
            canPlay = false;
            winner.Foreground = Brushes.Red;
            winner.Text = "LOSER!";
            curHouseWins++;
            houseWinTextBlock.Text = curHouseWins.ToString();

            if (betting)
            {
                if ((bankBalance - currentBet) > 0)
                {
                    bankBalance -= currentBet;
                }
                else
                {
                    bankBalance = 0;
                    MessageBox.Show("Out of money! Restarting game.");
                    Reset();
                }
            }
        }

        public void Reset()
        {
            bettingGroupBox.Visibility = Visibility.Hidden;
            betting = false;

            curHouseWins = 0;
            curPlayerWins = 0;
            isFirstRoll = false;
            canPlay = false;
            gameStarted = true;

            rollBtn.IsEnabled = false;
            playAgainBtn.IsEnabled = false;

            winner.Text = "";
            die1Roll.Text = "";
            die2Roll.Text = "";
            dieTotal.Text = "";

            playerWinTextBlock.Text = "";
            houseWinTextBlock.Text = "";
        }

        private void bankBtn_Click(object sender, RoutedEventArgs e)
        {
            betting = true;

            bettingGroupBox.Visibility = Visibility.Visible;

            bankBtn.IsEnabled = false;
            rollBtn.IsEnabled = false;
            InputBet inputBet = new InputBet(this);
            inputBet.Show();

            betBtn.IsEnabled = true;
        }

        private void rulesClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("A player rolls two dice where each die has six faces in the usual way (values 1 through 6)."
                          + "\nAfter the dice have come to rest the sum of the two upward faces is calculated."
                          + "\nThe first roll"
                          + "\n\tIf the sum is 7 or 11 on the first throw the roller / player wins."
                          + "\n\tIf the sum is 2, 3 or 12 the roller/ player loses, that is the house wins."
                          + "\n\tIf the sum is 4, 5, 6, 8, 9, or 10, that sum becomes the roller / player's \"point\"."
                          + "\nContinue given the player's point"
                          + "\n\tNow the player must roll the \"point\" total before rolling a 7 in order to win."
                          + "\n\tIf they roll a 7 before rolling the point value they got on the first roll the roller / player loses(the 'house' wins).");
        }

        private void betBtn_Click(object sender, RoutedEventArgs e)
        {
            betting = true;
            double number;

            if (Double.TryParse(balance.Text, out number))
                bankBalance = number;

            double bet;

            if (Double.TryParse(betTextBox.Text, out bet))
            {
                if(bet <= bankBalance && bet > 0)
                {
                    if (canPlay)
                        rollBtn.IsEnabled = true;

                    betBtn.IsEnabled = false;
                    bankBalance = bankBalance - bet;
                    currentBet = bet + bet;
                    balance.Text = bankBalance.ToString();
                }
                else
                {
                    MessageBox.Show("Invalid bet. Try Again");
                }
            }
            else
                MessageBox.Show("Invalid bet. Try again");
        }

        private void exitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.R && canPlay)
                PlayGame();

            if(e.Key == Key.S && gameStarted)
                startClick(sender, e);

            if(e.Key == Key.Space)
                resetClick(sender, e);
        }

        private void rollBtn_Click(object sender, RoutedEventArgs e)
        {
            bankBtn.IsEnabled = false;
            PlayGame();
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            isFirstRoll = true;

            betBtn.IsEnabled = true;
            if (!betting)
            {
                bankBtn.IsEnabled = true;
                rollBtn.IsEnabled = true;
            }

            playAgainBtn.IsEnabled = false;
                
            canPlay = true;
            pointTextBlock.Text = "";
            winner.Text = "";
            die1Roll.Text = "";
            die2Roll.Text = "";
            dieTotal.Text = "";
        }

        private void startClick(object sender, RoutedEventArgs e)
        {
            rollBtn.IsEnabled = true;
            bankBtn.IsEnabled = true;
            canPlay = true;
            isFirstRoll = true;
        }

        private void aboutClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Name: Courtney Grover\nVersion 1.0\n.NET Framework Version 4.6\n Any CPU, 32 bit preferred");
        }

        private void resetClick(object sender, RoutedEventArgs e)
        {
            Reset();
        }

    }
}
