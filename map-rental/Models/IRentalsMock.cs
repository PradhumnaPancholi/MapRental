using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map_rental.Models
{
    public interface IRentalsMock
    {
        IQueryable<Rental> Rentals { get; }
        //for access to User//
        IQueryable<User> Users { get; }

        Rental Save(Rental rental);
        void Delete(Rental rental);
    }
}
