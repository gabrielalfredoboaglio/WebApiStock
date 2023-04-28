using CodigoComun.Modelos.DTO;
using CodigoComun.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositoController : ControllerBase
    {
        private readonly DepositoService _depositoService;

        public DepositoController()
        {
            _depositoService = new DepositoService();
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerDepositoPorId(int id)
        {
            DepositoDTO depositoDTO = _depositoService.ObtenerDepositoPorId(id);

            if (depositoDTO == null)
            {
                return NotFound();
            }

            return Ok(depositoDTO);
        }

        [HttpGet]
        public IActionResult ObtenerTodosLosDepositos()
        {
            List<DepositoDTO> depositosDTO = _depositoService.ObtenerTodosLosDepositos();

            return Ok(depositosDTO);
        }

        [HttpPost]
        public IActionResult CrearDeposito(DepositoDTO depositoDTO)
        {
            try
            {
                DepositoDTO depositoCreado = _depositoService.AgregarDeposito(depositoDTO);

                return CreatedAtAction(nameof(ObtenerDepositoPorId), new { id = depositoCreado.Id }, depositoCreado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarDeposito(int id, DepositoDTO depositoDTO)
        {
            try
            {
                if (id != depositoDTO.Id)
                {
                    return BadRequest("El id del depósito no coincide con el id en la URL");
                }

                DepositoDTO depositoActualizado = _depositoService.ModificarDeposito(depositoDTO);

                if (depositoActualizado == null)
                {
                    return NotFound();
                }

                return Ok(depositoActualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarDeposito(int id)
        {
            DepositoDTO depositoEliminado = _depositoService.EliminarDeposito(id);

            if (depositoEliminado == null)
            {
                return NotFound();
            }

            return Ok(depositoEliminado);
        }
    }
}
