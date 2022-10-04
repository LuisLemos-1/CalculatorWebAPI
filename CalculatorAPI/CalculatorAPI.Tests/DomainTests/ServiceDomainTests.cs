using AutoMapper;
using CalculatorAPI;
using CalculatorAPI.Contracts;
using CalculatorAPI.Data;
using CalculatorAPI.Domain;
using CalculatorAPI.Models;
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
        public void GetAll_GettingAll_WithThreeCustomers()
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
            IEnumerable<Customer> expected = mapper.Map<List<Customer>>(customerModelList);

            //Assert
            Assert.Equal(expected.Count(), actual.Count());

            //Assert.Equal(expected, actual);
            for (int i = 0; i < expected.Count(); i++)
            {
                Assert.Equal(expected.ElementAt(i).Id, actual.ElementAt(i).Id);
                Assert.Equal(expected.ElementAt(i).Name, actual.ElementAt(i).Name);
                Assert.Equal(expected.ElementAt(i).DOB, actual.ElementAt(i).DOB);
            }
        }

        [Fact]
        public void GetAll_GettingNothing()
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
        public void GetCustomerById_Found()
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
        public void GetCustomerById_NotFound()
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
            Assert.True(actual == null);
        }

        [Theory]
        [InlineData(4,"Rodrigo Lemos", "2000-01-15")]
        [InlineData(4, "Ana Sampaio", "2010-11-05")]
        [InlineData(4, "Carolina Costa", "2001-03-04")]
        public void AddCustomer_CustomerAddedSucessfully(int id, string name, string dob)
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
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.DOB, actual.DOB);
            Assert.True(customerModelList.Count + 1 == newCustomersList.Count());
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
