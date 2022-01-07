using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2Go.Core.Genarators
{
    public class SerialGenarator : ISerialGenarator
    {
        public string GetLedgerTrxNumber()
        {
            var date = DateTime.Now;
            string str = date.Year.ToString("0000") + date.Month.ToString("00") + date.Day.ToString("00") +
                date.Hour.ToString("00") + date.Minute.ToString("00") + date.Second.ToString("00") + date.Millisecond.ToString("0000");
            return str;
        }

        public string GetTripNumber()
        {
            var date = DateTime.Now;
            string str = "T"+ date.Year.ToString("0000") + date.Month.ToString("00") + date.Day.ToString("00") +
                date.Hour.ToString("00") + date.Minute.ToString("00") + date.Second.ToString("00") + date.Millisecond.ToString("0000");
            return str;
        }
    }
}
