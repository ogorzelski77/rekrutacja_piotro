using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application.Dto
{
    public class ArticleWithPriceResponseDto: ArticleResponseDto
    {     
        public int Price { get; set; }
    }
}
