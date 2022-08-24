using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.Dto;
using WebAPI.Application.Interfaces;
using WebAPI.Controllers;

namespace WebAPI.Tests
{
    public class ArticleTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            // Arrange
            var mockRepository = new Mock<IArticleService>();
            mockRepository.Setup(x => x.GetArticle(42))
                .Returns(new ArticleResponseDto { Id = 42 });

            var controller = new ArticleController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(42);
            var contentResult = actionResult as OkNegotiatedContentResult<Product>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(42, contentResult.Content.Id);
        }
    }
}
