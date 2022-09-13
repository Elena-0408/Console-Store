using Moq;
using NUnit.Framework;
using Store.Data;
using Store.Entities;
using Store.Validation;
using System.Collections.Generic;
using Store.ServiceImplementation;


namespace TestProject1
{
    public class GuestServicesTests
    {
        private readonly Mock<IDataStore> data;
        private readonly GuestServices services;
       
        public GuestServicesTests()
        {
            data = new Mock<IDataStore>();
            services = new GuestServices(data.Object);
        }

        [Test]
        public void SearchGoodsByName_ReturnsNameOfProduct()
        {
            var expected = new Goods()
            {
                Id = 10,
                Name = "Iron Philips 6000",
                Category = "Household appliances",
                Price = 1999,
                Description = "Iron"
            };
            data.Setup(p => p.Products).Returns(new List<Goods>()
            {
               new Goods()
                {
                    Name = "Multicooker PHILIPS Viva Collection"
                },
                new Goods()
                {
                    Name = "Coffee machine Philips Series 1200"
                },
                expected
            });

            services.SearchGoodsByName(expected.Name);
            Assert.IsTrue(true);
        }

        [Test]
        public void SearchGoodsByName_ReturnsException()
        {
            var expected = typeof(StoreException);

            data.Setup(p => p.Products).Returns(new List<Goods>()
            {
               new Goods()
                {
                    Name = "Multicooker PHILIPS Viva Collection"
                },
                new Goods()
                {
                    Name = "Coffee machine Philips Series 1200"
                },

            });

            var actual = Assert.Catch(() => { services.SearchGoodsByName(expected.Name); });
            Assert.AreEqual(expected, actual.GetType(), message: "Method works incorrectly ");
        }

        [Test]
        public void GetListOfGoods_ReturnsCorrectLenghtOfList()
        {
            data.Setup(p => p.Products).Returns(new List<Goods>()
            {
              new Goods()
                {
                    Name = "Multicooker PHILIPS Viva Collection"
                },
                new Goods()
                {
                    Name = "Coffee machine Philips Series 1200"
                },
            });

            services.GetListOfGoods();
            Assert.IsTrue(true);
        }


        [Test]
        public void GetListOfGoods_ReturnsException()
        {
            var expected = typeof(StoreException);
            data.Setup(p => p.Products).Returns(new List<Goods>() { });
            var actual = Assert.Catch(() => { services.GetListOfGoods(); });
            Assert.AreEqual(expected, actual.GetType(), message: "Method works incorrectly ");
        }
    }
}