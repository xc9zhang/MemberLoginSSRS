using System.Threading.Tasks;
using MemberLoginSSRS;
using MemberLoginSSRS.Helpers;
using MemberLoginSSRS.Performance;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
[assembly: OwinStartup(typeof(Startup))]
namespace MemberLoginSSRS
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableDetailedErrors = true;
            app.MapSignalR(hubConfiguration);


            var performanceEngine = new PerformanceEngine(GlobalConstants.RefreshRate);
            Task.Factory.StartNew(async () => await performanceEngine.OnPerformanceMonitor());
        }
    }
}