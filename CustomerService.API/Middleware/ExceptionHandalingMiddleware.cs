using CustomerService.Application.Exceptions;
using CustomerService.Application.Logging;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.API.Middleware
{
    public class ExceptionHandalingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IErrorLogger _logger;


        public ExceptionHandalingMiddleware(RequestDelegate next, IErrorLogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = 422;
                var errors = ex.Errors.Select(x => new
                {
                    x.ErrorMessage,
                    x.PropertyName
                });

                await context.Response.WriteAsJsonAsync(errors);

            }
            catch (UnauthorizedUseCaseExecutionException ex)
            {
                context.Response.StatusCode = 401;

            }
            catch (UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = 401;

            }
            catch (EntityNotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = ex.Message
                });
            }
            catch (CustomerDiscountException ex)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = ex.Message
                });
            }
           
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = ex.Message
                });
            }
     
            catch (Exception ex)
            {
                Guid errorId = Guid.NewGuid();
                AppError error = new AppError
                {
                    Error = ex,
                    ErrorId = errorId,
                    Username = "test"
                };
                _logger.Log(error);

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var responseBody = new
                {
                    message = $"There was an error, please contact support with this error code: {errorId}."
                };

                await context.Response.WriteAsJsonAsync(responseBody);
            }
        }

    }
}
