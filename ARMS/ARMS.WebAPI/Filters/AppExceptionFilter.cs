////using ARMS.Application.Exceptions;
////using ARMS.Application.Utilities;
////using ARMS.Core.Models;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.AspNetCore.Mvc.Filters;
////using System.Net;

////namespace ARMS.WebAPI.Filters
////{
////    public class AppExceptionFilter : IAsyncExceptionFilter
////    {
////        private readonly ILogger<AppExceptionFilter> _logger;

////        public AppExceptionFilter(ILogger<AppExceptionFilter> logger)
////        {
////            _logger = logger;
////        }
////        public async Task OnExceptionAsync(ExceptionContext context)
////        {
////            if (!context.ActionDescriptor.IsControllerAction())
////            {
////                return;
////            }
////            await HandleAndWrapException(context);
////        }

////        private async Task HandleAndWrapException(ExceptionContext context)
////        {
////            //if (!ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
////            //{
////            //    return;
////            //}

////            context.HttpContext.Response.StatusCode = GetStatusCode(context);

////            context.Result = new ObjectResult(new ErrorInfo
////            {
////                Status = GetStatusCode(context),
////                Title = context.Exception.Message,
////                Errors = AppException.GetTrueExceptionMessage(context.Exception)
////            });

////            if (GetStatusCode(context) == 500)
////            {
////                _logger.LogError(context.Exception, context.Exception.Message);
////            }
////        }
////        /// <summary>
////        /// To get status code from exception
////        /// </summary>
////        /// <param name="context"></param>
////        /// <returns></returns>
////        protected virtual int GetStatusCode(ExceptionContext context)
////        {
////            if (context.Exception is AppAuthorizationException)
////            {
////                return context.HttpContext.User.Identity.IsAuthenticated
////                    ? (int)HttpStatusCode.Forbidden
////                    : (int)HttpStatusCode.Unauthorized;
////            }

////            if (context.Exception is AppValidationException)
////            {
////                return (int)HttpStatusCode.BadRequest;
////            }

////            if (context.Exception is FluentValidation.ValidationException)
////            {
////                return (int)HttpStatusCode.BadRequest;
////            }

////            if (context.Exception is AppEntityNotFoundException)
////            {
////                return (int)HttpStatusCode.NotFound;
////            }

////            return (int)HttpStatusCode.InternalServerError;
////        }
////    }

////}



////using ARMS.Application.Exceptions;
////using ARMS.Application.Utilities;
////using ARMS.Core.Models;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.AspNetCore.Mvc.Filters;
////using Microsoft.Extensions.Logging;
////using System.Net;

////namespace ARMS.WebAPI.Filters
////{
////    public class AppExceptionFilter : IAsyncExceptionFilter
////    {
////        private readonly ILogger<AppExceptionFilter> _logger;

////        public AppExceptionFilter(ILogger<AppExceptionFilter> logger)
////        {
////            _logger = logger;
////        }

////        public async Task OnExceptionAsync(ExceptionContext context)
////        {
////            if (!context.ActionDescriptor.IsControllerAction())
////            {
////                return;
////            }

////            await HandleAndWrapException(context);
////        }

////        private async Task HandleAndWrapException(ExceptionContext context)
////        {
////            context.HttpContext.Response.StatusCode = GetStatusCode(context);

////            context.Result = new ObjectResult(new ErrorInfo
////            {
////                Status = GetStatusCode(context),
////                Title = context.Exception.Message,
////                Errors = AppException.GetTrueExceptionMessage(context.Exception)
////            });

////            // Log the exception
////            if (GetStatusCode(context) == 500)
////            {
////                _logger.LogError(context.Exception, "An unhandled exception occurred: {ErrorMessage}", context.Exception.Message);
////            }
////            else
////            {
////                _logger.LogWarning(context.Exception, "An exception occurred: {ErrorMessage}", context.Exception.Message);
////            }

////        }

////        protected virtual int GetStatusCode(ExceptionContext context)
////        {
////            if (context.Exception is AppAuthorizationException)
////            {
////                return context.HttpContext.User.Identity.IsAuthenticated
////                    ? (int)HttpStatusCode.Forbidden
////                    : (int)HttpStatusCode.Unauthorized;
////            }

