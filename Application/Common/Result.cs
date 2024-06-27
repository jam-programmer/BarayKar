using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class Result
    {
        public bool IsSuccess { set; get; }
        public List<string>? Message { set; get; } = new();
        public object? Data { set; get; }

        public static Result Success(object? Data = null)
        {
            Result result = new();
            result.IsSuccess = true;
            if (Data != null)
            {
                result.Data = Data;
            }
            return result;
        }

        public static Result Fail(string? message = null)
        {
            Result result = new();
            result.IsSuccess = false;
            if (!string.IsNullOrEmpty(message))
            {
                result.Message!.Add(message);
            }
            return result;
        }

    }
}
