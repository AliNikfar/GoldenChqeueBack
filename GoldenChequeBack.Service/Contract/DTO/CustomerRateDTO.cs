using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract.DTO
{
    public class CustomerRateDTO
    {
        public Guid Id { get; set; }
        public string Title
        {
            get;
            set;
        }
    }
}
