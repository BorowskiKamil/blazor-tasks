using System;
using Newtonsoft.Json;

namespace BlazorTasks.Server.Models
{
    public class Collection<T> : Resource
    {
        public T[] Value { get; set; }
    }
}
