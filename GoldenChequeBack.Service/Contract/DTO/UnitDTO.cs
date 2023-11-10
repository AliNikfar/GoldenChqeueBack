using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract.DTO
{
    public class UnitDTO
    {

        public Guid Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }

        public int QuantityPerUnit
        {
            get;
            set;
        }
    }
}
