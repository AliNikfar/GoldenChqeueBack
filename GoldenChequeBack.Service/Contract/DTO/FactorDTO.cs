using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract.DTO
{
    public class FactorDTO
    {
        public CustomerDTO PersonCode { get; set; }
        public Int64 FactorSumPrice { get; set; }
        public int FactorSodDarsad { get; set; }
        public bool Visable { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastEdit { get; set; }
        public int LastHandleUser { get; set; }
        public DateTime FactorKharidDate { get; set; }
        public Int64 FactorSumObjectsPrice { get; set; }
        public int Kind { get; set; }
        public Int64 FactorBeforePrice { get; set; }
        public List<FactorObjectsDTO> FactorObjectList { get; set; }
        public List<GhestDTO> GhestList { get; set; }
    }
}
