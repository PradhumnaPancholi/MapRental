namespace map_rental.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rental")]
    public partial class Rental
    {
        public int RentalId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Rent { get; set; }

        [Required]
        [StringLength(50)]
        public string Contact { get; set; }

        public int UserId { get; set; }
        //added//
        //public string DisplayName { get; set; }
        //================================//
        public virtual User User { get; set; }
    }
}
