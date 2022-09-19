using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DeweyTrain.Models
{
    public class LeaderboardUsers
    {
        public string Username { get; set; }
        public BitmapImage ProfilePicture { get; set; }
        public int Points { get; set; }
    }
}
