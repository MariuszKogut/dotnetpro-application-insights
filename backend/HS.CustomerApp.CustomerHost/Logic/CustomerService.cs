using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HS.CustomerApp.CustomerHost.Models;
using HS.CustomerApp.IdClient;
using Microsoft.Extensions.Logging;

namespace HS.CustomerApp.CustomerHost.Logic
{
    public class CustomerService : ICustomerService
    {
        private readonly IIdClient _idClient;
        private readonly ILogger<CustomerService> _logger;

        private static readonly (string, string)[] Customers =
        {
            ("Microsoft", "USA"),
            ("Oracle", "USA"),
            ("IBM", "USA"),
            ("SAP", "Germany"),
            ("Synamtec", "USA"),
            ("EMC", "USA"),
            ("VMWare", "USA"),
            ("HP", "USA"),
            ("Salesforce.com", "USA"),
            ("Intuit", "USA")
        };

        private readonly List<CustomerModel> _data = new List<CustomerModel>();

        public CustomerService(IIdClient idClient, ILogger<CustomerService> logger)
        {
            _idClient = idClient;
            _logger = logger;
            SeedSampleData();
        }

        public async Task<long> Create(CustomerModel customerModel)
        {
            var id = await _idClient.GenerateAsync();
            customerModel.Id = id;
            _data.Add(customerModel);
            return id;
        }

        public IEnumerable<CustomerModel> ReadAll() => _data;

        public CustomerModel Read(long id) => _data.FirstOrDefault(x => x.Id == id);

        public void Update(CustomerModel customerModel) =>
            _data[_data.FindIndex(x => x.Id == customerModel.Id)] = customerModel;

        public void Delete(long id)
        {
            _logger.LogInformation("Delete customer with id {id}", id);
            _data.RemoveAll(x => x.Id == id);
        }

        private void SeedSampleData()
        {
            _data.AddRange(
                Customers
                    .Select((x, i) => new CustomerModel
                    {
                        Id = i + 1,
                        Name = x.Item1,
                        Location = x.Item2
                    }));
        }
    }
}