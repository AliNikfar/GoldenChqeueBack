using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract.DTO
{
    internal class CreateProductRequestDTO
    {
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

        public Guid Unit
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
