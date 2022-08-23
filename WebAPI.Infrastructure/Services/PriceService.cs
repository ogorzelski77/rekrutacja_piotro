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

    public class PriceService : IPriceService
    {
        private string connectionString;
        private ILogger<PriceService> logger;
        private readonly IServiceProvider serviceProvider;
        private readonly IConfiguration configuration;




        public PriceService(ILogger<PriceService> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }
       

        public async Task<List<PriceResponseDto>> GetPrice(int articleID)
        {
           
            using var connection = new SqlConnection(connectionString);
            {
                var procedure = "GetPrices";
                var values = new { ArticleID = articleID };
                var result =  await connection.QueryAsync<PriceResponseDto>(procedure, values, commandType: CommandType.StoredProcedure);
                var model = result.Adapt<List<PriceResponseDto>>();
                return model;
            }
        }
      
        public async Task<int> UpdatePrice(PriceUpdateDto price)
        {

            using var connection = new SqlConnection(connectionString);
            {
                var procedure = "UpdatePrice";
                var values = new { ArticleID = price.ArticleID, PricelistID = price.PricelistID, Price = price.Price };
                var result = await connection.ExecuteScalarAsync<int>(procedure, values, commandType: CommandType.StoredProcedure);
            
                return result;
            }
        }
       
    }
}
