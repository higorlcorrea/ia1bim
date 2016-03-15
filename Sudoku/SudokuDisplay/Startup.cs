using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SudokuDisplay.Startup))]
namespace SudokuDisplay
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
