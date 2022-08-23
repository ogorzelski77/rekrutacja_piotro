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

		public class UpdatePriceCommand : IRequest<RequestResult<int>>
		{
			public PriceUpdateDto Price { get; set; }
			
			public UpdatePriceCommand(PriceUpdateDto Price)
			{
				this.Price = Price;
			}

			public class UpdatePriceCommandHandler : IRequestHandler<UpdatePriceCommand, RequestResult<int>>
			{
				private readonly IPriceService PriceService;
				private readonly ILogger<UpdatePriceCommand> logger;

				public UpdatePriceCommandHandler(IPriceService PriceService, ILogger<UpdatePriceCommand> logger)
				{
					this.PriceService = PriceService;
					this.logger = logger;
				}
				public async Task<RequestResult<int>> Handle(UpdatePriceCommand request, CancellationToken cancellationToken)
				{
					try
					{
					var result = await PriceService.UpdatePrice(request.Price);

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



