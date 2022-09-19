using DeweyTrain.Models;
using DeweyTrain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DeweyTrain
{
    internal class LeaderboardController
    {
        ReplacingBooksPage replacingBooksPage = new ReplacingBooksPage();
        //Loading the leaderboard list view with placeholder objects to simulate a
        //live release leaderboard (Feldman, 2016).
        public List<LeaderboardUsers> leaderboardUsers = new List<LeaderboardUsers>()
            {
                new LeaderboardUsers{Username="Jeff", ProfilePicture=LoadImage("ImgAssets/account.png"), Points = 20},
                new LeaderboardUsers{Username="Kim", ProfilePicture=LoadImage("ImgAssets/account.png"), Points = 55},
                new LeaderboardUsers{Username="Ben", ProfilePicture=LoadImage("ImgAssets/account.png"), Points = 100},
            };

        //Ordering the list by users points (Alons, 2015).
        public List<LeaderboardUsers> sortedLeaders = new List<LeaderboardUsers>();
        
        public void sortLeaderboardUsers()
        {
            replacingBooksPage.addCurrentUser();
            leaderboardUsers.Add(replacingBooksPage.currentUser);
            sortedLeaders = leaderboardUsers.OrderByDescending(x => x.Points).ToList();
        }

        // for this code image needs to be a project resource (Feldman, 2016).
        public static BitmapImage LoadImage(string filename)
        {
            return new BitmapImage(new Uri("pack://application:,,,/" + filename));
        }
    }
}
