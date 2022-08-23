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
	public class GetPriceRequest : IRequest<List<PriceResponseDto>>
	{

        public int ArticleID { get; set; }
        public class GetPriceRequestHandler : IRequestHandler<GetPriceRequest, List<PriceResponseDto>>
		{
			private readonly IPriceService PriceService;
			private readonly ILogger<GetPriceRequestHandler> logger;

			public GetPriceRequestHandler(IPriceService PriceService, ILogger<GetPriceRequestHandler> logger)
			{
				this.PriceService = PriceService;
				this.logger = logger;
			}

			public async Task<List<PriceResponseDto>> Handle(GetPriceRequest request, CancellationToken cancellationToken)
			{
				var Price = await PriceService.GetPrice(request.ArticleID);
				return Price;
			}

       
        }
	}
}
