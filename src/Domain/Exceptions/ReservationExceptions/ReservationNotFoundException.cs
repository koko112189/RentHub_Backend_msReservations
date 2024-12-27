using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.ReservationExceptions
{
    public class ReservationNotFoundException : Exception
    {
        public ReservationNotFoundException(Guid id)
            : base($"The reservation with ID '{id}' was not found.")
        {
        }
    }
}
