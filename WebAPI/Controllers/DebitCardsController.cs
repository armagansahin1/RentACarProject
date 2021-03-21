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
    public class DebitCardsController : ControllerBase
    {
        private IDebitCardService _debitCardService;

        public DebitCardsController(IDebitCardService debitCardService)
        {
            _debitCardService = debitCardService;
        }

        [HttpPost("verify")]
        public IActionResult VerifyPayment(DebitCard debitCard)
        {
            var result = _debitCardService.VerifyPayment(debitCard);
            if (result.Success)
            {
                return Ok(result);
            }
            return Ok(result);
        }
    }
}
