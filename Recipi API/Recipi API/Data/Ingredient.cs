using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipi_API.Data
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public int Quantity{ get; set; }
        public string Unit { get; set; }
        public int RecipeId { get; set; }
    }
}
