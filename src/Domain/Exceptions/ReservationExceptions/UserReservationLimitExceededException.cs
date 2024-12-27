using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.ReservationExceptions
{
    public class UserReservationLimitExceededException : Exception
    {
        public UserReservationLimitExceededException(string message)
           : base(message)
        {
        }
    }
}
