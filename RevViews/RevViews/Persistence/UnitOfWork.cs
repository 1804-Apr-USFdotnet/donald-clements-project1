using System;
using RevViews.Context;
using RevViews.Core;
using RevViews.Persistence.Repository;
using NLog;

namespace RevViews.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly RevViewsDB2Entities _context;

        public UnitOfWork(RevViewsDB2Entities context)
        {
            try
            {
                _context = context;
                Reviews = new ReviewRepository(_context);
                Restaurants = new RestaurantRepository(_context);
            }
            catch (Exception e)
            {
                logger.Error(e);
            }

        }

        public UnitOfWork()
        {
            try
            {
                _context = new RevViewsDB2Entities();
                Reviews = new ReviewRepository(_context);
                Restaurants = new RestaurantRepository(_context);
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
            
        }

        public IReviewRepository Reviews { get; }
        public IRestaurantRepository Restaurants { get; }

        public int Complete()
        {
            try
            {
                //logger.Info("Something Saved Test");
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                logger.Error(e);
                return 0;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}