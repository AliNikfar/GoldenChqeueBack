using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract.DTO
{
    public class UpdateGhestRequestDTO
    {
        public Int64 Price { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        public DateTime PassDate { get; set; }
        public Guid Factor;
    }
}
