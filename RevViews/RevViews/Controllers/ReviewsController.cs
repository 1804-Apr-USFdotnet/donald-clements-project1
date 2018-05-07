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
    public class ReviewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ReviewsController()
        {
            _unitOfWork = new UnitOfWork();
        }

        // GET: Reviews
        public ActionResult Index()
        {
            var reviews = _unitOfWork.Reviews.GetAll();
            return View(reviews);
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = _unitOfWork.Reviews.Get((int)id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create(int restID)
        {
            ViewBag.RestaurantID = restID;
            ViewBag.RestaurantName = _unitOfWork.Restaurants.Get(restID).RestaurantName;
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RestaurantID,Username,Rating,Headline,Body,ReviewedOn")] Review review)
        {

            review.Restaurant = _unitOfWork.Restaurants.Get(review.RestaurantID);
            review.ReviewID = null;
            if (ModelState.IsValid)
            {
                _unitOfWork.Reviews.Add(review);
                _unitOfWork.Complete();
                return RedirectToAction("../Restaurants/Details/"+review.RestaurantID);
            }

            ViewBag.RestaurantID = review.RestaurantID;
            ViewBag.RestaurantName = _unitOfWork.Restaurants.Get(review.RestaurantID).RestaurantName;
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = _unitOfWork.Reviews.Get((int)id);
            if (review == null)
            {
                return HttpNotFound();
            }
            //ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestaurantName", review.RestaurantID);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewID,RestaurantID,Username,Rating,Headline,Body,ReviewedOn")] Review review)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantID = new SelectList(_unitOfWork.Restaurants.GetAll(), "RestaurantID", "RestaurantName", review.RestaurantID);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = _unitOfWork.Reviews.Get((int) id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = _unitOfWork.Reviews.Get((int)id);
            _unitOfWork.Reviews.Remove(review);
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
