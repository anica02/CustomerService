using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Application.Logging
{
    public interface IErrorLogger
    {
        void Log(AppError error);
    }

    public class AppError
    {
        public string Username { get; set; }
        public Guid ErrorId { get; set; }
        public Exception Error { get; set; }
    }
}
