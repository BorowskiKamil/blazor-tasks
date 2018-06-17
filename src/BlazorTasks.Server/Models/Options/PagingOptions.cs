using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorTasks.Server.Models
{
    public class PagingOptions
    {

        [Range(1, 9999, ErrorMessage = "Offset must be greater than 0.")]
        public int? Offset { get; set; }

        [Range(1, 9999, ErrorMessage = "Limit must be greater than 0 and less than 100.")]
        public int? Limit { get; set; }

    }
}