using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoldenChequeBack.Domain.Entities
{
    public class Customer : BaseEntity
    {
        [Required]
        public int Code 
        {
            get;
            set;
        }
        [Required]
        [StringLength(50)]
        public string Name 
        {
            get;
            set;
        }
        [Required]
        [StringLength(100)]
        public string LastName 
        {
            get;
            set;
        }
        [StringLength(50)]
        public string FatherName 
        {
            get;
            set;
        }
        [Required]
        [StringLength(20)]
        public string PhoneNum 
        {
            get;
            set;
        }
        [StringLength(11)]
        public string Mob1
        {
            get;
            set;
        }
        [StringLength(11)]
        public string Mob2
        {
            get;
            set;
        }
        [StringLength(11)]
        public string Mob3
        {
            get;
            set;
        }
        [Required]
        public City City
        {
            get;
            set;
        }
        [StringLength(200)]
        public string Address 
        { 
            get; 
            set; 
        }
        [StringLength(10)]
        public string PostalCode 
        {
            get;
            set;
        }

        [StringLength(200)]
        public string Details
        {
            get;
            set;
        }

        public int? MaxBuyPrice
        {
            get;
            set;
        }

        public DateTime? BirthDate
        {
            get;
            set;
        }

        public CustomerRate CustomerRate
        {
            get;
            set;
        }

    }
}
