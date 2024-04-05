using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Exceptions;
public class ConflictException : PassInException
{
    public ConflictException(string message) : base(message)
    {
    }
}
