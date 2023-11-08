using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract.DTO
{
    public class FactorObjectsDTO
    {
        public Int64 Price { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public Int64 Sum { get; set; }
        public Guid[] ObjectsList { get; set; }
        public FactorDTO Factor;
    }
}
