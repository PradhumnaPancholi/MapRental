using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using map_rental.Models;

namespace map_rental.Controllers
{
    [Authorize]
    public class RentalsController : Controller
    {
        //private MapRentalModel db = new MapRentalModel();
        private IRentalsMock db;

        //default constructor//
        public RentalsController()
        {
            this.db = new EFRentals();
        }

        //mock constructor//
        public RentalsController(IRentalsMock mock)
        {
            this.db = mock;
        }
        // GET: Rentals
        [AllowAnonymous]
        public ActionResult Index()
        {
            var rentals = db.Rentals.Include(r => r.User);
            //return View(rentals.ToList());
            return View("Index", rentals.ToList());
        }

        public ViewResult Rentals(object p)
        {
            throw new NotImplementedException();
        }

        // GET: Rentals/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            Rental rental = db.Rentals.SingleOrDefault(r => r.RentalId == id);
            if (rental == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View(rental);
        }

        // GET: Rentals/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username");
            return View("Create");
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentalId,Title,Address,City,State,Rent,Contact,UserId")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                //if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                //{
                String Username = User.Identity.Name;
                //}
                //db.Rentals.Add(rental);
                db.Save(rental);
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", rental.UserId);
            return View("Create", rental);
        }

        // GET: Rentals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //Rental rental = db.Rentals.Find(id);
            Rental rental = db.Rentals.SingleOrDefault(r => r.RentalId == id);
            if (rental == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", rental.UserId);
            return View("Edit", rental);
        }

        //POST: Rentals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalId,Title,Address,City,State,Rent,Contact,UserId")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(rental).State = EntityState.Modified;
                db.Save(rental);
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Username", rental.UserId);
            return View("Edit", rental);
        }

        // GET: Rentals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            Rental rental = db.Rentals.SingleOrDefault(r => r.RentalId == id);
            if (rental == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View("Delete",rental);
        }


        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //    Rental rental = db.Rentals.Find(id);
            //    db.Rentals.Remove(rental);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            Rental rental = db.Rentals.SingleOrDefault(r => r.RentalId == id);
            if (rental == null)
            {
                return View("Error");
            }
            else
            {
                db.Delete(rental);
                return RedirectToAction("Index");
            }
            //}

            //protected override void Dispose(bool disposing)
            //{
            //    if (disposing)
            //    {
            //        db.Dispose();
            //    }
            //    base.Dispose(disposing);
        }
    }
}
