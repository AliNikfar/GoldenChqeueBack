using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract.DTO
{
    public class UpdateCityRequestDTO
    {
        public string Name
        {
            get;
            set;
        }

        public string CityCode
        {
            get;
            set;
        }

        public Guid Ostan
        {
            get;
            set;
        }
    }
}
