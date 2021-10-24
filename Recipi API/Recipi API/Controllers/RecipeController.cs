using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipi_API.Data;
using Recipi_API.DTO;
using Recipi_API.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipi_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly recipeService _rService;
        public RecipeController(recipeService rService)
        {
            _rService = rService;
        }
        [HttpGet("")]
        public ActionResult<IEnumerable<recipeDto>> Recipies()
        {
            return _rService.GetRecipes().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<recipeDetailsDto> GetRecipe(int id)
        {
            recipeDetailsDto item = _rService.GetRecipe(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            recipeDetailsDto item;
            try
            {
                item = _rService.GetRecipe(id);
            }
            catch
            {
                return NotFound();
            }      
            if(item != null)
            {
                _rService.DeleteRecipe(item.id);

                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost("")]
        public ActionResult<Recipe> CreateRecipe(Recipe item)
        {
            Recipe created = _rService.CreateRecipe(item);
            return CreatedAtAction(nameof(GetRecipe), new { id = created.RecipeId }, created);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateRecipe(int id, Recipe updated)
        {
            if (id != updated.RecipeId)
            {
                return BadRequest();
            }
            Recipe recipe = _rService.UpdateRecipe(id, updated);
            if (recipe == null)
            {
                return NotFound();
            }
            //return NoContent();

            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.RecipeId }, recipe);
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<recipeDto>> Search([FromQuery] searchModel options)
        {
            List<Recipe> recipes = _rService.GetRecipesFull().ToList();
            List<recipeDto> filteredRecipes = new List<recipeDto>();
            if(options.categories == null)
            {
                options.categories = new List<int>();
            }
            string title = null;

            if (options.Title != null)
            {
                title = options.Title.ToLower();
                foreach (var item in recipes.Where(x => x.Title.ToLower().Contains(title) || x.Time <= options.maxTime || (int)x.Difficulty <= options.maxDificulty || options.categories.Contains(x.CategoryId)))
                {
                    filteredRecipes.Add(new recipeDto { title = item.Title, category = item.Category.Name, difficulty = (int)item.Difficulty, id = item.RecipeId, time = item.Time });
                }

            }
            else
            {
                foreach (var item in recipes.Where(x => x.Time <= options.maxTime || (int)x.Difficulty <= options.maxDificulty || options.categories.Contains(x.CategoryId)))
                {
                    filteredRecipes.Add(new recipeDto {title = item.Title, category = item.Category.Name, difficulty = (int)item.Difficulty, id = item.RecipeId, time = item.Time});
                }
            }
            if(options.Title == null && options.maxDificulty == null && options.maxTime == null && options.categories.Count == 0)
            {
                return _rService.GetRecipes().ToList();
            }else
                return filteredRecipes;
        }

    }
}
