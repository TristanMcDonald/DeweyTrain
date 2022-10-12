using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyTrain.Models
{
    public class Question
    {
        public int CallNumber { get; set; }
        public string Description { get; set; }

        public Question()
        {

        }
        public Question(int callNumber, string description)
        {
            this.CallNumber = callNumber;
            this.Description = description;
        }
    }
}
