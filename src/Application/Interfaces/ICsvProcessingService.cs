﻿using Application.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICsvProcessingService
    {
        Task<(List<ApplicantDto> SuccessfulRecords, List<(ApplicantDto Record, string Error)> FailedRecords)> ProcessCsvAsync(IFormFile file);
    }
}
