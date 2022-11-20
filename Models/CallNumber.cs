using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyTrain.Models
{
    public class CallNumber
    {
        public int callNumber { get; set; }

        public CallNumber()
        {

        }
        public CallNumber(int callNum)
        {
            this.callNumber = callNum;
        }
    }
}
