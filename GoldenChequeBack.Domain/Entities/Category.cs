using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }
        public Guid ParentId { get; set; }
    }
}
