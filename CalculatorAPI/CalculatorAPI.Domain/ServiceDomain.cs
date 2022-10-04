using AutoMapper;
using CalculatorAPI.Contracts;
using CalculatorAPI.Data;
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
        private readonly IDataContext _data;
        private readonly IMapper _mapper;

        public ServiceDomain(IMapper mapper, IDataContext data)
        {
            _mapper = mapper;
            _data = data;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _mapper.Map<List<Customer>>(_data.Customers.ToList());
        }

        public Customer GetCustomerById(int id)
        {
            var findCostumer = _data.Customers.Where(e => e.Id == id).FirstOrDefault();

            if (findCostumer == null) return null;

            return _mapper.Map<Customer>(findCostumer);
        }

        public Customer AddCustomer(Customer newCustomer)
        {
            CustomerModel newCustomerModel = new CustomerModel {
                Name = newCustomer.Name,
                DOB = newCustomer.DOB
            };

            try
            {
                _data.Customers.Add(newCustomerModel);
                _data.saveDB();
                //return _mapper.Map<Customer>(newCustomerModel);
                return _mapper.Map<Customer>(_data.Customers.Last());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool UpdateCustomer(int id, Customer editedCustomer)
        {
            //var existingCustomer = _customers.Where(e => e.Id == id).FirstOrDefault();
            var existingCustomer = _data.Customers.Where(e => e.Id == id).FirstOrDefault();

            if (existingCustomer == null) return false;

            existingCustomer.Name = editedCustomer.Name;
            existingCustomer.DOB = editedCustomer.DOB;

            _data.saveDB();
            return true;
        }

        public bool DeleteCustomerById(int id)
        {
            var findCostumer = _data.Customers.Where(e => e.Id == id).FirstOrDefault();

            if (findCostumer == null)
                return false;

            _data.Customers.Remove(findCostumer);
            _data.saveDB();
            return true;
        }

        public IEnumerable<CustomerByProduct> GetCustomersByIdProduct(int id)
        {
            var query = 
                from person in _data.Customers.ToList()
                join order in _data.Orders.ToList() on id equals order.IdProduct
                select new CustomerByProduct()
                {
                    Id = person.Id,
                    Name = person.Name,
                    IdOrder = order.IdOrder
                };

            return query;
        }

    }
}
