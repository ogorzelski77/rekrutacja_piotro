using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.Dto;
using MediatR;
using WebAPI.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace WebAPI.Application.Requests
{
	public class GetArticleListRequest : IRequest<CollectionInfoDto<ArticleWithPriceResponseDto>>
	{

        public int PricelistID { get; set; }
        public int PageSize { get; set; }
		public int PageNumber { get; set; }
		public class GetArticleListRequestHandler : IRequestHandler<GetArticleListRequest, CollectionInfoDto<ArticleWithPriceResponseDto>>
		{
			private readonly IArticleService articleService;
			private readonly ILogger<GetArticleListRequestHandler> logger;

			public GetArticleListRequestHandler(IArticleService articleService, ILogger<GetArticleListRequestHandler> logger)
			{
				this.articleService = articleService;
				this.logger = logger;
			}

			public async Task<CollectionInfoDto<ArticleWithPriceResponseDto>> Handle(GetArticleListRequest request, CancellationToken cancellationToken)
			{
				var articles = await articleService.GetArticleList(request.PricelistID, request.PageSize, request.PageNumber);
				return articles;
			}

       
        }
	}
}
