using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.Dto;

namespace WebAPI.Application.Interfaces
{
	public interface IPriceService
	{
		Task<List<PriceResponseDto>> GetPrice(int ArticleID);
		Task<int> UpdatePrice(PriceUpdateDto price);
	}
}
