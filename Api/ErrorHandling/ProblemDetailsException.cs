using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.ErrorHandling
{
    public class ProblemDetailsException : Exception
    {
        public ProblemDetailsException(string title, params (string key, object value)[] extensions) :
            this(title, string.Empty, extensions)
        {
        }

        public ProblemDetailsException(string title, string details, params (string key, object value)[] extensions) :
            this(StatusCodes.Status400BadRequest, title, details, extensions)
        {
        }

        public ProblemDetailsException(int statusCode, string title, string details, params (string key, object value)[] extensions)
        {
            ProblemDetails = new ProblemDetails
            {
                Title = title,
                Detail = details,
                Status = statusCode
            };

            foreach (var extension in extensions.Select(tuple => new KeyValuePair<string, object>(tuple.key, tuple.value)))
            {
                ProblemDetails.Extensions.Add(extension);
            }
        }

        public ProblemDetails ProblemDetails { get; }
    }
}