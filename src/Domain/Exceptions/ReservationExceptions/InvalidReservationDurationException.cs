using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.ReservationExceptions
{
    public class InvalidReservationDurationException : Exception
    {
        public InvalidReservationDurationException(string message)
            : base(message)
        {
        }
    }
}
