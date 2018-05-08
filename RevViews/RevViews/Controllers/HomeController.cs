using System.Linq;
using System.Web.Mvc;
using RevViews.Core;
using RevViews.Persistence;

namespace RevViews.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public HomeController()
        {
            _unitOfWork = new UnitOfWork();
        }
        public ActionResult Index()
        {
            ViewData["top3"] = _unitOfWork.Restaurants.GetAll().OrderByDescending(r => r.Rating()).Take(3).ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}