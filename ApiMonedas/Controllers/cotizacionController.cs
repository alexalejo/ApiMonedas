using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiMonedas.Models;
using Microsoft.EntityFrameworkCore;
namespace ApiMonedas.Controllers
{
    [Produces("application/json")]
    [Route("api/cotizacion")]
    public class cotizacionController : Controller
    {
        private readonly ApplicationDbContext context;
        public cotizacionController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IEnumerable<Moneda> lecturatotal()
        {
            return context.Moneda.ToList();
        }
        [HttpGet ("{id}", Name ="Moneda Creada")]
        public IActionResult lecturaID(int Id)
        {
            var moneda = context.Moneda.FirstOrDefault(x => x.Id == Id);
            if (moneda == null)
            {
                return NotFound();
            }

            return Ok(moneda);
        }
        [HttpGet("{Divisa}")]
        public IActionResult lecturaDivisa(string Divisa)
        {
            var moneda = context.Moneda.FirstOrDefault(x => x.Divisa == Divisa);
            if (moneda == null)
            {
                return NotFound();
            }

            return Ok(moneda);
        }
        [HttpPost]
        public IActionResult Altadedivisa([FromBody]Moneda moneda)
        { 
            //VALIDAR QUE NO EXISTA MONEDA
            if (ModelState .IsValid) 
            {
                context.Moneda.Add(moneda);
                
                context.SaveChanges();
                return new CreatedAtRouteResult("Moneda Creada", new { id = moneda.Id }, moneda);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public IActionResult modificacionDivisa([FromBody]Moneda moneda, int Id)
        {
            if (moneda.Id!=Id)
            {
                return BadRequest();
            }
            context.Entry(moneda).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult borrarDivisa(int Id)
        {
            var moneda = context.Moneda.FirstOrDefault(x=>x.Id==Id);
            if (moneda==null)
            {
                return NotFound();
            }
            context.Moneda.Remove(moneda); 
            context.SaveChanges();
            return Ok();
        }
    }
}


