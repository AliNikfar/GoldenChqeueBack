using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Domain.Entities
{
    public class Factor : BaseEntity
    {
        public Customer CustomerId { get; set; }
        public Int64 FactorSumPrice { get; set; }
        public int FactorSodDarsad { get; set; }
        public DateTime FactorKharidDate { get; set; }
        public Int64 FactorSumObjectsPrice { get; set; }
        public int Kind { get; set; }
        public Int64 FactorBeforePrice { get; set; }
        public List<FactorObjects> FactorObjectList { get; set; }
        public List<Ghest> GhestList { get; set; }

    }
}
