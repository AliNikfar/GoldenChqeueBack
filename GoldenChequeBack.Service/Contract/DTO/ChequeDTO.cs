using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract.DTO
{
    public class ChequeDTO
    {
        public int Kind { get; set; }
        public int ShomareHesab { get; set; }
        public int ShomareChek { get; set; }
        public CustomerDTO SahabCheque { get; set; }
        public ShobeDTO Shobe { get; set; }
        public DateTime ChequeDate { get; set; }
        public int ChequeStatus { get; set; }
        public DateTime PassDate { get; set; }
        public string Detail { get; set; }
        public FactorDTO FactorID { get; set; }
        public bool Visable { get; set; }
        public int ChequePrice { get; set; }
    }
}
