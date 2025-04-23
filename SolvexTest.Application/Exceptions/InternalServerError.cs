using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvexTest.Application.Exceptions
{
    public class InternalServerError : ApplicationException
    {
        public InternalServerError(string message)
            : base(message)
        {
        }

        public InternalServerError(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}