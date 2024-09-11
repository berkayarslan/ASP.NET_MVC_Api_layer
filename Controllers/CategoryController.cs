using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockControl_Entity.Entities.Concrete;
using StockControl_Service.Services.Abstract;

namespace StockControl_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericService<Category> _service;

        public CategoryController(IGenericService<Category> service)
        {
            _service = service;
        }

        /*
         hata almadan bu şekilde json ekleyebilirsin 
        {
              "id": 0,
              "addedDate": "2024-09-11T06:52:35.208Z",
              "modifiedDate": "2024-09-11T06:52:35.208Z",
              "isActive": true,
              "cateGoryName": "string",
              "description": "string" 
        }
         
         */
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _service.Add(category);
            //return Ok();
            return CreatedAtAction("GetCategoryById", new { id = category.ID }, category);
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(_service.GetAll());//tüm kategoriler gelsin 
        }

        [HttpGet]//aktif olan tüm kategoriler gelsin 
        public IActionResult GetAllActiveCategories()
        {
            return Ok(_service.GetActive());
        }

        [HttpGet]
        public IActionResult GetCategoryById(int id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, Category category)
        {
            if (id != category.ID) return BadRequest();

            try
            {
                if (_service.Update(category)) return Ok();
                else return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _service.GetById(id);
            if (category is null) return BadRequest();

            _service.Remove(category);
            return Ok("Kategori silindi.");
        }

        //Id bilgisini aldığımız categori aktf yap

        [HttpGet("{id}")]
        public IActionResult SetCategoryActive(int id)
        {
            var category = _service.GetById(id);
            if(category is null) return NotFound();

            try
            {
                _service.GetActive(id);
                return Ok("Aktifleştirildi");
            }
            catch
            {
                return BadRequest();
            }
            
        }

    }
}
