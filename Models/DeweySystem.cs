using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyTrain.Models
{
    public class DeweySystem
    {
        public string TopLevelCallNumber { get; set; }
        public List<string> ThirdLevelCallNumbers { get; set; }

        public DeweySystem()
        {

        }

        public DeweySystem(string topLevelCallNumber, List<string> thirdLevelCallNumbers)
        {
            this.TopLevelCallNumber = topLevelCallNumber;
            this.ThirdLevelCallNumbers = thirdLevelCallNumbers;
        }
    }
}
