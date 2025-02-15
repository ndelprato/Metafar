using AutoMapper;
using Metafar.Core.Models;
using Metafar.Core.Models.Dtos;
using Metafar.Infraestructura;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/saldo")]
    [ApiController]
    public class SaldosController : ControllerBase
    {
        private readonly ISaldoRepositorio _saldoRepositorio;
        private readonly IMapper _mapper;

        public SaldosController(ISaldoRepositorio saldoRepositorio, IMapper mapper)
        {
            _saldoRepositorio = saldoRepositorio;
            _mapper = mapper;
        }

        [HttpGet("{tarjeta}")]
        public ActionResult<SaldoDto> GetSaldo(string tarjeta)
        {
            var saldo = _saldoRepositorio.GetSaldo(tarjeta);
            if (saldo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SaldoDto>(saldo));
        }

        [HttpPost]
        public ActionResult PostSaldo(SaldoDto saldoDto)
        {
            if (_saldoRepositorio.ExisteSaldo(saldoDto.Tarjeta))
            {
                return BadRequest("Ya existe un saldo para esta tarjeta");
            }
            var saldo = _mapper.Map<Saldo>(saldoDto);
            if (_saldoRepositorio.ActualizarSaldo(saldo))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public ActionResult PutSaldo(SaldoDto saldoDto)
        {
            var saldo = _mapper.Map<Saldo>(saldoDto);
            if (_saldoRepositorio.ActualizarSaldo(saldo))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
