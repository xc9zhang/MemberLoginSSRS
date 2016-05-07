using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemberLoginSSRS.Models
{
    public class DailyDataRow
    {
        public int Tickets { get; set; }
        public int Hour { get; set; }

        public string Client { get; set; }

        public DateTime MaxDate { get; set; }
    }
}
