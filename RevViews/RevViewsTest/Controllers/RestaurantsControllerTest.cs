using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevViews.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using RevViews.Core;
using RevViews.Models;
using TestStack.FluentMVCTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace RevViews.Controllers.Tests
{
    [TestFixture]
    public class RestaurantsControllerTest
    {
        
        [Test]
        public void EditRouteNullTest()
        {
            int? id = null;
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            fakeUnitOfWork.Setup(x => x.Restaurants.Get(It.IsAny<int>())).Returns(new Restaurant(){RestaurantID = id});
            var sut = new RestaurantsController(fakeUnitOfWork.Object);
            sut.WithCallTo(x => x.Edit((int?) id)).ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void EditRouteNonNullTest()
        {
            int? id = 1;
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            fakeUnitOfWork.Setup(x => x.Restaurants.Get(It.IsAny<int>())).Returns(new Restaurant() { RestaurantID = id });
            var sut = new RestaurantsController(fakeUnitOfWork.Object);
            sut.WithCallTo(x => x.Edit(id)).ShouldRenderDefaultView();
        }

        [Test]
        public void DeleteRouteNullTest()
        {
            int? id = null;
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            fakeUnitOfWork.Setup(x => x.Restaurants.Get(It.IsAny<int>())).Returns(new Restaurant() { RestaurantID = id });
            var sut = new RestaurantsController(fakeUnitOfWork.Object);
            sut.WithCallTo(x => x.Delete((int?)id)).ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void DeleteRouteNotNullTest()
        {
            int? id = 1;
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            fakeUnitOfWork.Setup(x => x.Restaurants.Get(It.IsAny<int>())).Returns(new Restaurant() { RestaurantID = id });
            var sut = new RestaurantsController(fakeUnitOfWork.Object);
            sut.WithCallTo(x => x.Delete(id)).ShouldRenderDefaultView();
        }

        [Test]
        public void DetailsRouteNullTest()
        {
            int? id = null;
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            fakeUnitOfWork.Setup(x => x.Restaurants.Get(It.IsAny<int>())).Returns(new Restaurant() { RestaurantID = id });
            var sut = new RestaurantsController(fakeUnitOfWork.Object);
            sut.WithCallTo(x => x.Details((int?)id)).ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void DetailsRouteNotNullTest()
        {
            int? id = 1;
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            fakeUnitOfWork.Setup(x => x.Restaurants.Get(It.IsAny<int>())).Returns(new Restaurant() { RestaurantID = id });
            fakeUnitOfWork.Setup(x => x.Reviews.Find(o => o.RestaurantID == It.IsAny<int>()))
                .Returns(new List<Review>());//.Reviews.Find(o => o.RestaurantID == id)
            var sut = new RestaurantsController(fakeUnitOfWork.Object);
            sut.WithCallTo(x => x.Details(id)).ShouldRenderDefaultView();
        }
    }
}