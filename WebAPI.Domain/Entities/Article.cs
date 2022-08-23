using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain.Entities
{
    public class Article
{
    public int ArticleID { get; set; }

    public string Name { get; set; }

    public DateTime InsertionTime { get; set; }

    public DateTime? UpdateTime { get; set; }

}
}
