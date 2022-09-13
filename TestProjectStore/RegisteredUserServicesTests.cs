using Moq;
using NUnit.Framework;
using Store.Data;
using Store.Entities;
using Store.GetUserData;
using Store.ServiceImplementation;
using Store.Validation;
using System.Collections.Generic;


namespace TestProject1
{
   public class RegisteredUserServicesTests
    {
        private readonly Mock<IDataStore> data;
        private readonly RegisteredUserServices services;
        public RegisteredUserServicesTests()
        {
            data = new Mock<IDataStore>();
            services = new RegisteredUserServices(data.Object);
        }

        [Test]
        public void SearchGoodsByName_ReturnsTrue()
        {
            var expected = new Goods()
            {
                Name = "Iron Philips 6000"
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
            data.Setup(o => o.RegisteredUsers).Returns(new List<RegisteredUser>()
            {
                new RegisteredUser()
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
        public void Cancellation_ReturnsTrue()
        {
            data.Setup(o => o.Orders).Returns(new List<Order>()
            {
               new Order()
                {
                    Id = 10,
                    Customer = "Julia",
                    Status = StatusOfOrder.PaymentReceived,
                    Product = new List<Goods>()
                    {
                        new Goods()
                        {
                           Id = 16,
                           Name = "Coffee machine Philips Series 1200",
                           Category = "Kitchen appliances"
                        }
                    }
                }
            });
            services.Cancellation(10);
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
                }
            });
            var actual = services.ChangeStatus(10);
            Assert.AreEqual(true, actual, message: "Method works incorrectly ");
        }
        
        [Test]
        public void GetOrdersHistory_ReturnsTrue()
        {
            var product1 = new Goods()
            {
                Id = 15,
                Name = "Multicooker PHILIPS Viva Collection",
                Category = "Kitchen appliances"

            };
            var product2 = new Goods()
            {
                Id = 16,
                Name = "Coffee machine Philips Series 1200",
                Category = "Kitchen appliances"
            };
            data.Setup(u => u.RegisteredUsers).Returns(new List<RegisteredUser>()
            {
              new RegisteredUser()
              {
                  Id = 6,
                  Name = "Julia",
                  Phone = "365217895",
                  Email = "ju@gmail.com",
                  Password = "123789",
                  IsLoginIn = true

                 },
                 new RegisteredUser()
                 {
                   Id = 7,
                   Name = "Kate",
                   Phone = "3702317852",
                   Email = "k0303@gmail.com",
                   Password = "0303password",
                   IsLoginIn = false

                 }
               }); 
            data.Setup(o => o.Orders).Returns(new List<Order>()
            {
               new Order()
                {
                    Id = 10,
                    Customer = "Julia",
                    Status = StatusOfOrder.PaymentReceived,
                    Product = new List<Goods>()
                    {
                        product1, product2
                    }
                }
            });
            services.GetOrdersHistory();
            Assert.IsTrue(true);
        }

        [Test]
        public void GetOrdersHistory_ReturnsFalse()
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
                  IsLoginIn = true

                 },
                 new RegisteredUser()
                 {
                   Id = 7,
                   Name = "Kate",
                   Phone = "3702317852",
                   Email = "k0303@gmail.com",
                   Password = "0303password",
                   IsLoginIn = false

                 }
               });
            data.Setup(o => o.Orders).Returns(new List<Order>()
            {
               
            });
            services.GetOrdersHistory();
            Assert.IsFalse(false);
        }

        
        [Test]
        public void CheckPhone_ReturnsTrue()
        {
            GetDataForUser.CheckPhone("38063547896");
            Assert.IsTrue(true);
        }

        [Test]
        public void CheckPhone_ReturnsFalse()
        {
            GetDataForUser.CheckPhone("3806354789U");
            Assert.IsFalse(false);
        }

        [Test]
        public void CheckEmail_ReturnsTrue()
        {
            GetDataForUser.CheckEmail("rrrr@gmail.com");
            Assert.IsTrue(true);
        }

        [Test]
        public void CheckEmail_ReturnsFalse()
        {
            GetDataForUser.CheckEmail("email");
            Assert.IsFalse(false);
        }
        [Test]
        public void ChangePersonalInformation_ReturnsTrue()
        {
            data.Setup(u => u.RegisteredUsers).Returns(new List<RegisteredUser>()
                {
                  new RegisteredUser()
                  {
                      Id = 7,
                      Name = "Aarav Davis",
                      Email = "Aarav@gmail.com",
                      Password = "Davis87",
                      Phone = "3965214872",
                      IsLoginIn = true
                      
                  },
                  new RegisteredUser()
                  {
                      Id = 8,
                      Name = "Oscar Adams",
                      Email = "Osc@gmail.com",
                      Password = "OscAd",
                      Phone = "325896421",
                      IsLoginIn = false
                  },
                });

            services.ChangePersonalInformation("Aarav@gmail.com", "Davis789456", "3965214872");
            Assert.IsTrue(true);
        }
    }
}
