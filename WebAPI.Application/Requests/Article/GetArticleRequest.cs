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
	public class GetArticleRequest : IRequest<ArticleResponseDto>
	{

        public int ArticleID { get; set; }
		public class GetArticleRequestHandler : IRequestHandler<GetArticleRequest, ArticleResponseDto>
		{
			private readonly IArticleService ArticleService;
			private readonly ILogger<GetArticleRequestHandler> logger;

			public GetArticleRequestHandler(IArticleService ArticleService, ILogger<GetArticleRequestHandler> logger)
			{
				this.ArticleService = ArticleService;
				this.logger = logger;
			}

			public async Task<ArticleResponseDto> Handle(GetArticleRequest request, CancellationToken cancellationToken)
			{
				var Article = await ArticleService.GetArticle(request.ArticleID);
				return Article;
			}

       
        }
	}
}
