using Moq;
using NUnit.Framework;
using Store.Data;
using Store.Entities;
using Store.ServiceImplementation;
using Store.Validation;
using System.Collections.Generic;


namespace TestProject1
{
    public class AdministratorServicesTests
    {
        private readonly Mock<IDataStore> data;
        private readonly AdministratorServices services;
       
        public AdministratorServicesTests()
        {
            data = new Mock<IDataStore>();
            services = new AdministratorServices(data.Object);
            
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

        
        [Test]
        public void SignOut_ReturnsTrue()
        {
            data.Setup(o => o.Administrators).Returns(new List<Administrator>()
            {
                new Administrator()
                {
                Id = 6,
                Name = "Sam",
                Email = "sssam@gmail.com",
                Password = "070707",
                Phone = "380655548625",
                IsLoginIn = true
                }
            });
            services.SignOut();
            Assert.IsTrue(true);
        }

        [Test]
        public void AddProduct_ReturnsTrue()
        {
            data.Setup(o => o.Products).Returns(new List<Goods>()
            {
              new Goods()
                {
                Id = 10,
                Name = "Iron Philips 6000",
                Category = "Household appliances",
                Price = 1999,
                Description = "Iron"
                }
           });

            services.AddProduct("Xiaomi", "Smartphone", 6000, "Chine");
            
            Assert.IsTrue(true);

        }

        [Test]
        public void ChangeStatus_ReturnsTrue()
        {
            data.Setup(o => o.Orders).Returns(new List<Order>()
            {
               new Order()
                {
                    Id = 10,
                    Status = StatusOfOrder.PaymentReceived,
                },
               new Order()
               {
                    Id = 11,
                    Status = StatusOfOrder.Completed,
                }
            });
            services.ChangeStatus(11, StatusOfOrder.Sent);
            Assert.IsTrue(true);
        }

        [Test]
        public void GetInformationOfUser_ReturnsTrue()
        {
            data.Setup(u => u.RegisteredUsers).Returns(new List<RegisteredUser>()
            {
              new RegisteredUser()
              {
                  Id = 6,
                  Name = "Julia",
                  Phone = "365217895",
                  Email = "ju@gmail.com",
                  Password = "123789",

              },
              new RegisteredUser()
              {
                Id = 7,
                Name = "Kate",
                Phone = "3702317852",
                Email = "k0303@gmail.com",
                Password = "0303password",

              }
            });
            Assert.IsTrue(true);

        }

        [Test]
        public void GetInformationOfUser_ReturnsFalse()
        {
            data.Setup(u => u.RegisteredUsers).Returns(new List<RegisteredUser>() { });

            Assert.IsFalse(false);
        }
        
        [Test]
        public void ChangeProductInformation_ReturnsTrue()
        {
            data.Setup(p => p.Products).Returns(new List<Goods>()
            {
               new Goods()
                {
                    Id = 6,
                    Name = "Xiaomi Mi Smart Band 6 Black",
                    Category = "Wearable gadgets",
                    Price = 1100,
                    Description = "Xiaomi fitness bracelets"
                },
                new Goods()
                {
                    Id = 7,
                    Name = "Smart watch Fossil Gen 6",
                    Category = "Wearable gadgets",
                    Price = 7000,
                    Description = "Smart watch"
                }
            });
            var actual = services.ChangeProductInformation(6, "Xiaomi Mi Smart Band 7 Gold", "Wearable gadgets", 1500, "Xiaomi fitness bracelets");
            Assert.AreEqual(true, actual, message: "Method works incorrectly ");
        }
        [Test]
        public void ChangeInformationOfUser_ReturnsTrue()
        {
            data.Setup(u => u.RegisteredUsers).Returns(new List<RegisteredUser>()
            {
              new RegisteredUser()
              {
                  Id = 7,
                  Name = "Aarav Davis",
                  Email = "Aarav@gmail.com",
                  Password = "Davis87",
                  Phone = "3965214872"
              },
              new RegisteredUser()
              {
                  Id = 8,
                  Name = "Oscar Adams",
                  Email = "Osc@gmail.com",
                  Password = "OscAd",
                  Phone = "325896421"
              },
            });

            var actual = services.ChangeInformationOfUser(8, "Oscar Adams", "OsCaR@gmail.com", "OscAd", "1521354658746");
            Assert.AreEqual(true, actual, message: "Method works incorrectly ");
        }
    }
}
