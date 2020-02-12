using CashRegisterAPI2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashRegisterAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly CashRegisterDbContext DataContext;

        public ProductsController(CashRegisterDbContext dataContext)
        {
            DataContext = dataContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            IQueryable<Product> products = DataContext.Products;

            return await products.ToListAsync();
        }
    }
}
