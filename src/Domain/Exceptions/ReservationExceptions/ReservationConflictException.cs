using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.ReservationExceptions
{
    public class ReservationConflictException : Exception
    {
        public ReservationConflictException(string message)
            : base(message)
        {
        }

    }
}
