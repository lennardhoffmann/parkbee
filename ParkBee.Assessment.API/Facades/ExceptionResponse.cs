using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkBee.Assessment.API.Facades
{
    public static class ExceptionResponse
    {
        /// <summary>
        /// Assign available error attributes to return
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static BadRequestObjectResult RespondWith(Exception ex)
        {
            return new BadRequestObjectResult(
                new
                {
                    success = false,
                    message = ex.Message,
                    extra = ex.InnerException != null ? new
                    {
                        source = ex.InnerException.Source,
                        message = ex.InnerException.Message
                    } : null,
                    codeKey = ex.Data["codeKey"] ?? null,
                    codeAction = ex.Data["codeAction"] ?? null,
                    codeUnique = ex.Data["codeUnique"] ?? null,
                    userId = ex.Data["userId"] ?? null
                });
        }
    }
}
