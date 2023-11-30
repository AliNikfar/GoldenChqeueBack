using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract.DTO
{
    public  class UpdateProductRequestDTO
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

        public Guid UnitId
        {
            get;
            set;
        }
        public Guid CategoryId
        {
            get;
            set;
        }
        public int WareHouseStock
        {
            get;
            set;
        }
        public Guid? ImageId
        {
            get;
            set;
        }

    }
}
