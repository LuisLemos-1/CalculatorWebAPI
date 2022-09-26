using CalculatorAPI.Contracts;
using CalculatorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorAPI.Domain 
{
    public class ServiceDomain : IServiceDomain
    {
        private readonly List<CustomerModel> _customers;

        public ServiceDomain()
        {
            _customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    Id = 1,
                    Name = "Jose Manuel",
                    DOB = new DateTime(1980, 7, 3)
                },
                new CustomerModel
                {
                    Id = 2,
                    Name = "Jorge Castro",
                    DOB = new DateTime(1981, 9, 3)
                },
                new CustomerModel
                {
                    Id = 3,
                    Name = "Catarina Costa",
                },
            };
        }

        public IEnumerable<Customer> GetAll()
        {
            List<Customer> customersList = new List<Customer>();

            foreach (var c in _customers)
            {
                customersList.Add(new Customer
                {
                    Id = c.Id,
                    Name = c.Name,   
                    DOB = c.DOB
                });
            }
            return customersList;
        }

        public Customer GetCustomerById(int id)
        {
            var findCostumer = _customers.Where(e => e.Id == id).FirstOrDefault();

            if(findCostumer == null) return null;

            return new Customer
            {
                Id = findCostumer.Id,
                Name = findCostumer.Name,
                DOB = findCostumer.DOB
            };
        }

        public Customer AddCustomer(Customer newCustomer)
        {
            CustomerModel newCustomerModel = new CustomerModel {
                Name = newCustomer.Name,
                DOB = newCustomer.DOB
            };

            try
            {
                newCustomerModel.Id = _customers.Max(e => e.Id) + 1;
                newCustomer.Id = newCustomerModel.Id;
                _customers.Add(newCustomerModel);
                return newCustomer;
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateCustomer(int id, Customer editedCustomer)
        {
            var existingCustomer = _customers.Where(e => e.Id == id).FirstOrDefault();

            if (existingCustomer == null) return false;

            existingCustomer.Name = editedCustomer.Name;
            existingCustomer.DOB = editedCustomer.DOB;

            return true;
        }

        public bool DeleteCustomerById(int id)
        {
            //CustomerModel findCostumer = _customers.Where(e => e.Id == id).FirstOrDefault();
            var findCostumer = _customers.Where(e => e.Id == id).FirstOrDefault();

            if (findCostumer == null)
                return false;

            _customers.Remove(findCostumer);
            return true;
        }

    }
}
