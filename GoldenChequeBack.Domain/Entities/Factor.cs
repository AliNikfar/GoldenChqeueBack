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
        // Foreign key (mapped to the existing FK column)
        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }
        public Int64 FactorSumPrice { get; set; }
        public int FactorSodDarsad { get; set; }
        public DateTime FactorKharidDate { get; set; }
        public Int64 FactorSumObjectsPrice { get; set; }
        public int Kind { get; set; }
        public Int64 FactorBeforePrice { get; set; }
        public ICollection<FactorObjects> FactorObjectList { get; set; }
        public ICollection<Ghest> GhestList { get; set; }

    }
}
