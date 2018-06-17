using System;

namespace BlazorTasks.Server.Models
{
    public class RootResponse : Resource
    {
        public Link Categories { get; set; }

        public Link Tasks { get; set; }

        public Link Comments { get; set; }

    }
}
