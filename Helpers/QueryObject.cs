using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TarifPaylasim.Helpers
{
    public class QueryObject
    {
        public string? RecipeName { get; set; } = null;
        public string? Category { get; set; } = null;

        public string? SortBy { get; set; } = null;

        public bool IsDecsending { get; set; } = false;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 15;
    }
}