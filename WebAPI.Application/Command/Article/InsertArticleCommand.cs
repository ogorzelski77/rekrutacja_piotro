using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.Dto;
using MediatR;
using WebAPI.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace WebAPI.Application.Commands
{

		public class InsertArticleCommand : IRequest<RequestResult<int>>
		{
			public ArticleInsertDto Article { get; set; }
			
			public InsertArticleCommand(ArticleInsertDto article)
			{
				this.Article = article;
			}

			public class InsertArticleCommandHandler : IRequestHandler<InsertArticleCommand, RequestResult<int>>
			{
				private readonly IArticleService articleService;
				private readonly ILogger<InsertArticleCommand> logger;

				public InsertArticleCommandHandler(IArticleService articleService, ILogger<InsertArticleCommand> logger)
				{
					this.articleService = articleService;
					this.logger = logger;
				}
				public async Task<RequestResult<int>> Handle(InsertArticleCommand request, CancellationToken cancellationToken)
				{
					try
					{
					var result = await articleService.InsertArticle(request.Article);

						return new RequestResult<int>
						{
							Result = result,
							Success = true
						};

					}
					catch (Exception ex)
					{
						return new RequestResult<int>
						{
							Success = false,
							ErrorCode = 400,
							ErrorDesctiption = ex.Message
						};
					}
				}
			}
		}
	}



