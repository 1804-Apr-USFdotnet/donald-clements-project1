using System;

namespace RevViews.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IRestaurantRepository Restaurants { get; }
        IReviewRepository Reviews { get; }
        int Complete();
    }
}