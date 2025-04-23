using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Common.Results
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Value { get; private set; }
        public string? Message { get; private set; }

        private Result(bool isSuccess, T? value, string? message)
        {
            IsSuccess = isSuccess;
            Value = value;
            Message = message;
        }

        public static Result<T> Success(T value) =>
            new Result<T>(true, value, null);
        
        public static Result<T> Success(T data, string? message = null) =>
            new Result<T>(true, data, message);

        public static Result<T> Success() =>
            new Result<T>(true, default, null);
        

        public static Result<T> Failure(string error) => 
            new Result<T>(false, default, error);
        

        public static Result<T> Failure(string error, T? value) =>
            new Result<T>(false, value, error); // Para devolver un valor con un error (si es necesario)
        
    }
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string? Error { get; private set; }

        private Result(bool isSuccess, string? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
        {
            return new Result(true, null);
        }

        public static Result Failure(string error)
        {
            return new Result(false, error);
        }
    }
}