////            if (context.Exception is AppValidationException || context.Exception is FluentValidation.ValidationException)
////            {
////                return (int)HttpStatusCode.BadRequest;
////            }

////            if (context.Exception is AppEntityNotFoundException)
////            {
////                return (int)HttpStatusCode.NotFound;
////            }

////            return (int)HttpStatusCode.InternalServerError;
////        }
////    }
////}

//using ARMS.Application.Entities.ExceptionLog.Services.Interface;
//using ARMS.Application.Entities.ExceptionLog.Services;
//using ARMS.Application.Entities.ExceptionLog.ViewModel;
//using ARMS.Application.Exceptions;
//using ARMS.Application.Utilities;
//using ARMS.Core.Models;
//using ARMS.Core.Entities;
//using ARMS.Infrastructure.Contexts;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Net;
//using System.Threading.Tasks;
//using ARMS.Application.Common;

//namespace ARMS.WebAPI.Filters
//{
//    public class AppExceptionFilter : IAsyncExceptionFilter
//    {
//        private readonly ILogger<AppExceptionFilter> _logger;
//        private readonly ErrorDBContext _dbContext;
//        private readonly IExceptionLog _service;
//        private readonly IdentityInfo _currentUser;

//        public AppExceptionFilter(ILogger<AppExceptionFilter> logger, ErrorDBContext dbContext, ExceptionLogService service, IdentityInfo currentUser)
//        {
//            _logger = logger;
//            _dbContext = dbContext;
//            _service = service;
//            _currentUser = currentUser;
//        }

//        public async Task OnExceptionAsync(ExceptionContext context)
//        {
//            if (!context.ActionDescriptor.IsControllerAction())
//            {
//                return;
//            }


//            _logger.LogError(context.Exception, "An unhandled exception occurred: {ErrorMessage}", context.Exception.Message);


//            await LogErrorToDatabase(context.Exception);


//            await HandleAndWrapException(context);
//        }

//        private async Task LogErrorToDatabase(Exception exception)
//        {

//            string exceptionMessage = TruncateExceptionMessage(exception.ToString(), 256);
//            string exceptionMessage1 = TruncateExceptionMessage(exception.Message, 256);

//            var errorLog = new ExceptionLogModel
//            {
//                Timestamp = DateTime.UtcNow,
//                LogLevel = "Error",
//                Message = exceptionMessage1,
//                Exception = exceptionMessage
//            };

//            try
//            {
//                errorLog.Audit();
//                _dbContext.ExceptionLog.Add(errorLog);
//                await _dbContext.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {

//                _logger.LogError(ex, "Error occurred while saving error log to the database.");

//                throw;
//            }
//        }

//        private async Task HandleAndWrapException(ExceptionContext context)
//        {
//            context.HttpContext.Response.StatusCode = GetStatusCode(context);

//            context.Result = new ObjectResult(new ErrorInfo
//            {
//                Status = GetStatusCode(context),
//                Title = context.Exception.Message,
//                Errors = AppException.GetTrueExceptionMessage(context.Exception)
//            });
//        }

//        private string TruncateExceptionMessage(string message, int maxLength)
//        {
//            if (message.Length <= maxLength)
//            {
//                return message;
//            }
//            else
//            {
//                return message.Substring(49, maxLength);
//            }
//        }

//        protected virtual int GetStatusCode(ExceptionContext context)
//        {
//            if (context.Exception is AppAuthorizationException)
//            {
//                return context.HttpContext.User.Identity.IsAuthenticated
//                    ? (int)HttpStatusCode.Forbidden
//                    : (int)HttpStatusCode.Unauthorized;
//            }

//            if (context.Exception is AppValidationException || context.Exception is FluentValidation.ValidationException)
//            {
//                return (int)HttpStatusCode.BadRequest;
//            }

//            if (context.Exception is AppEntityNotFoundException)
//            {
//                return (int)HttpStatusCode.NotFound;
//            }

