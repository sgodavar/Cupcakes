using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cupcakes_shop.Startup))]
namespace Cupcakes_shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
