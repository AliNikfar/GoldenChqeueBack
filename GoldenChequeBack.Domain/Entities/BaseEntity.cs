using System;
using System.ComponentModel.DataAnnotations;

namespace GoldenChequeBack.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime RegisterDate { get; set; }
        public int RegisterUser { get; set; }
        public DateTime LastChangeDate { get; set; }
        public int LastChangeUser { get; set; }

    }

}