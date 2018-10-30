using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Attendance_ManagementV1._0.Startup))]
namespace Attendance_ManagementV1._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }
}
