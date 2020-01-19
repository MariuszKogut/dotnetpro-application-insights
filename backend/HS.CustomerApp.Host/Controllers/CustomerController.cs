using System.Collections.Generic;
using HS.CustomerApp.Host.Logic;
using HS.CustomerApp.Host.Models;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType( typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Insert(CustomerModel customerModel) => Ok(_customerService.Create(customerModel));

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType( typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Update(CustomerModel customerModel)
        {
            _customerService.Update(customerModel);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public void Delete(long id) => _customerService.Delete(id);
    }
}