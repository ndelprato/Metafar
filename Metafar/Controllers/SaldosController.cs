using AutoMapper;
using Metafar.Core;
using Metafar.Core.Models;
using Metafar.Core.Models.Dtos;
using Metafar.Infraestructura;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XAct;
using XAct.Library.Settings;

namespace MyApp.Namespace
{
    [Route("api/saldo")]
    [ApiController]
    public class SaldosController : ControllerBase
    {
        private readonly ISaldoRepositorio _saldoRepositorio;
        private readonly IOperacionesRepositorio _operacionesRepositorio;
        private readonly IMapper _mapper;

        public SaldosController(ISaldoRepositorio saldoRepositorio, IMapper mapper, IOperacionesRepositorio operacionesRepositorio)
        {
            _saldoRepositorio = saldoRepositorio;
            _mapper = mapper;
            _operacionesRepositorio = operacionesRepositorio;

        }
        [Authorize]
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

        /*[HttpPost]
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
        }*/
        [Authorize]
        [HttpPatch("{tarjeta}", Name = "RetiroController")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarSaldo(string tarjeta, [FromBody] SaldoMontoDto saldoDto)
        {
            
            

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (saldoDto == null || saldoDto.Monto == null)
            {
                ModelState.AddModelError("", "El monto es requerido");
                return BadRequest(ModelState);
            }
            
            var saldoExiste = _saldoRepositorio.ExisteSaldo(tarjeta);
            if (!saldoExiste)
            {
                return NotFound($"No se encontro saldo con tarjeta {tarjeta}");
            }
            Saldo saldoActual = _saldoRepositorio.GetSaldo(tarjeta);
           Operaciones operaciones = new Operaciones();
            operaciones.Operacion = "Extracción";
            operaciones.Tarjeta = tarjeta;
            
            operaciones.MontoInicial = Convert.ToString(saldoActual.Monto);
            operaciones.Fecha = DateTime.Now;
            var saldoFinal = Convert.ToInt64(saldoActual.Monto) - Convert.ToInt64(saldoDto.Monto);
            operaciones.MontoFinal = Convert.ToString(saldoFinal);
            if (saldoFinal < 0)
            {
                ModelState.AddModelError("", "No se puede retirar más de lo que tiene en la tarjeta");
                return BadRequest(ModelState);
            }
            saldoActual.Monto = saldoFinal.ToString();

            var cambios = _mapper.Map<Saldo>(saldoActual);
            
            
            _operacionesRepositorio.GuardarOperacion(operaciones);

            if (!_saldoRepositorio.ActualizarSaldo(cambios))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {cambios.Tarjeta}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }



    }
}
