using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardsController : ControllerBase
    {
        private ICreditCardService _creditCardService;

        public CreditCardsController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpPost("verify")]
        public IActionResult VerifyPayment(CreditCard creditCard)
        {
            var result = _creditCardService.VerifyPayment(creditCard);
            if (result.Success)
            {
                return Ok(result);
            }
            return Ok(result);
        }
    }
}
