using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyTrain.Models
{
    public class Description
    {
        public string description { get; set; }
        public Description(string desc)
        {
            this.description = desc;
        }
    }
}
