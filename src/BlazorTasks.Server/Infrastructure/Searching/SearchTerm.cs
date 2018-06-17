﻿using System;

namespace BlazorTasks.Server.Infrastructure
{
    public class SearchTerm
    {

        public string Name { get; set; }

        public string Operator { get; set; }

        public string Value { get; set; }

        public bool ValidSyntax { get; set; }

        public ISearchExpressionProvider ExpressionProvider { get; set; }

    }
}
