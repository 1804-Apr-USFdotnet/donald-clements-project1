using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RevViews.Core;
using RevViews.Models;
using RevViews.Persistence;

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
            _unitOfWork = new UnitOfWork(new RevViewsDB2Entities());
        }
        //private RevViewsDB2Entities db = new RevViewsDB2Entities();

        // GET: Restaurants
        public ActionResult Index()
        {
            //return View(db.Restaurants.ToList());
            return View(_unitOfWork.Restaurants.GetAll());
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Restaurant restaurant = _unitOfWork.Restaurants.Find( o=>o.RestaurantID==id).Single();
            Restaurant restaurant = _unitOfWork.Restaurants.SingleOrDefault(o => o.RestaurantID == id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
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
        public ActionResult Create([Bind(Include = "RestaurantID,RestaurantName,AddressLineOne,City,StateCode,PostalCode,Phone,Website")] Restaurant restaurant)
        {
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = _unitOfWork.Restaurants.SingleOrDefault(o => o.RestaurantID == id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RestaurantID,RestaurantName,AddressLineOne,City,StateCode,PostalCode,Phone,Website")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = _unitOfWork.Restaurants.SingleOrDefault(o => o.RestaurantID == id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = _unitOfWork.Restaurants.SingleOrDefault(o => o.RestaurantID == id);
            _unitOfWork.Restaurants.Remove(restaurant);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
