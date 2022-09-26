using CalculatorAPI.Contracts;
using CalculatorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorAPI.Domain 
{
    internal class ServiceDomain : IServiceDomain
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
                    Id=c.Id,
                    Name=c.Name,   
                    DOB = c.DOB
                });
            }
            return customersList;
        }

        public Customer GetCustomerById(int id)
        {
            CustomerModel findCostumer = _customers.Find(e => e.Id == id);

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
                _customers.Add(newCustomerModel);
                return newCustomer;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool UpdateCustomer(int id, Customer editedCustomer)
        {
            int idx = _customers.FindIndex(e => e.Id == id);
            if (idx == -1)
                return false;

            _customers[idx].Name = editedCustomer.Name;
            _customers[idx].DOB = editedCustomer.DOB;
            return true;
        }

        public bool DeleteCustomerById(int id)
        {
            CustomerModel findCostumer = _customers.Find(e => e.Id == id);
            if (findCostumer == null)
                return false;

            _customers.Remove(findCostumer);
            return true;
        }

    }
}
