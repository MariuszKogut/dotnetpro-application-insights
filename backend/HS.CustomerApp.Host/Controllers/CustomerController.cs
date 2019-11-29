using System.Collections.Generic;
using HS.CustomerApp.Host.Logic;
using HS.CustomerApp.Host.Models;
using Microsoft.AspNetCore.Mvc;

namespace HS.CustomerApp.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IEnumerable<CustomerModel> Get() => _customerService.ReadAll();

        [Route("{id}")]
        [HttpGet]
        public CustomerModel Get(long id) => _customerService.Read(id);

        [HttpPost]
        public void Insert(CustomerModel customerModel) => _customerService.Create(customerModel);

        [HttpPut]
        public void Update(CustomerModel customerModel) => _customerService.Update(customerModel);

        [HttpDelete]
        public void Delete(long id) => _customerService.Delete(id);
    }
}