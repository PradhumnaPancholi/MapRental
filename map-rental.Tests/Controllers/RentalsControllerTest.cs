using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using map_rental.Controllers;
using map_rental.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace map_rental.Tests.Controllers
{
    [TestClass]
    public class RentalsControllerTest
    {
        RentalsController controller;
        Mock<IRentalsMock> mock;
        List<Rental> rentals;
        

        [TestInitialize]
        public void TestInitialize()
        {
            //mock data object to hold fake data//
            mock = new Mock<IRentalsMock>();

            //populate mock list
            rentals = new List<Rental>
            {
                new Rental { RentalId = 777 , Title = "Title 1", Address = "107, Georgian Drive", City = "Barrie", State = "ON", Rent=700, Contact = "111-222-3333",
                    User = new User {UserId = 111, Username = "ColtStele@gmail.com", DisplayName = "Colt Stele"}
                },

                new Rental { RentalId = 888 , Title = "Title 2", Address = "108, Georgian Drive", City = "Barrie", State = "ON", Rent=700, Contact = "555-666-7777",
                    User = new User {UserId = 222, Username = "steve@gmail.com", DisplayName = "Stephen Grider"}
                }
            };

            mock.Setup(r => r.Rentals).Returns(rentals.AsQueryable());
            controller = new RentalsController(mock.Object);
        }
        //GET: Rentals/Index
        #region
        [TestMethod]
        public void IndexLoadsView()
        {
            //arrange//
            RentalsController controller = new RentalsController();

            //act//
            ViewResult result = controller.Index() as ViewResult;

            //assert//
            Assert.AreEqual("Index", result.ViewName);

        }

        [TestMethod]
        public void IndexReturnsRentals()
        {
            //act //
            var result = (List<Rental>)((ViewResult)controller.Index()).Model;

            //assert//
            CollectionAssert.AreEqual(rentals, result);
        }
        #endregion

        //GET:  Rentals/Details/:id
        #region
        [TestMethod]
        public void DetailsNoIdLoadsError()
        {
            //act//
            ViewResult result = (ViewResult)controller.Details(null);

            //assert//
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidIdLoadsError()
        {
            //act//
            ViewResult result = (ViewResult)controller.Details(6665);

            //assert//
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidIdLoadsError()
        {
            //act//
            Rental result = (Rental)((ViewResult)controller.Details(777)).Model;

            //assert//
            Assert.AreEqual(rentals[0], result);
        }
        #endregion

        // GET: Rentals/Edit
        #region
        [TestMethod]
        public void EditNoIdLoadsError()
        {
            //arrange
            int? id = null;
            //act 
            ViewResult result = (ViewResult)controller.Edit(id);
            //assert 
            Assert.AreEqual("Error", result.ViewName);
        }
        [TestMethod]
        public void EditValidIdLoadsRental()//to verify if view returns correct data 
        {
            //act
            Rental result = (Rental)((ViewResult)controller.Edit(777)).Model;
            //assert 
            Assert.AreEqual(rentals[0], result);
        }
        [TestMethod]
        public void EditInvalidIdLoadsError()
        {
            //act 
            ViewResult result = (ViewResult)controller.Edit(666);
            //assert 
            Assert.AreEqual("Error", result.ViewName);
        }
        [TestMethod]
        public void EditViewBagUser()//to solve/check issue with userId
        {
            //act
            ViewResult result = (ViewResult)controller.Edit(777);
            //assert
            Assert.IsNotNull(result.ViewBag.UserId);
        }
        [TestMethod]
        public void EditValidIdLoadsView()//to verify view 
        {
            //act 
            ViewResult result = (ViewResult)controller.Edit(777);
            //assert
            Assert.AreEqual("Edit", result.ViewName);
        }
        #endregion

        // POST: Rentals/Edit
        #region
        [TestMethod]
        public void EditValidModelCreatesRental()
        {
            //act 
            Rental testRental = new Rental { RentalId = 777 };
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(testRental);

            //assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        [TestMethod]
        public void EditModelIsValidReturnView()//to verify view//
        {
            //act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(rentals[0]);
            //Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        [TestMethod]
        public void EditInvalidModelReturnView()
        {
            //arrange
            controller.ModelState.AddModelError("Error", "Error thing");
            Rental testRental = new Rental { RentalId = 1 };
            //act
            ViewResult result = (ViewResult)controller.Edit(testRental);
            //assert
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void EditInvalidModelReloadRental()
        {
            //arrange
            controller.ModelState.AddModelError("Error", "Error thing");
            Rental testRental = new Rental { RentalId = 1 };
            //act
            Rental result = (Rental)((ViewResult)controller.Edit(testRental)).Model;
            //assert
            Assert.AreEqual(testRental, result);


        }
        #endregion

        //GET: Rentals/Create
        #region
        [TestMethod]
        public void CreateLoadsView()
        {
            //act
            ViewResult result = (ViewResult)controller.Create();
            //assert
            Assert.AreEqual("Create", result.ViewName);
        }
        #endregion

        // POST: Rentals/Create
        [TestMethod]
        public void ModelSavesNewRecord()
        {
            ////act
            //Rental copiedRentalFromGlobal = rental;
            //RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(copiedRentalFromGlobal);
            ////assert
            //Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        // GET: Rentals/Delete
        #region

        [TestMethod]
        public void DeleteNoIdLoadsError()
        {
            //act
            ViewResult result = (ViewResult)controller.Delete(null);

            //assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteInvalidIdLoadsError()
        {
            //act
            ViewResult result = (ViewResult)controller.Delete(555);

            //assert
            Assert.AreEqual("Error", result.ViewName);
        }
        #endregion
        // POST: Rentals/Delete
        [TestMethod]
        public void DeleteConfirmedDataSuccessful()
        {
            //act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.DeleteConfirmed(100);

            //assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

    }
}

