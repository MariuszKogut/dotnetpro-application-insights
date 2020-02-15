using System.Collections.Generic;
using System.Threading.Tasks;
using HS.CustomerApp.CustomerHost.Logic;
using HS.CustomerApp.CustomerHost.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ApplicationInsights;

namespace HS.CustomerApp.CustomerHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly TelemetryClient _telemetryClient;

        public CustomerController(ICustomerService customerService, TelemetryClient telemetryClient)
        {
            _customerService = customerService;
            _telemetryClient = telemetryClient;
        }

        [HttpGet]
        public IEnumerable<CustomerModel> Get() => _customerService.ReadAll();

        [Route("{id}")]
        [HttpGet]
        public CustomerModel Get(long id) => _customerService.Read(id);

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert(CustomerModel customerModel)
        {
            var id = await _customerService.Create(customerModel);
            Track("Customer.Update", customerModel.Id);
            return Ok(id);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult Update(CustomerModel customerModel)
        {
            _customerService.Update(customerModel);
            Track("Customer.Update", customerModel.Id);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public void Delete(long id)
        {
            _customerService.Delete(id);
            Track("Customer.Delete", id);
        }

        private void Track(string eventName, long id)
        {
            _telemetryClient.TrackEvent(eventName, new Dictionary<string, string> {{"customer-id", id.ToString()}});
        }
    }
}