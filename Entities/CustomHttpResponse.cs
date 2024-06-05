using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities;

public class CustomHttpResponse<T>
{
    public T Data { get; set; }
    public int StatusCode { get; set; }

    public CustomHttpResponse(T data, int statusCode)
    {
        Data = data;
        StatusCode = statusCode;
    }
}