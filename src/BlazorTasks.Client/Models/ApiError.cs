using System;
using System.ComponentModel;
using System.Linq;

namespace BlazorTasks.Client.Models
{
    public class ApiError
    {
        public string Message { get; set; }
        public string Details { get; set; }
        public string StackTrace { get; set; }
    }
}
