using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Application.Commands;
using WebAPI.Application.Dto;
using WebAPI.Application.Requests;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {

        private readonly IMediator mediator;
        private readonly ILogger<PriceController> logger;


        public PriceController(IMediator mediator, ILogger<PriceController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        
        [HttpGet]
       
        public async Task<IActionResult> GetPrices(int articleID)
        {
            try
            {
                logger.LogInformation($" GetPrices {articleID}");

                var result = await mediator.Send(new GetPriceRequest() { ArticleID = articleID});
                if (result != null)
                    return Ok(result);
                else return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PriceUpdateDto price)
        {
            try
            {
                logger.LogInformation(@$" Put Price");

                var result = await mediator.Send(new UpdatePriceCommand(price));
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
