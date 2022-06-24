using Core.Utilities.Messages;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";
            IEnumerable<ValidationFailure> errors;
            if (e.GetType() == typeof(ValidationException))
            {
                message = e.Message;
                errors = ((ValidationException)e).Errors;
                httpContext.Response.StatusCode = 400;

                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    StatusCode = 400,
                    Message = message,
                    Errors = errors 
                }.ToString());

            }

            if (e.GetType() == typeof(LoginRequiredException))
            {
                message = e.Message;
                httpContext.Response.StatusCode = 401;

                return httpContext.Response.WriteAsync(new LoginRequiredErrorDetails
                {
                    StatusCode = 401,
                    Message = message,
                }.ToJson());
            }

            if (e.GetType() == typeof(AuthorizationDeniedException))
            {
                message = e.Message;
                httpContext.Response.StatusCode = 401;

                return httpContext.Response.WriteAsync(new AuthorizationDeniedErrorDetails
                {
                    StatusCode = 401,
                    Message = message,
                }.ToJson());
            }



            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());

        }
    }
}
