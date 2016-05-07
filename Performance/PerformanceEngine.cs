using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MemberLoginSSRS.Helpers;
using MemberLoginSSRS.Hubs;
using MemberLoginSSRS.Models;
using Microsoft.AspNet.SignalR;

namespace MemberLoginSSRS.Performance
{
    public class PerformanceEngine
    {
        private IHubContext _hubs;
        private readonly int _pollIntervalMillis;
        static Random _cpuRand;
        static Random _memRand;
        static Random _netIn;
        static Random _netOut;
        static Random _diskRd;
        static Random _diskWt;

        /*
        public static readonly IEnumerable<PerformanceCounter> ServiceCounters = new[]
        {
            //http://weblogs.thinktecture.com/ingo/2004/06/getting-the-current-process-your-own-cpu-usage.html
           // Code already written a LONG time ago to do this exact thing.
            new PerformanceCounter("Processor Information", "% Processor Time", "_Total"),
            new PerformanceCounter("Memory", "Available MBytes"),
            new PerformanceCounter("Process", "% Processor Time", GetCurrentProcessInstanceName(), true),   
            new PerformanceCounter("Process", "Working Set", GetCurrentProcessInstanceName(), true)                                       
        };
         * */

        public PerformanceEngine(int pollIntervalMillis)
        {
            //HostingEnvironment.RegisterObject(this);
            _hubs = GlobalHost.ConnectionManager.GetHubContext<PerformanceHub>();
            _pollIntervalMillis = pollIntervalMillis;
            _cpuRand = new Random();
            _memRand = new Random();
            _netIn = new Random();
            _netOut = new Random();
            _diskRd = new Random();
            _diskWt = new Random();
        }

        public async Task OnPerformanceMonitor()
        {
            //Monitor for infinity!
            while (true)
            {
                _hubs.Clients.All.broadcastBooking(DailyReportDataHelper.GetDailyData(DateTime.Now));
                await Task.Delay(_pollIntervalMillis);
            }
        }

        public void Stop(bool immediate)
        {

            //HostingEnvironment.UnregisterObject(this);
        }

        #region engine helpers
        private static string GetCurrentProcessInstanceName()
        {
            Process proc = Process.GetCurrentProcess();
            int pid = proc.Id;
            return GetProcessInstanceName(pid);
        }

        private static string GetProcessInstanceName(int pid)
        {
            PerformanceCounterCategory cat = new PerformanceCounterCategory("Process");

            string[] instances = cat.GetInstanceNames();
            foreach (string instance in instances)
            {

                using (PerformanceCounter cnt = new PerformanceCounter("Process",
                     "ID Process", instance, true))
                {
                    int val = (int)cnt.RawValue;
                    if (val == pid)
                    {
                        return instance;
                    }
                }
            }
            throw new Exception("Could not find performance counter " +
                "instance name for current process. This is truly strange ...");
        }
        #endregion
    }

}