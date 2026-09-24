using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Domain.Entities
{
    public class Ghest : BaseEntity
    {
        public Int64 Price { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        public DateTime PassDate { get; set; }
        // Foreign key (mapped to the existing FK column)
        public Guid FactorId { get; set; }

        public Factor Factor { get; set; }

    }
}
