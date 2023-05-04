using CodigoComun.Modelos.DTO;
using CodigoComun.Negocio;
using CodigoComun.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly StockService _stockService;

        public StockController()
        {
            StockRepository stockRepository = new StockRepository();
            _stockService = new StockService(stockRepository);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerStockPorId(int id)
        {
            StockDTO stockDTO = _stockService.ObtenerStockPorId(id);

            if (stockDTO == null)
            {
                return NotFound();
            }

            return Ok(stockDTO);
        }

        [HttpGet]
        public IActionResult ObtenerTodosLosStocks()
        {
            List<StockDTO> stocksDTO = _stockService.ObtenerTodosLosStocks();

            return Ok(stocksDTO);
        }

        [HttpPost]
        public IActionResult CrearStock(StockDTO stockDTO)
        {
            try
            {
                StockDTO stockCreado = _stockService.AgregarStock(stockDTO);

                return CreatedAtAction(nameof(ObtenerStockPorId), new { id = stockCreado.Id }, stockCreado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("IngresarStock")]
        public IActionResult IngresarStock([FromBody] IngresoStockDTO ingresoStockDTO)
        {
            try
            {
                var result = _stockService.IngresarStock(ingresoStockDTO.CodigoArticulo, (int)ingresoStockDTO.IdDeposito, (decimal)ingresoStockDTO.Cantidad);

                if (result.HuboError)
                {
                    return BadRequest(result.Mensaje);
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("EgresoStock")]
        public IActionResult EgresoStock([FromBody] EgresoStockDTO egresoStockDTO)
        {
            try
            {
                var result = _stockService.EgresoStock(egresoStockDTO.CodigoArticulo, (int)egresoStockDTO.IdDeposito, (decimal)egresoStockDTO.Cantidad);

                if (result.HuboError)
                {
                    return BadRequest(result.Mensaje);
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public IActionResult ActualizarStock(int id, StockDTO stockDTO)
        {
            try
            {
                if (id != stockDTO.Id)
                {
                    return BadRequest("El id del stock no coincide con el id en la URL");
                }

                StockDTO stockActualizado = _stockService.ActualizarStock(stockDTO);

                if (stockActualizado == null)
                {
                    return NotFound();
                }

                return Ok(stockActualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarStock(int id)
        {
            StockDTO stockEliminado = _stockService.EliminarStock(id);

            if (stockEliminado == null)
            {
                return NotFound();
            }

            return Ok(stockEliminado);
        }
    }
}
