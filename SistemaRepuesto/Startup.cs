using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaRepuesto.Startup))]
namespace SistemaRepuesto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