//            return (int)HttpStatusCode.InternalServerError;
//        }
//    }
//}

//using ARMS.Application.Exceptions;
//using ARMS.Application.Utilities;
//using ARMS.Core.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using System.Net;

//namespace ARMS.WebAPI.Filters
//{
//    public class AppExceptionFilter : IAsyncExceptionFilter
//    {
//        private readonly ILogger<AppExceptionFilter> _logger;

//        public AppExceptionFilter(ILogger<AppExceptionFilter> logger)
//        {
//            _logger = logger;
//        }
//        public async Task OnExceptionAsync(ExceptionContext context)
//        {
//            if (!context.ActionDescriptor.IsControllerAction())
//            {
//                return;
//            }
//            await HandleAndWrapException(context);
//        }

//        private async Task HandleAndWrapException(ExceptionContext context)
//        {
//            //if (!ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
//            //{
//            //    return;
//            //}

//            context.HttpContext.Response.StatusCode = GetStatusCode(context);

//            context.Result = new ObjectResult(new ErrorInfo
//            {
//                Status = GetStatusCode(context),
//                Title = context.Exception.Message,
//                Errors = AppException.GetTrueExceptionMessage(context.Exception)
//            });

//            if (GetStatusCode(context) == 500)
//            {
//                _logger.LogError(context.Exception, context.Exception.Message);
//            }
//        }
//        /// <summary>
//        /// To get status code from exception
//        /// </summary>
//        /// <param name="context"></param>
//        /// <returns></returns>
//        protected virtual int GetStatusCode(ExceptionContext context)
//        {
//            if (context.Exception is AppAuthorizationException)
//            {
//                return context.HttpContext.User.Identity.IsAuthenticated
//                    ? (int)HttpStatusCode.Forbidden
//                    : (int)HttpStatusCode.Unauthorized;
//            }

//            if (context.Exception is AppValidationException)
//            {
//                return (int)HttpStatusCode.BadRequest;
//            }

//            if (context.Exception is FluentValidation.ValidationException)
//            {
//                return (int)HttpStatusCode.BadRequest;
//            }

//            if (context.Exception is AppEntityNotFoundException)
//            {
//                return (int)HttpStatusCode.NotFound;
//            }

//            return (int)HttpStatusCode.InternalServerError;
//        }
//    }

//}



//using ARMS.Application.Exceptions;
//using ARMS.Application.Utilities;
//using ARMS.Core.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.Extensions.Logging;
//using System.Net;

//namespace ARMS.WebAPI.Filters
//{
//    public class AppExceptionFilter : IAsyncExceptionFilter
//    {
//        private readonly ILogger<AppExceptionFilter> _logger;

//        public AppExceptionFilter(ILogger<AppExceptionFilter> logger)
//        {
//            _logger = logger;
//        }

//        public async Task OnExceptionAsync(ExceptionContext context)
//        {
//            if (!context.ActionDescriptor.IsControllerAction())
//            {
//                return;
//            }

//            await HandleAndWrapException(context);
//        }

//        private async Task HandleAndWrapException(ExceptionContext context)
//        {
//            context.HttpContext.Response.StatusCode = GetStatusCode(context);

//            context.Result = new ObjectResult(new ErrorInfo
//            {
//                Status = GetStatusCode(context),
//                Title = context.Exception.Message,
//                Errors = AppException.GetTrueExceptionMessage(context.Exception)
//            });

//            // Log the exception
//            if (GetStatusCode(context) == 500)
//            {
//                _logger.LogError(context.Exception, "An unhandled exception occurred: {ErrorMessage}", context.Exception.Message);
//            }
//            else
//            {
//                _logger.LogWarning(context.Exception, "An exception occurred: {ErrorMessage}", context.Exception.Message);
//            }

//        }

//        protected virtual int GetStatusCode(ExceptionContext context)
//        {
//            if (context.Exception is AppAuthorizationException)
//            {
//                return context.HttpContext.User.Identity.IsAuthenticated
//                    ? (int)HttpStatusCode.Forbidden
//                    : (int)HttpStatusCode.Unauthorized;
//            }

