using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RevViews.Core;
using RevViews.Models;

namespace RevViews.Persistence.Repository
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(DbContext context) : base(context)
        {
        }

        public RevViewsDB2Entities RevViewsContext => Context as RevViewsDB2Entities;

    }
}