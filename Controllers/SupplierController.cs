using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockControl_Entity;
using StockControl_Entity.Entities.Concrete;
using StockControl_Service.Services.Abstract;

namespace StockControl_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IGenericService<Supplier> _service;

        public SupplierController(IGenericService<Supplier> service)
        {
            _service = service;
        }


        //tum tedarikciler 

        [HttpGet]
        public IActionResult GetAllSuppliers()
        {
            var suppliers = _service.GetAll();

            if (suppliers!=null)
            {
                return Ok(suppliers);
            }
            return BadRequest();
        }

        [HttpGet]//aktif olan tüm tedarikcilerr gelsin 
        public IActionResult GetAllActiveSuppliers()
        {
            return Ok(_service.GetActive());
        }
        [HttpPost]
        public IActionResult AddSupplier(Supplier supplier)
        {
            _service.Add(supplier);
            //return Ok();
            return CreatedAtAction("GetCategoryById", new { id = supplier.ID }, supplier);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSupplier(int id, Supplier supplier)
        {
            if (id != supplier.ID) return BadRequest();

            try
            {
                if (_service.Update(supplier)) return Ok();
                else return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
       
        
        [HttpDelete("{id}")]
        public IActionResult DeleteSupplier(int id)
        {
            var supplier = _service.GetById(id);
            if (supplier is null) return BadRequest();

            _service.Remove(supplier);
            return Ok("Kategori silindi.");
        }

        [HttpGet("{id}")]
        public IActionResult SetSupplierActive(int id)
        {
            var supplier = _service.GetById(id);
            if (supplier is null) return NotFound();

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
