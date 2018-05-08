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

namespace RevViews.Controllers.Tests
{
    [TestFixture]
    public class ReviewsControllerTests
    {
        [Test]
        public void EditRouteNullTest()
        {
            int? id = null;
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            fakeUnitOfWork.Setup(x => x.Reviews.Get(It.IsAny<int>())).Returns(new Review() { ReviewID = id });
            var sut = new ReviewsController(fakeUnitOfWork.Object);
            sut.WithCallTo(x => x.Edit((int?)id)).ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void EditRouteNonNullTest()
        {
            int? id = 1;
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            fakeUnitOfWork.Setup(x => x.Reviews.Get(It.IsAny<int>())).Returns(new Review() { ReviewID = id });
            var sut = new ReviewsController(fakeUnitOfWork.Object);
            sut.WithCallTo(x => x.Edit(id)).ShouldRenderDefaultView();
        }

        [Test]
        public void DeleteRouteNullTest()
        {
            int? id = null;
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            fakeUnitOfWork.Setup(x => x.Reviews.Get(It.IsAny<int>())).Returns(new Review() { ReviewID = id });
            var sut = new ReviewsController(fakeUnitOfWork.Object);
            sut.WithCallTo(x => x.Delete((int?)id)).ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void DeleteRouteNotNullTest()
        {
            int? id = 1;
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            fakeUnitOfWork.Setup(x => x.Reviews.Get(It.IsAny<int>())).Returns(new Review() { ReviewID = id });
            var sut = new ReviewsController(fakeUnitOfWork.Object);
            sut.WithCallTo(x => x.Delete((int)id)).ShouldRenderDefaultView();
        }

        [Test]
        public void DetailsRouteNullTest()
        {
            int? id = null;
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            fakeUnitOfWork.Setup(x => x.Reviews.Get(It.IsAny<int>())).Returns(new Review() { ReviewID = id });
            var sut = new ReviewsController(fakeUnitOfWork.Object);
            sut.WithCallTo(x => x.Details((int?)id)).ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [Test]
        public void DetailsRouteNotNullTest()
        {
            int? id = 1;
            var fakeUnitOfWork = new Mock<IUnitOfWork>();
            fakeUnitOfWork.Setup(x => x.Reviews.Get(It.IsAny<int>())).Returns(new Review() { ReviewID = id });
            var sut = new ReviewsController(fakeUnitOfWork.Object);
            sut.WithCallTo(x => x.Details((int)id)).ShouldRenderDefaultView();
        }
    }
}