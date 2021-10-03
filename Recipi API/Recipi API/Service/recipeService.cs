using Microsoft.EntityFrameworkCore;
using Recipi_API.Data;
using Recipi_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipi_API.Service
{
    public class recipeService
    {
        private readonly ApplicationDbContext _context;
        public recipeService(ApplicationDbContext context)
        {
            _context = context;

        }
        public IEnumerable<recipeDto> GetRecipes()
        {
            return _context.Recipes.Select(entity =>
                new recipeDto()
                {
                    id = entity.RecipeId,
                    title = entity.Title,
                    time = entity.Time, 
                    category = entity.Category.Name,
                    difficulty = (int)entity.Difficulty,
                }).ToList();

        }
        public IEnumerable<Recipe> GetRecipesFull()
        {
            return _context.Recipes.Include(x => x.Category).ToList();

        }
        public recipeDetailsDto GetRecipe(int id)
        {
            Recipe item = _context.Recipes.Include(x => x.Category).Where(x => x.RecipeId == id).FirstOrDefault();
            recipeDetailsDto recipe = new recipeDetailsDto();
            if (item == null)
            {
                return null;
            }
            List<Ingredient> ingredients = _context.Ingredients.Where(x => x.RecipeId == item.RecipeId).ToList();
            recipe.id = item.RecipeId;
            recipe.title = item.Title;
            recipe.difficulty = (int)item.Difficulty;
            recipe.time = item.Time;
            recipe.category = item.Category.Name;
            recipe.ingredients = ingredients;
            return recipe;
        }
        public Recipe CreateRecipe(Recipe item)
        {
            _context.Recipes.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Recipe UpdateRecipe(int id, Recipe item)
        {
            Recipe toUpdate = _context.Recipes.Find(id);
            if (toUpdate == null)
            {
                return null;
            }
            toUpdate.Time = item.Time;
            toUpdate.Title = item.Title;
            toUpdate.CategoryId = item.CategoryId;
            toUpdate.Difficulty = item.Difficulty;
            _context.SaveChanges();
            return toUpdate;
        }

        public IEnumerable<recipeDto> GetRecipesSearch(string name)
        {
            return _context.Recipes.Where(x => x.Title == name).Select(entity =>
                new recipeDto()
                {
                    id = entity.RecipeId,
                    title = entity.Title,
                    time = entity.Time,
                    category = entity.Category.Name,
                    difficulty = (int)entity.Difficulty,
                }).ToList();
        }

        public void DeleteRecipe(int id)
        {
            Recipe toDelete = _context.Recipes.Find(id);
            _context.Recipes.Remove(toDelete);
            _context.SaveChanges();
        }

       
    }
}
