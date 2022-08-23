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
    public class ArticleController : ControllerBase
    {

        private readonly IMediator mediator;
        private readonly ILogger<ArticleController> logger;


        public ArticleController(IMediator mediator, ILogger<ArticleController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAsync([FromQuery] int pricelistID, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            try
            {
                logger.LogInformation($" GetArticleList {pageSize}, {pageNumber}");

                var result = await mediator.Send(new GetArticleListRequest() { PricelistID = pricelistID, PageSize = pageSize, PageNumber = pageNumber});
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("{articleID}")]
        public async Task<IActionResult> GetOneAsync(int articleID)
        {
            try
            {
                logger.LogInformation($" GetArticle {articleID}");

                var result = await mediator.Send(new GetArticleRequest() { ArticleID = articleID });
                if (result != null)
                    return Ok(result);
                else return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ArticleInsertDto article)
        {
            try
            {
                logger.LogInformation(@$" Post Article");

                var result = await mediator.Send(new InsertArticleCommand(article));
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ArticleUpdateDto article)
        {
            try
            {
                logger.LogInformation(@$" Put Article");

                var result = await mediator.Send(new UpdateArticleCommand(article));
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int articleID)
        {
            try
            {
                logger.LogInformation(@$" Delete Article");

                var result = await mediator.Send(new DeleteArticleCommand(articleID));
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
