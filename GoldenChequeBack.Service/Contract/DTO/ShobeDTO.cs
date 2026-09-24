using System;

namespace GoldenChequeBack.Service.Contract.DTO
{
    public class ShobeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Details { get; set; }
        public Guid? BankId { get; set; }
    }
}
