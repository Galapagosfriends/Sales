using Owin;

namespace SalesApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
             ConfigureAuth(app);
        }
    }
}
