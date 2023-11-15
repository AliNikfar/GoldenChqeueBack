using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly ICustomerRepository _customer;

        public CustomerApiController(ICustomerRepository customer)
        {
            _customer = customer;
        }
        // GET: api/<CustomerApiController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var customer = await _customer.GetAllAsync();


            // Map to DTO
            var response = new List<CustomerDTO>();
            foreach (var crnt in customer)
            {
                response.Add(new CustomerDTO
                {
                    Id = crnt.Id,
                    Name = crnt.Name,
                    Address = crnt.Address,
                    BirthDate = crnt.BirthDate,
                    //City = crnt.City.Id,
                    Code = crnt.Code,
                    //CustomerRate = crnt.CustomerRate.Id,
                    Details = crnt.Details,
                    FatherName = crnt.Details,
                    LastName = crnt.LastName,
                    MaxBuyPrice = crnt.MaxBuyPrice,
                    Mob1 = crnt.Mob1 , 
                    Mob2= crnt.Mob2,
                    Mob3 = crnt.Mob3,
                    PhoneNum = crnt.PhoneNum,
                    PostalCode = crnt.PostalCode
                });
            }
            return Ok(response);
        }

        // GET api/<CustomerApiController>/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingcustomer = await _customer.GetById(id);
            if (existingcustomer is null)
            {
                return NotFound();
            }
            var response = new CustomerDTO
            {
                Id = existingcustomer.Id,
                Name = existingcustomer.Name,
                Address = existingcustomer.Address,
                BirthDate = existingcustomer.BirthDate,
                City = existingcustomer.City.Id,
                Code = existingcustomer.Code,
                CustomerRate = existingcustomer.CustomerRate.Id,
                Details = existingcustomer.Details,
                FatherName = existingcustomer.Details,
                LastName = existingcustomer.LastName,
                MaxBuyPrice = existingcustomer.MaxBuyPrice,
                Mob1 = existingcustomer.Mob1,
                Mob2 = existingcustomer.Mob2,
                Mob3 = existingcustomer.Mob3,
                PhoneNum = existingcustomer.PhoneNum,
                PostalCode = existingcustomer.PostalCode
            };
            return Ok(response);
        }

        // POST api/<CustomerApiController>
        [HttpPost]
        public async Task<IActionResult> Post(CustomerDTO customer)
        {
            //Map DTO
            var cus = new Customer
            {
                Code = customer.Code,
                Name = customer.Name,
                LastName = customer.LastName,
                FatherName = customer.FatherName,
                PhoneNum = customer.PhoneNum,
                Mob1 = customer.Mob1,
                Mob2 = customer.Mob2,   
                Mob3 = customer.Mob3,   
                //City = customer.City,
                Address = customer.Address,
                PostalCode = customer.PostalCode,
                Details = customer.Details,
                MaxBuyPrice = customer.MaxBuyPrice,
                BirthDate = customer.BirthDate,
                //CustomerRate = customer.CustomerRate

            };
            await _customer.InsertAsync(cus);
            var response = new CustomerDTO
            {
                Code = cus.Code,
                Name = cus.Name,
                LastName = cus.LastName,
                FatherName = cus.FatherName,
                PhoneNum = cus.PhoneNum,
                Mob1 = cus.Mob1,
                Mob2 = cus.Mob2,
                Mob3 = cus.Mob3,
                //City = cus.City,
                Address = cus.Address,
                PostalCode = cus.PostalCode,
                Details = cus.Details,
                MaxBuyPrice = cus.MaxBuyPrice,
                BirthDate = cus.BirthDate,
                //CustomerRate = cus.CustomerRate
            };
            return Ok(response);
        }

        // PUT api/<CustomerApiController>/5
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateCustomerRequestDTO request)
        {
            //convert DTO to Domain Model
            var cust = new Customer
            {
                Id = id,
                Code = request.Code,
                Name = request.Name,
                LastName = request.LastName,
                FatherName = request.FatherName,
                PhoneNum = request.PhoneNum,
                Mob1 = request.Mob1,
                Mob2 = request.Mob2,
                Mob3 = request.Mob3,
                //City = request.City.Id,
                Address = request.Address,
                PostalCode = request.PostalCode,
                Details = request.Details,
                MaxBuyPrice = request.MaxBuyPrice,
                BirthDate = request.BirthDate,
                //CustomerRate = request.CustomerRate.Id
            };
            cust = await _customer.UpdateAsync(cust);
            if (cust == null)
            {
                return NotFound();
            }

            var response = new CustomerDTO
            {
                Id = id,
                Code = cust.Code,
                Name = cust.Name,
                LastName = cust.LastName,
                FatherName = cust.FatherName,
                PhoneNum = cust.PhoneNum,
                Mob1 = cust.Mob1,
                Mob2 = cust.Mob2,
                Mob3 = cust.Mob3,
                City = cust.City.Id,
                Address = cust.Address,
                PostalCode = cust.PostalCode,
                Details = cust.Details,
                MaxBuyPrice = cust.MaxBuyPrice,
                BirthDate = cust.BirthDate,
                CustomerRate = cust.CustomerRate.Id
            };

            return Ok(response);
        }

        // DELETE api/<CustomerApiController>/5
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var cust = await _customer.DeleteAsync(id);
            if (cust == null)
            {
                return NotFound();
            }
            var response = new CustomerDTO
            {
                Id = id,
                Code = cust.Code,
                Name = cust.Name,
                LastName = cust.LastName,
                FatherName = cust.FatherName,
                PhoneNum = cust.PhoneNum,
                Mob1 = cust.Mob1,
                Mob2 = cust.Mob2,
                Mob3 = cust.Mob3,
                City = cust.City.Id,
                Address = cust.Address,
                PostalCode = cust.PostalCode,
                Details = cust.Details,
                MaxBuyPrice = cust.MaxBuyPrice,
                BirthDate = cust.BirthDate,
                CustomerRate = cust.CustomerRate.Id
            };
            return Ok(response);
        }
    }
}
