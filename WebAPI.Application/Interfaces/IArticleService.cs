using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.Dto;

namespace WebAPI.Application.Interfaces
{
	public interface IArticleService
	{
		Task<CollectionInfoDto<ArticleWithPriceResponseDto>> GetArticleList(int pricelistID, int pageSize, int pageNumber);
		Task<ArticleResponseDto> GetArticle(int articleID);
		Task<int> InsertArticle(ArticleInsertDto article);
		Task<int> UpdateArticle(ArticleUpdateDto article);
		Task<int> DeleteArticle(int articleID);
	}
}
