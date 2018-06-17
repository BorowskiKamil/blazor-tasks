using System;
using System.Collections.Generic;

namespace BlazorTasks.Server.Models
{
    public class PagedResults<T>
    {

        public IEnumerable<T> Items { get; set; }

        public int TotalSize { get; set; }
    }
}
