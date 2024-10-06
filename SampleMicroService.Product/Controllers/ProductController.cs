using SampleMicroService.Product.Entities;
using SampleMicroService.Product.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SampleMicroService.Product.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Variable
        private readonly IProductRepository _productsRepository;
        private readonly ILogger<ProductController> _logger;

        #endregion

        #region Constructor
        public ProductController(ILogger<ProductController> logger, IProductRepository productsRepository)
        {
            _logger = logger;
            _productsRepository = productsRepository;
        }
        #endregion

        #region Crud_Actions

        [HttpGet]
        [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Entities.Product>>> GetVal()
        {
            var prod =await _productsRepository.GetProducts();
            return Ok(prod);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Entities.Product>> GetProductById(string id)
        {
            var prod= await _productsRepository.GetProductById(id);
            if (prod != null) return Ok(prod);
            _logger.LogError($"Product with id : {id}, hasn't been found in database");
            return NotFound();

        }

        [HttpPost]
        [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Entities.Product>> CreateProduct([FromBody] Entities.Product product)
        {
            await _productsRepository.Create(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id },product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateProduct([FromBody]Entities.Product product)
        {
            return Ok(await _productsRepository.Update(product));

        }

        [HttpDelete("id:length(24)")]
        [ProducesResponseType(typeof(Entities.Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteProductById(string id)
        {

            return Ok(await _productsRepository.Delete(id));
        }

        #endregion
    }
}
