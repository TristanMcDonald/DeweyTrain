using DeweyTrain.Models;
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
using System.Linq;

namespace DeweyTrain.Views
{
    /// <summary>
    /// Interaction logic for LeaderboardPage.xaml
    /// </summary>
    public partial class LeaderboardPage : Page 
    {
        LeaderboardController leaderboardController = new LeaderboardController();
        public List<LeaderboardUsers> leaderboardUsers = new List<LeaderboardUsers>();
        public LeaderboardPage()
        {
            InitializeComponent();

            //Loading the leaderboard list view with placeholder objects to simulate a
            //live release leaderboard (Feldman, 2016).
            leaderboardController.sortLeaderboardUsers();
            leaderboardUsers = leaderboardController.sortedLeaders;
            this.leaderboardListView.ItemsSource = leaderboardUsers;
        }
    }
}
