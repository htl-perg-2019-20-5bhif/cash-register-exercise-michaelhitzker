using CashRegisterAPI2.Model;
using CashRegisterAPI3.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CashRegisterAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptsController : ControllerBase
    {
        private readonly CashRegisterDbContext DataContext;

        public ReceiptsController(CashRegisterDbContext dataContext)
        {
            DataContext = dataContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]List<ReceiptLineDTO> receiptLineDto)
        {
            var products = new Dictionary<int, Product>();

            var newReceipt = new Receipt
            {
                ReceiptTimestamp = new DateTime().Millisecond,
                ReceiptLines = receiptLineDto.Select(rl => new ReceiptLine
                {
                    Id = 0,
                    Product = products[rl.ProductId],
                    Amount = rl.Amount,
                    TotalPrice = rl.Amount * products[rl.ProductId].UnitPrice
                }).ToList()
            };
            newReceipt.TotalPrice = newReceipt.ReceiptLines.Sum(rl => rl.TotalPrice);

            await DataContext.Receipts.AddAsync(newReceipt);
            await DataContext.SaveChangesAsync();

            return StatusCode((int)HttpStatusCode.Created, newReceipt);
        }
    }
}

