using System;
using System.Collections.Generic;

namespace MemberLoginSSRS.Models
{
    public class DailyData
    {
        public string CurrentServerTime { get; set; }
        public int TEB2BTotal { get; set; }
        public int TE2UTotal { get; set; }
        public int HOPPERTotal { get; set; }
        public int CTRIPTotal { get; set; }

        public IEnumerable<DailyDataRow> TEB2BList { get; set; }
        public IEnumerable<DailyDataRow> TE2UList { get; set; }
        public IEnumerable<DailyDataRow> HOPPERList { get; set; }
        public IEnumerable<DailyDataRow> CTRIPList { get; set; }

        public DailyData()
        {
            CurrentServerTime = DateTime.Now.ToString();
            TEB2BList = InitialList(24);
            TE2UList = InitialList(24);
            HOPPERList = InitialList(24);
            CTRIPList = InitialList(24);
        }

        private IEnumerable<DailyDataRow> InitialList(int size)
        {
            var list = new List<DailyDataRow>();
            for (int i=0;i<size;i++)
            {
                list.Add(new DailyDataRow
                         {
                             Hour =i
                         });
            }
            return list;
        }


        public string TEB2BMaxDateTime { get; set; }

        public string TE2UMaxDateTime { get; set; }

        public string HOPPERMaxDateTime { get; set; }

        public string CTRIPMaxDateTime { get; set; }
    }
}
