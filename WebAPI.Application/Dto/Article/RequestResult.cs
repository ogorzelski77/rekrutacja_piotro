using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application.Dto
{
    public class RequestResult<T>
    {
        public RequestResult()
        {
        }
        public RequestResult(bool success, int code, string message)
        {
            Success = success;
            ErrorCode = code;
            ErrorDesctiption = message;
        }
        public bool Success { get; set; }
        public T Result { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorDesctiption { get; set; }
    }
}
