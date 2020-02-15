using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HS.CustomerApp.CustomerHost.Models;

namespace HS.CustomerApp.CustomerHost.Logic
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory _httpClientFactory;

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

        public CustomerService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            SeedSampleData();
        }

        public async Task<long> Create(CustomerModel customerModel)
        {
            var idClient = new IdClient.IdClient(_httpClientFactory.CreateClient());
            var id = await idClient.GenerateAsync();
            customerModel.Id = id;
            _data.Add(customerModel);
            return id;
        }

        public IEnumerable<CustomerModel> ReadAll() => _data;

        public CustomerModel Read(long id) => _data.FirstOrDefault(x => x.Id == id);

        public void Update(CustomerModel customerModel) =>
            _data[_data.FindIndex(x => x.Id == customerModel.Id)] = customerModel;

        public void Delete(long id) => _data.RemoveAll(x => x.Id == id);

        private void SeedSampleData()
        {
            _data.AddRange(
                Customers
                    .Select((x, i) => new CustomerModel
                    {
                        Id = i,
                        Name = x.Item1,
                        Location = x.Item2
                    }));
        }
    }
}