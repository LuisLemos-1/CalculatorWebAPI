using AutoMapper;
using CalculatorAPI.Contracts;
using CalculatorAPI.Models;

namespace CalculatorAPI
{
    public class AutoMapperServiceProfile : Profile
    {
        public AutoMapperServiceProfile()
        {
            CreateMap<CustomerModel, Customer>();
        }
    }
}
