﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicantRepository ApplicantRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void Rollback();
    }
}
