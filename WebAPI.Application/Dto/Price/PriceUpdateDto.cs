using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application.Dto
{
    public class PriceUpdateDto
    {
        public int ArticleID { get; set; }
        public int PricelistID { get; set; }
        public decimal Price { get; set; }
      
    }
}
