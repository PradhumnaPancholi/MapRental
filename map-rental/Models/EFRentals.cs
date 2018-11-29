using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace map_rental.Models
{
    public class EFRentals : IRentalsMock
    {
        //connect to DB //
        private MapRentalModel db = new MapRentalModel();

        public IQueryable<Rental> Rentals { get { return db.Rentals; } }
        //for access to users//
        public IQueryable<User> Users { get { return db.Users; } }

        public void Delete(Rental rental)
        {
            db.Rentals.Remove(rental);
            db.SaveChanges();
        }

        public Rental Save(Rental rental)
        {
            if (rental.RentalId == 0)
            {
                //insert//
                db.Rentals.Add(rental);
            }
            else
            {
                //update//
                db.Entry(rental).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();
            return rental;
        }
    }
}