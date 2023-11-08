using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Domain.Entities
{
    public class Bank:BaseEntity
    {
        public string Title { get; set; }

        public ICollection<Shobe> ShobeList {get; set;}
    }
}
