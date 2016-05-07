using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MemberLoginSSRS.Models;

namespace MemberLoginSSRS.Helpers
{
    public static class Sql
    {
        public static T Read<T>(DbDataReader DataReader, string FieldName)
        {
            int FieldIndex;
            try { FieldIndex = DataReader.GetOrdinal(FieldName); }
            catch { return default(T); }

            if (DataReader.IsDBNull(FieldIndex))
            {
                return default(T);
            }
            else
            {
                object readData = DataReader.GetValue(FieldIndex);
                if (readData is T)
                {
                    return (T)readData;
                }
                else
                {
                    try
                    {
                        return (T)Convert.ChangeType(readData, typeof(T));
                    }
                    catch (InvalidCastException)
                    {
                        return default(T);
                    }
                }
            }
        }
    }
    public static class DailyReportDataHelper
    {
        public static DailyData GetDailyData(DateTime date)
        {
            var data = new DailyData();

            using (var con = new SqlConnection(GlobalConstants.TeReportConnection))
            {
                using (var cmd = new SqlCommand("SP_SSRS_GetBookingDailyData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //to do : replace following when release
                    cmd.Parameters.AddWithValue("@Date", date);
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        var list = new List<DailyDataRow>();
                        while (dr.Read())
                        {
                            list.Add(new DailyDataRow
                            {
                                Hour = Sql.Read<Int32>(dr, "HHour"),
                                Tickets = Sql.Read<Int32>(dr, "Tickets"),
                                Client = Sql.Read<String>(dr, "Client"),
                                MaxDate = Sql.Read<DateTime>(dr, "MaxDate"),
                            });
                        }

                        data.TEB2BList = ParseList(list, data.TEB2BList, "TEB2B");
                        data.TE2UList = ParseList(list, data.TE2UList, "TE2U");
                        data.HOPPERList = ParseList(list, data.HOPPERList, "HOPPER");
                        data.CTRIPList = ParseList(list, data.CTRIPList, "CTRIP");

                        data.TEB2BTotal = data.TEB2BList.Sum(p => p.Tickets);
                        data.TE2UTotal = data.TE2UList.Sum(p => p.Tickets);
                        data.HOPPERTotal = data.HOPPERList.Sum(p => p.Tickets);
                        data.CTRIPTotal = data.CTRIPList.Sum(p => p.Tickets);

                        data.TEB2BMaxDateTime = DateTime.Now.ToString();
                        data.TE2UMaxDateTime = DateTime.Now.ToString();
                        data.HOPPERMaxDateTime = DateTime.Now.ToString();
                        data.CTRIPMaxDateTime = DateTime.Now.ToString();


                    }
                }
            }
            return data;
        }

        private static IEnumerable<DailyDataRow> ParseList(IEnumerable<DailyDataRow> data, IEnumerable<DailyDataRow> list, string client)
        {
            Random rd=new Random();
            foreach (var datarow in data.Where(p => p.Client.ToLower() == client.ToLower()))
            {
                var match = list.FirstOrDefault(p => p.Hour == datarow.Hour);
                //match.Tickets = datarow.Tickets + rd.Next(10);
                match.Tickets = datarow.Tickets; 
                match.MaxDate = datarow.MaxDate;
            }
            return list;
        }

    }
}