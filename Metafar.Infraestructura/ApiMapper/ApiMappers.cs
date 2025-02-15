using System.Runtime;
using AutoMapper;
using Metafar.Core.Models;
using Metafar.Core.Models.Dtos;

namespace Metafar.Infraestructura;

public class ApiMappers : Profile
{

    public ApiMappers()
    {
        CreateMap<Saldo, SaldoDto>().ReverseMap();
    }

}
