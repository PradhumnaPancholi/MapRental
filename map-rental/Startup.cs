using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(map_rental.Startup))]
namespace map_rental
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
