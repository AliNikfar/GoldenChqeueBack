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
        public Person SahabCheque { get; set; }
        public Shobe  Shobe { get; set; }
        public DateTime ChequeDate { get; set; }
        public int ChequeStatus { get; set; }
        public DateTime PassDate { get; set; }
        public string Detail { get; set; }
        public Factor FactorID { get; set; }
        public bool Visable { get; set; }
        public Int64 ChequePrice { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastEdit { get; set; }
        public int LastHandleUser { get; set; }

    }
}
