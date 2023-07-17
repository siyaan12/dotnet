using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDodos.Domain.Wrapper
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message = null)
        {
            Message = message;
            Data = data;
        }
        public Response(string message)
        {
            //Succeeded = false;
            Message = message;
        }
        public Response(string message, int statuscode)
        {
            StatusCode = statuscode;
            Message = message;
        }
        public Response(T data, int statuscode, string message = null)
        {
            StatusCode = statuscode;
            Message = message;
            Data = data;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
