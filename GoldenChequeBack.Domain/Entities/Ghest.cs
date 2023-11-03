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
        

        public int LastHandleUser { get; set; }
        public int Visable { get; set; }
        public int CreateDate { get; set; }

        public int LastEdit { get; set; }
        public Factor Factor;

    }
}
