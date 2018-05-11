using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web.Mvc;
using NLog;
using RevViews.Core;
using RevViews.Persistence;

namespace RevViews.Controllers
{
    
    public class HomeController : Controller
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
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
            try
            {
                ViewData["top3"] = _unitOfWork.Restaurants.GetAll().OrderByDescending(r => r.Rating()).Take(3).ToList();
                return View();
            }
            catch (Exception e)
            {
                logger.Error(e);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

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