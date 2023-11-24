using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Domain.Entities
{
    public class Product :BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Title
        {
            get;
            set;
        }
        [Required]
        public int Price
        {
            get;
            set;
        }
        [Required]
        public int BuyPrice
        {
            get;
            set;
        }

        public Unit  Unit
        {
            get;
            set;
        }
        public int WareHouseStock
        {
            get;
            set;
        }
        public string ImageURL
        {
            get;
            set;
        }
    }
}
