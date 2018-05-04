using System;
using System.Collections.Generic;
using System.Data.Entity;
using RevViews.Core;
using RevViews.Models;

namespace RevViews.Persistence.Repository
{
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(DbContext context) : base(context)
        {
        }

        public RevViewsDB2Entities RevViewsContext => Context as RevViewsDB2Entities;

    }
}