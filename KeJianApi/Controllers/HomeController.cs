using System.Web.Mvc;

namespace KeJianApi.Controllers
{
    /// <summary>
    /// Home
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Home Index
        /// </summary>
        public ActionResult Index()
        {
            return Redirect("/swagger");
        }

    }
}
