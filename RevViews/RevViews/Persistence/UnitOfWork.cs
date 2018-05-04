﻿using NLog;
using RevViews.Core;
using RevViews.Models;
using RevViews.Persistence.Repository;

namespace RevViews.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RevViewsDB2Entities _context;

        public UnitOfWork(RevViewsDB2Entities context)
        {
            _context = context;
            Reviews = new ReviewRepository(_context);
            Restaurants = new RestaurantRepository(_context);
        }

        public IReviewRepository Reviews { get; }
        public IRestaurantRepository Restaurants { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}