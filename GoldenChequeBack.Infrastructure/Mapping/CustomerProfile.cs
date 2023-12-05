using AutoMapper;
using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract.DTO;

namespace GoldenChequeBack.Infrastructure.Mapping
{
    public  class CustomerProfile :Profile
    {
        public CustomerProfile()
        {
            //(dest => dest.Id,
            // opt => opt.MapFrom(src => src.CustomerId)

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CustomerDTO, Customer>();
            });

            IMapper mapper = config.CreateMapper();
            var source = new CustomerDTO();
            var dest = mapper.Map<CustomerDTO, Customer>(source);
        }
    }
}
