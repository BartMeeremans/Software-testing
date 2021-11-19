using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipi_API.Data;
using Recipi_API.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipi_API.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly categoryService _cService;
        public CategoryController(categoryService cService)
        {
            _cService = cService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Category>> Categories()
        {
            return _cService.GetCategories().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            Category item = _cService.GetCategory(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
        [HttpPost("")]
        public ActionResult<Category> CreateCategory(Category item)
        {
            Category created = _cService.CreateCategory(item);
            return CreatedAtAction(nameof(GetCategory), new { id = created.CategoryId }, created);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            Category item;
            try
            {
                item = _cService.GetCategory(id);
            }
            catch
            {
                return NotFound();
            }
            if (item != null)
            {
                _cService.DeleteCategory(item.CategoryId);
                return Ok();
            }

            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateCategory(int id, Category updated)
        {
            if (id != updated.CategoryId)
            {
                return BadRequest();
            }
            Category category = _cService.UpdateCategory(id, updated);
            if (category == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetCategory), new { id = updated.CategoryId }, updated);
        }
    }
}
