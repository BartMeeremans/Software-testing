using Recipi_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipi_API.DTO
{
    public class searchModel
    {
        public string Title { get; set; }
        public int? maxDificulty { get; set; }
        public int? maxTime { get; set; }
        public List<int> categories { get; set; }
    }
}
