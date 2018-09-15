using DAL.Entities;
using DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReadRSSFeed.Controllers;
using ReadRSSFeed.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadRssFeedUnitTest
{
    [TestClass]
    public class FeedControllerTest
    {
        private Mock<SubscriptionRepository> _mockSubscriptionRepo;
        private Mock<PostRepository> _mockPostRepo;
        private FeedController _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockSubscriptionRepo = new Mock<SubscriptionRepository>();
            _mockPostRepo = new Mock<PostRepository>();
            _controller = new FeedController(_mockSubscriptionRepo.Object,_mockPostRepo.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockSubscriptionRepo.VerifyAll();
        }

        [TestMethod]
        public void Index_ExpectViewResultReturned()
        {
            var subscriptions = new List<Subscription>
            {
                new Subscription()
                {
                    DisplayName = "Test Feed",                    
                    URL ="http://unittest.com"
                }
            };
            
            //_mockPostRepo.Setup(x => x.Get()).Returns(posts);
          
            var expectedModel = new List<Subscription>();
           
            //foreach (var stubContact in feeds)
            //{
            //    expectedModel.Add(new FeedViewModel()
            //    {
            //        Title = stubContact.Title,
            //        Link = stubContact.Link,
            //        Description = stubContact.Description,
            //        PubDate = stubContact.PubDate
            //    });
            //}

            var result = _controller.Index() as ViewResult;

            var actualModel = result.Model as List<Subscription>;
            for (int i = 0; i < expectedModel.Count; i++)
            {
                Assert.AreEqual(expectedModel[i].DisplayName, actualModel[i].DisplayName);
                Assert.AreEqual(expectedModel[i].URL, actualModel[i].URL);               
            }
        }
    }
}
