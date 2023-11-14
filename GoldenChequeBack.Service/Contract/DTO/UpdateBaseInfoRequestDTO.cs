using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract.DTO
{
    public class UpdateBaseInfoRequestDTO
    {
        public string Title { get; set; }
        public string StringValue { get; set; }
        public int? IntValue { get; set; }
        public Int64? LongValue { get; set; }
        public string Detail { get; set; }
        public bool? BoolValue { get; set; }
    }
}
