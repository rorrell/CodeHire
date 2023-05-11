using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodeHire.Startup))]
namespace CodeHire
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
