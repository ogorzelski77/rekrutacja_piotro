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

		public class UpdateArticleCommand : IRequest<RequestResult<int>>
		{
			public ArticleUpdateDto Article { get; set; }
			
			public UpdateArticleCommand(ArticleUpdateDto article)
			{
				this.Article = article;
			}

			public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, RequestResult<int>>
			{
				private readonly IArticleService articleService;
				private readonly ILogger<UpdateArticleCommand> logger;

				public UpdateArticleCommandHandler(IArticleService articleService, ILogger<UpdateArticleCommand> logger)
				{
					this.articleService = articleService;
					this.logger = logger;
				}
				public async Task<RequestResult<int>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
				{
					try
					{
					var result = await articleService.UpdateArticle(request.Article);

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



