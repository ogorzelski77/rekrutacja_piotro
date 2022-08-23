using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application.Dto
{
    public class ArticleResponseDto
    {
        public int ArticleID { get; set; }
        public string Name { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
