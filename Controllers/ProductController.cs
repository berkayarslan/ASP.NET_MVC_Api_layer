using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockControl_Entity;
using StockControl_Service.Services.Abstract;

namespace StockControl_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericService<Product> _service;

        public ProductController(IGenericService<Product> service)
        {
            _service = service;
        }

        /// <summary>
        /// tum urunler ilişkileriyle gelsin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_service.GetAll(x => x.Category, b => b.Supplier));
        }

        /// <summary>
        /// tüm aktif olanları getir--ilişkileri ile beraber
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetActiveProduct(int id)
        {
            return Ok(_service.GetActive(x => x.Category, y => y.Supplier));
        }
        /// <summary>
        /// ürünü id si ile getir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id) 
        {
            return Ok(_service.GetById(id));
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _service.Add(product);
            return CreatedAtAction("GetProductById", new { id = product.ID }, product);
        }
    }
}
