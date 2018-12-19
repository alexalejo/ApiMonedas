using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiMonedas.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace ApiMonedas.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>//DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Moneda> Moneda { get; set; }
    }
}