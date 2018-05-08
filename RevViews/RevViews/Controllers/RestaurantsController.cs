using PagedList;
using RevViews.Core;
using RevViews.Models;
using RevViews.Persistence;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace RevViews.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public RestaurantsController()
        {
            _unitOfWork = new UnitOfWork();
        }
        //private RevViewsDB2Entities db = new RevViewsDB2Entities();

        // GET: Restaurants

        public ActionResult Index(string sort, string search, int? page)
        {
            ViewBag.NameSortParm = sort == "Name" ? "name_desc" : "Name";
            ViewBag.RatingSortParm = string.IsNullOrEmpty(sort) ? "Rating" : "";

            ViewBag.CurrentSort = sort;
            ViewBag.CurrentSearch = search;

            var restaurants = _unitOfWork.Restaurants.Find(r => search == null || r.RestaurantName.Contains(search))
                .OrderByDescending(r => r.Rating());

            switch (sort)
            {
                case "name_desc":
                    restaurants = restaurants.OrderByDescending(r => r.RestaurantName);
                    break;
                case "Name":
                    restaurants = restaurants.OrderBy(r => r.RestaurantName);
                    break;
                case "Rating":
                    restaurants = restaurants.OrderBy(r => r.Rating());
                    break;
                default:
                    restaurants = restaurants.OrderByDescending(r => r.Rating());
                    break;
            }

            var pageSize = 5;
            var pageNumber = page ?? 1;

            return View(restaurants.ToPagedList(pageNumber, pageSize));
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            // Restaurant restaurant = _unitOfWork.Restaurants.Find( o=>o.RestaurantID==id).Single();
            ViewData["id"] = id;
            ViewBag.rev = _unitOfWork.Reviews.Find(o => o.RestaurantID == id);
            var restaurant = _unitOfWork.Restaurants.Get((int) id);
            if (restaurant == null) return HttpNotFound();
            return View(restaurant);
        }

        // GET: Restaurants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include =
                "RestaurantID,RestaurantName,AddressLineOne,City,StateCode,PostalCode,Phone,Website")]
            Restaurant restaurant)
        {
            if (string.IsNullOrWhiteSpace(restaurant.Website))
            {
                restaurant.Website = "NA";
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Restaurants.Add(restaurant);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var restaurant = _unitOfWork.Restaurants.Get((int) id);
            if (restaurant == null) return HttpNotFound();
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
                "RestaurantID,RestaurantName,AddressLineOne,City,StateCode,PostalCode,Phone,Website")] Restaurant restaurant)
        {
            if (string.IsNullOrWhiteSpace(restaurant.Website))
            {
                restaurant.Website = "NA";
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Restaurants.Update(restaurant);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var restaurant = _unitOfWork.Restaurants.Get((int) id);
            if (restaurant == null) return HttpNotFound();
            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var restaurant = _unitOfWork.Restaurants.Get(id);
            _unitOfWork.Restaurants.Remove(restaurant);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}