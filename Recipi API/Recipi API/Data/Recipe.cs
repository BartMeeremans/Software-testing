using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipi_API.Data
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public int Time { get; set; }
        public Difficulty? Difficulty { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
    public enum Difficulty
    {
        Easy, Intermediate, Advanced
    }
}
