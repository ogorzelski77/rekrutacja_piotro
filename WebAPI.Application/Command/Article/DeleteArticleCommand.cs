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

		public class DeleteArticleCommand : IRequest<RequestResult<int>>
		{
			public int ArticleID { get; set; }
			
			public DeleteArticleCommand(int articleID)
			{
				this.ArticleID = articleID;
			}

			public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, RequestResult<int>>
			{
				private readonly IArticleService articleService;
				private readonly ILogger<DeleteArticleCommand> logger;

				public DeleteArticleCommandHandler(IArticleService articleService, ILogger<DeleteArticleCommand> logger)
				{
					this.articleService = articleService;
					this.logger = logger;
				}
				public async Task<RequestResult<int>> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
				{
					try
					{
					var result = await articleService.DeleteArticle(request.ArticleID);

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



