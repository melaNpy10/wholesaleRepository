using Microsoft.AspNetCore.Mvc;
using WholesalesRetailer.Services.Interfaces;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            try
            {
                var response = _productService.GetProducts();
                if (response != null) return Ok(response);
                else return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetProducts)} - Error while getting Products. Message={ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Product>>> GetProductsById(int id)
        {
            try
            {
                var response = await _productService.GetProductById(id);
                if (response != null) return Ok(response);
                else return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetProductsById)} - Error while getting Products. Message={ex.Message}");
                return StatusCode(500);
            }
        }
        [HttpGet("{code}")]
        public ActionResult<int> GetProductsIdByCode(string code)
        {
            try
            {
                var response = _productService.GetProductIdByCode(code);
                if (response != null) return Ok(response);
                else return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetProductsIdByCode)} - Error while getting Products. Message={ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet("{code}/{quantity}")]
        public void GetProductsById(string code, int quantity)
        {
            try
            {
                _productService.UpdateProduct(code, quantity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetProductsById)} - Error while getting Products. Message={ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<Product?> AddProduct(Product request)
        {
            try
            {
                var response = _productService.AddProduct(request);
                if (response != null) return Ok(response);
                else return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(AddProduct)} - Error while adding Products. Message={ex.Message}");
                return StatusCode(500);
            }

        }
    }
}
