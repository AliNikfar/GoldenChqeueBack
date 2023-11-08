using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Domain.Entities
{
    public class Cheque : BaseEntity
    {
        public int Kind { get; set; }
        public int ShomareHesab { get; set; }
        public int ShomareChek { get; set; }
        public Customer SahabCheque { get; set; }
        public Shobe  Shobe { get; set; }
        public DateTime ChequeDate { get; set; }
        public int ChequeStatus { get; set; }
        public DateTime PassDate { get; set; }
        public string Detail { get; set; }
        public Factor? Factor { get; set; }
        public int ChequePrice { get; set; }

    }
}
