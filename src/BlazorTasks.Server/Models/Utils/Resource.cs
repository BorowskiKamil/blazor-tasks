using System;
using Newtonsoft.Json;

namespace BlazorTasks.Server.Models
{
    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }

    }
}
