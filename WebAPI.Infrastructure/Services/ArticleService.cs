using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.Dto;
using WebAPI.Application.Interfaces;
using Mapster;


namespace WebAPI.Infrastructure.Services
{

    public class ArticleService : IArticleService
    {
        private string connectionString;
        private ILogger<ArticleService> logger;
        private readonly IServiceProvider serviceProvider;
        private readonly IConfiguration configuration;




        public ArticleService(ILogger<ArticleService> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<CollectionInfoDto<ArticleWithPriceResponseDto>> GetArticleList(int pricelistID, int pageSize, int pageNumber)
        {
            var model = new CollectionInfoDto<ArticleWithPriceResponseDto>();
            using var connection = new SqlConnection(connectionString);
            {
                var procedure = "GetArticleListWithPrice";
                var values = new { PricelistID = pricelistID, PageSize = pageSize, PageNumber = pageNumber };
                var results = await connection.QueryAsync(procedure, values, commandType: CommandType.StoredProcedure);

                if (results?.Count() > 0)
                {
                    model.Count = results.Count();
                    model.TotalRows = results.First().TotalRows;
                    model.Pages = (int)Math.Ceiling((double)results.First().TotalRows / (double)pageSize);
                    model.Collection = results.Adapt<List<ArticleWithPriceResponseDto>>();

                }
                return model;
            }
        }

        public async Task<ArticleResponseDto> GetArticle(int articleID)
        {
           
            using var connection = new SqlConnection(connectionString);
            {
                var procedure = "GetArticle";
                var values = new { ArticleID = articleID };
                var result =  await connection.QueryFirstOrDefaultAsync<ArticleResponseDto>(procedure, values, commandType: CommandType.StoredProcedure);
                var model = result.Adapt<ArticleResponseDto>();
                return model;
            }
        }
        public async Task<int> InsertArticle(ArticleInsertDto article)
        {

            using var connection = new SqlConnection(connectionString);
            {
                var procedure = "InsertUpdateArticle";
                var values = new { Name = article.Name};
                var result = await connection.ExecuteScalarAsync<int>(procedure, values, commandType: CommandType.StoredProcedure);
              
                return result;
            }
        }
        public async Task<int> UpdateArticle(ArticleUpdateDto article)
        {

            using var connection = new SqlConnection(connectionString);
            {
                var procedure = "InsertUpdateArticle";
                var values = new { ArticleID = article.ArticleID, Name = article.Name };
                var result = await connection.ExecuteScalarAsync<int>(procedure, values, commandType: CommandType.StoredProcedure);
            
                return result;
            }
        }
        public async Task<int> DeleteArticle(int articleID)
        {

            using var connection = new SqlConnection(connectionString);
            {
                var procedure = "DeleteArticle";
                var values = new { ArticleID = articleID };
                var result = await connection.ExecuteScalarAsync<int>(procedure, values, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
