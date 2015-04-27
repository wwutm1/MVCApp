using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EthosLendingInterview.Startup))]
namespace EthosLendingInterview
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
