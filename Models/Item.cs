using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyTrain.Models
{
    public class Item
    {
        public double CallNumber { get; set; }
        public string AuthorInitials { get; set; }

        public Item()
        {

        }
        public Item(double callNumber, string authorInitials)
        {
            this.CallNumber = callNumber;
            this.AuthorInitials = authorInitials;
        }
    }
}
