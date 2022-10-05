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
using System.Xml.Linq;

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
        [InlineData("Rodrigo Lemos", "2000-01-15")]
        [InlineData("Ana Sampaio", "2010-11-05")]
        [InlineData("Carolina Costa", "2001-03-04")]
        public void Domain_AddCustomer_ReturnCustomer_WhenCustomerIsAddedToList(string name, string dob)
        {
            // Arrange
            var dataContextMock = new Mock<IDataContext>();
            var customerModelList = CreateCustomersList();

            Customer newCustomer = new Customer()
            {
                Id = customerModelList.Count,
                Name = name,
                DOB = DateTime.Parse(dob)
            };

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

            //Assert
            dataContextMock.Verify(m => m.Customers.Add(It.IsAny<CustomerModel>()), Times.Once());
            dataContextMock.Verify(m => m.saveDB(), Times.Once());
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.DOB, actual.DOB);
        }

        [Theory]
        [InlineData(1, "Rodrigo Lemos", "2000-01-15")]
        [InlineData(2, "Ana Sampaio", "2010-11-05")]
        [InlineData(3, "Carolina Costa", "2001-03-04")]
        public void Domain_UpdateCustomer_ReturnTrue_WhenCustomerIsUpdatedSucessfully(int id, string name, string dob)
        {
            // Arrange
            var dataContextMock = new Mock<IDataContext>();
            var customerModelList = CreateCustomersList();

            Customer newCustomer = new Customer()
            {
                Id = id,
                Name = name,
                DOB = DateTime.Parse(dob)
            };

            dataContextMock.Setup(x => x.Customers).ReturnsDbSet(customerModelList);
            //auto mapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperServiceProfile());
            });
            var mapper = config.CreateMapper();
            IServiceDomain domain = new ServiceDomain(mapper, dataContextMock.Object);

            //Act
            var actual = domain.UpdateCustomer(newCustomer.Id, newCustomer);

            //Assert
            dataContextMock.Verify(m => m.saveDB(), Times.Once());
            Assert.True(actual);
        }

        [Theory]
        [InlineData(-1, "Rodrigo Lemos", "2000-01-15")]
        [InlineData(4, "Ana Sampaio", "2010-11-05")]
        [InlineData(6, "Carolina Costa", "2001-03-04")]
        public void Domain_UpdateCustomer_ReturnFalse_WhenCustomerIdNotFound(int id, string name, string dob)
        {
            // Arrange
            var dataContextMock = new Mock<IDataContext>();
            var customerModelList = CreateCustomersList();

            Customer newCustomer = new Customer()
            {
                Id = id,
                Name = name,
                DOB = DateTime.Parse(dob)
            };

            dataContextMock.Setup(x => x.Customers).ReturnsDbSet(customerModelList);
            //auto mapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperServiceProfile());
            });
            var mapper = config.CreateMapper();
            IServiceDomain domain = new ServiceDomain(mapper, dataContextMock.Object);

            //Act
            var actual = domain.UpdateCustomer(newCustomer.Id, newCustomer);

            //Assert
            dataContextMock.Verify(m => m.saveDB(), Times.Never());
            Assert.False(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Domain_DeleteCustomerById_ReturnTrue_WhenCustomerIsRemovedFromList(int id)
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
            var actual = domain.DeleteCustomerById(id);

            //Assert
            dataContextMock.Verify(m => m.Customers.Remove(It.IsAny<CustomerModel>()), Times.Once());
            dataContextMock.Verify(m => m.saveDB(), Times.Once());
            Assert.True(actual);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(0)]
        [InlineData(-1)]
        public void Domain_DeleteCustomerById_ReturnFalse_WhenCustomerIsNotOnList(int id)
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
            var actual = domain.DeleteCustomerById(id);

            //Assert
            dataContextMock.Verify(m => m.Customers.Remove(It.IsAny<CustomerModel>()), Times.Never());
            dataContextMock.Verify(m => m.saveDB(), Times.Never());
            Assert.False(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Domain_GetCustomersByIdProduct_ReturnIdCustomerNameIdOrder_WhenCustomerPurchasedProduct(int idProduct)
        {
            // Arrange
            var dataContextMock = new Mock<IDataContext>();
            var customerModelList = CreateCustomersList();
            dataContextMock.Setup(x => x.Customers).ReturnsDbSet(customerModelList);
            var orderModelList = CreateOrdersList();
            dataContextMock.Setup(x => x.Orders).ReturnsDbSet(orderModelList);
            //auto mapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperServiceProfile());
            });
            var mapper = config.CreateMapper();
            IServiceDomain domain = new ServiceDomain(mapper, dataContextMock.Object);

            //Act
            var actual = domain.GetCustomersByIdProduct(idProduct).ToList();

            var expected =
                (from person in customerModelList
                join order in orderModelList on person.Id equals order.IdCustomer
                where idProduct == order.IdProduct
                select new CustomerByProduct()
                {
                    Id = person.Id,
                    Name = person.Name,
                    IdOrder = order.IdOrder
                }).ToList();

            //Assert
            Assert.Equal(expected.Count, actual.Count);

            expected.ForEach(ex => {
                var act = actual.Where(c => c.IdOrder == ex.IdOrder).FirstOrDefault();
                Assert.NotNull(act);
                Assert.Equal(ex.Id, act.Id);
                Assert.Equal(ex.Name, act.Name);
                Assert.Equal(ex.IdOrder, act.IdOrder);
            });
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

        private List<OrderModel> CreateOrdersList()
        {
            var orderModelList = new List<OrderModel>() {
                new OrderModel()
                {
                    IdOrder = 1,
                    IdCustomer = 1,
                    IdProduct = 1,
                    Date = new DateTime(1991, 01, 30),
                    Quantity = 1
                },
                new OrderModel()
                {
                    IdOrder = 2,
                    IdCustomer = 3,
                    IdProduct = 1,
                    Date = new DateTime(1991, 01, 30),
                    Quantity = 1
                },
                new OrderModel()
                {
                    IdOrder = 3,
                    IdCustomer = 2,
                    IdProduct = 2,
                    Date = new DateTime(1991, 01, 30),
                    Quantity = 1
                },
            };

            return orderModelList;
        }
    }
}
