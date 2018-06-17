using System;
using Newtonsoft.Json;

namespace BlazorTasks.Client.Models
{
    public class Collection<T> : Link
    {
        public T[] Value { get; set; }
    }
}
