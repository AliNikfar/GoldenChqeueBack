using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Title
        {
            get;
            set;
        }
        public int Price
        {
            get;
            set;
        }
        public int BuyPrice
        {
            get;
            set;
        }

        public UnitDTO Unit
        {
            get;
            set;
        }
        public int WareHouseStock
        {
            get;
            set;
        }
        
        public string ImageExtention
        {
            get;
            set;
        }
    }
}
