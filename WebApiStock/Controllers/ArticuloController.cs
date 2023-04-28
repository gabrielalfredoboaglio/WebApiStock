using CodigoComun.Modelos.DTO;
using CodigoComun.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebApiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly ArticuloService _articuloService;

        public ArticuloController()
        {
            _articuloService = new ArticuloService();
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerArticuloPorId(int id)
        {
            ArticuloDTO articuloDTO = _articuloService.GetArticuloPorId(id);

            if (articuloDTO == null)
            {
                return NotFound();
            }

            return Ok(articuloDTO);
        }

        [HttpGet]
        public IActionResult ObtenerTodosLosArticulos()
        {
            List<ArticuloDTO> articulosDTO = _articuloService.ObtenerTodosLosArticulos();

            return Ok(articulosDTO);
        }

        [HttpPost]
        public IActionResult CrearArticulo(ArticuloDTO articuloDTO)
        {
            try
            {
                ArticuloDTO articuloCreado = _articuloService.AgregarArticulo(articuloDTO);

                return CreatedAtAction(nameof(ObtenerArticuloPorId), new { id = articuloCreado.Id }, articuloCreado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarArticulo(int id, ArticuloDTO articuloDTO)
        {
            try
            {
                if (id != articuloDTO.Id)
                {
                    return BadRequest("El id del artículo no coincide con el id en la URL");
                }

                ArticuloDTO articuloActualizado = _articuloService.ActualizarArticulo(articuloDTO);

                if (articuloActualizado == null)
                {
                    return NotFound();
                }

                return Ok(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpDelete("{id}")]
        public IActionResult EliminarArticulo(int id)
        {
            ArticuloDTO articuloEliminado = _articuloService.EliminarArticulo(id);

            if (articuloEliminado == null)
            {
                return NotFound();
            }

            return Ok(articuloEliminado);
        }
    }
}
