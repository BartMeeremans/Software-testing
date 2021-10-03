using Recipi_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipi_API.DTO
{
    public class recipeDetailsDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public int time { get; set; }
        public string category { get; set; }
        public int difficulty { get; set; }
        public List<Ingredient> ingredients { get; set; }
    }
}
