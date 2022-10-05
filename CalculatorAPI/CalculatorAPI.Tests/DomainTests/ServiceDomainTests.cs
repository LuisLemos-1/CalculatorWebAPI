using AutoMapper;
using CalculatorAPI;
using CalculatorAPI.Contracts;
using CalculatorAPI.Data;
using CalculatorAPI.Domain;
using CalculatorAPI.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorAPI.Tests.DomainTests
{
    public class ServiceDomainTests
    {
        [Fact]
        public void Domain_GetAll_ReturnAllCustomers_WhenNotEmpty()
        {
            // Arrange
            var dataContextMock = new Mock<IDataContext>();
            var customerModelList = CreateCustomersList();
            dataContextMock.Setup(x => x.Customers).ReturnsDbSet(customerModelList);

            //auto mapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperServiceProfile());
            });
            var mapper = config.CreateMapper();
            IServiceDomain domain = new ServiceDomain(mapper, dataContextMock.Object);

            //Act
            var actual = domain.GetAll();
            List<Customer> expected = mapper.Map<List<Customer>>(customerModelList);

            //Assert
            Assert.Equal(expected.Count(), actual.Count());

            //Assert.Equal(expected, actual);
            expected.ForEach(x => {
                var customer = actual.Where(c => c.Id == x.Id).FirstOrDefault();
                Assert.NotNull(customer);
                Assert.Equal(x.Id, customer.Id);
                Assert.Equal(x.Name, customer.Name);
                Assert.Equal(x.DOB, customer.DOB);
            });
        }

        [Fact]
        public void Domain_GetAll_ReturnEmptyList_WhenNoCustomers()
        {
            // Arrange
            var dataContextMock = new Mock<IDataContext>();
            IEnumerable<CustomerModel> customerModelList = new List<CustomerModel>();
            dataContextMock.Setup(x => x.Customers).ReturnsDbSet(customerModelList);

            //auto mapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperServiceProfile());
            });
            var mapper = config.CreateMapper();
            IServiceDomain domain = new ServiceDomain(mapper, dataContextMock.Object);

            //Act
            var actual = domain.GetAll();
            IEnumerable<Customer> expected = mapper.Map<List<Customer>>(customerModelList);

            //Assert
            Assert.Empty(expected);
            Assert.Empty(actual);
        }

        [Fact]
        public void Domain_GetCustomerById_ReturnFoundCustomer_WhenFoundOnList()
        {
            // Arrange
            var customerToFind = new CustomerModel()
            {
                Id = 1,
                Name = "Jose Santos",
                DOB = new DateTime(1990, 02, 22)
            };

            var dataContextMock = new Mock<IDataContext>();
            var customerModelList = CreateCustomersList();
            dataContextMock.Setup(x => x.Customers).ReturnsDbSet(customerModelList);
            //auto mapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperServiceProfile());
            });
            var mapper = config.CreateMapper();
            IServiceDomain domain = new ServiceDomain(mapper, dataContextMock.Object);

            //Act
            var actual = domain.GetCustomerById(customerToFind.Id);
            var expected = mapper.Map<Customer>(customerToFind);

            //Assert
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.DOB, actual.DOB);
        }

        [Fact]
        public void Domain_GetCustomerById_ReturnNullCustomer_WhenNotFoundOnList()
        {
            // Arrange
            var dataContextMock = new Mock<IDataContext>();
            var customerModelList = CreateCustomersList();
            dataContextMock.Setup(x => x.Customers).ReturnsDbSet(customerModelList);
            //auto mapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperServiceProfile());
            });
            var mapper = config.CreateMapper();
            IServiceDomain domain = new ServiceDomain(mapper, dataContextMock.Object);

            //Act
            var actual = domain.GetCustomerById(customerModelList.Count() + 1);

            //Assert
            Assert.Null(actual);
        }

        [Theory]
        [InlineData(4,"Rodrigo Lemos", "2000-01-15")]
        [InlineData(4, "Ana Sampaio", "2010-11-05")]
        [InlineData(4, "Carolina Costa", "2001-03-04")]
        public void Domain_AddCustomer_ReturnCustomer_WhenCustomerIsAddedToList(int id, string name, string dob)
        {
            // Arrange
            Customer newCustomer = new Customer()
            {
                Id = id,
                Name = name,
                DOB = DateTime.Parse(dob)                
            };
        
            var dataContextMock = new Mock<IDataContext>();
            var customerModelList = CreateCustomersList();
            dataContextMock.Setup(x => x.Customers).ReturnsDbSet(customerModelList);
            //auto mapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperServiceProfile());
            });
            var mapper = config.CreateMapper();
            IServiceDomain domain = new ServiceDomain(mapper, dataContextMock.Object);

            //Act
            var actual = domain.AddCustomer(newCustomer);
            var expected = newCustomer;
            var newCustomersList = domain.GetAll();

            //Assert
            dataContextMock.Verify(m => m.Customers.Add(It.IsAny<CustomerModel>()), Times.Once());
            dataContextMock.Verify(m => m.saveDB(), Times.Once());
            //Assert.Equal(expected.Id, actual.Id);
            //Assert.Equal(expected.Name, actual.Name);
            //Assert.Equal(expected.DOB, actual.DOB);
            //Assert.True(customerModelList.Count + 1 == newCustomersList.Count());
        }


        private List<CustomerModel> CreateCustomersList()
        {
            var customerModels = new List<CustomerModel>() {
                new CustomerModel()
                {
                    Id = 1,
                    Name = "Jose Santos",
                    DOB = new DateTime(1990, 02, 22)
                },
                new CustomerModel()
                {
                    Id = 2,
                    Name = "Ana Ferreira",
                    DOB = new DateTime(1993, 11, 13)
                },
                new CustomerModel()
                {
                    Id = 3,
                    Name = "Andre Correia",
                    DOB = new DateTime(1991, 01, 30)
                }
            };

            return customerModels;
        }
    }
}