//            if (context.Exception is AppValidationException || context.Exception is FluentValidation.ValidationException)
//            {
//                return (int)HttpStatusCode.BadRequest;
//            }

//            if (context.Exception is AppEntityNotFoundException)
//            {
//                return (int)HttpStatusCode.NotFound;
//            }

//            return (int)HttpStatusCode.InternalServerError;
//        }
//    }
//}

using ARMS.Application.Entities.ExceptionLog.Services.Interface;
using ARMS.Application.Entities.ExceptionLog.Services;
using ARMS.Application.Entities.ExceptionLog.ViewModel;
using ARMS.Application.Exceptions;
using ARMS.Application.Utilities;
using ARMS.Core.Models;
using ARMS.Core.Entities;
using ARMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using ARMS.Application.Common;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace ARMS.WebAPI.Filters
{
    public class AppExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<AppExceptionFilter> _logger;
        private readonly ErrorDBContext _dbContext;
        private readonly IExceptionLog _service;
        private readonly IdentityInfo _currentUser;


        public AppExceptionFilter(ILogger<AppExceptionFilter> logger, ErrorDBContext dbContext, ExceptionLogService service, IdentityInfo currentUser)
        {
            _logger = logger;
            _dbContext = dbContext;
            _service = service;
            _currentUser = currentUser;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {

            if (!(context.ActionDescriptor is ControllerActionDescriptor descriptor))
            {
                return;
            }

            var controllerName = descriptor.ControllerName;
            var actionName = descriptor.ActionName;
            string exceptionMessage1 = context.Exception.InnerException?.Message ?? ExtractRelevantExceptionMessage(context.Exception.ToString());

            var exceptionMessage = $"Exception in {controllerName}-{actionName}:{context.Exception.Message}";


            _logger.LogError(context.Exception, "An unhandled exception occurred: {ErrorMessage}", context.Exception.Message);


            await LogErrorToDatabase(exceptionMessage1, exceptionMessage);


            await HandleAndWrapException(context);
        }

        private async Task LogErrorToDatabase(string exceptionMessage1, string exceptionMessage)
        {


            var errorLog = new ExceptionLogModel
            {
                Timestamp = DateTime.UtcNow.ToLocalTime(),
                LogLevel = "Error",
                Message = exceptionMessage,
                Exception = exceptionMessage1
            };

            try
            {
                errorLog.Audit();
                _dbContext.ExceptionLog.Add(errorLog);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error occurred while saving error log to the database.");

                throw;
            }
        }

        private async Task HandleAndWrapException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = GetStatusCode(context);

            context.Result = new ObjectResult(new ErrorInfo
            {
                Status = GetStatusCode(context),
                Title = context.Exception.Message,
                Errors = AppException.GetTrueExceptionMessage(context.Exception)
            });
        }

        private string ExtractRelevantExceptionMessage(string exceptionMessage1)
        {
            // Split the exception message by line breaks
            var lines = exceptionMessage1.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            // The first line usually contains the relevant exception message
            if (lines.Length > 0)
            {
                // Extract the exception type and message from the first line
                var firstLineParts = lines[0].Split(':');
                if (firstLineParts.Length > 1)
                {
                    // Join the exception type and message
                    return $"{firstLineParts[0].Trim()}: {firstLineParts[1].Trim()}";
                }
            }

            // If parsing fails, return the original exception message
            return exceptionMessage1;
        }


        protected virtual int GetStatusCode(ExceptionContext context)
        {
            if (context.Exception is AppAuthorizationException)
            {
                return context.HttpContext.User.Identity.IsAuthenticated
                    ? (int)HttpStatusCode.Forbidden
                    : (int)HttpStatusCode.Unauthorized;
            }

            if (context.Exception is AppValidationException || context.Exception is FluentValidation.ValidationException)
            {
                return (int)HttpStatusCode.BadRequest;
            }

            if (context.Exception is AppEntityNotFoundException)
            {
                return (int)HttpStatusCode.NotFound;
            }

            return (int)HttpStatusCode.InternalServerError;
        }
    }
}

