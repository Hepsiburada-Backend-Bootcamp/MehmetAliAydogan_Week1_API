using HepsiBurada_Week1.Model;
using HepsiBurada_Week1.QueryFilters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HepsiBurada_Week1.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<ProductDto> _products = new List<ProductDto>()
            {
                new ProductDto(){Id=1, Name="Television", Brand="Philips", Description="40 inch TV", Price=4000, DiscountPerc=10},
                new ProductDto(){Id=2, Name="Computer", Brand="Philips", Description="8 GB RAM Laptop", Price=10000, DiscountPerc=15},
                new ProductDto(){Id=3, Name="Monitor", Brand="Asus", Description="25 inch monitor", Price=2000,DiscountPerc=5},
                new ProductDto(){Id=4, Name="Mouse", Brand="Razer", Description="Gaming Mouse", Price=500, DiscountPerc=15},
                new ProductDto(){Id=5, Name="Keyboard", Brand="Logitech", Description="Mechanical Keyboard", Price=1500, DiscountPerc=50},
                new ProductDto(){Id=6, Name="HeadPhone", Brand="HyperX", Description="Gaming HeadSet", Price=2000, DiscountPerc=60},
                new ProductDto(){Id=7, Name="MousePad", Brand="SteelSeries", Description="40*40 Mousepad", Price=200, DiscountPerc=5},
            };

        // GET: api/v1/products
        [HttpGet]
        public IActionResult GetProducts([FromQuery] ProductFilter filter)
        {
            try
            {
              List<ProductDto> _result = _products.FindAll(prod =>
              prod.Name.ToLower() == (filter.Name is not null ? filter.Name.ToLower() : prod.Name.ToLower()) &&
              prod.Brand.ToLower() == (filter.Brand is not null ? filter.Brand.ToLower() : prod.Brand.ToLower()) &&

              prod.Price <= (filter.PriceLT is not null ? int.Parse(filter.PriceLT) : prod.Price) &&
              prod.Price >= (filter.PriceMT is not null ? int.Parse(filter.PriceMT) : prod.Price) &&

              prod.DiscountPerc >= (filter.DiscountPercLT is not null ? int.Parse(filter.DiscountPercLT) : prod.DiscountPerc)
            );

                switch (filter.SortBy?.ToLower())
                {
                    case "name":
                        _result.Sort((x, y) => x.Name.CompareTo(y.Name));
                        break;

                    case "brand":
                        _result.Sort((x, y) => x.Brand.CompareTo(y.Brand));
                        break;

                    case "price":
                        _result.Sort((x, y) => x.Price.CompareTo(y.Price));
                        break;

                    case "discountperc":
                        _result.Sort((x, y) => x.DiscountPerc.CompareTo(y.DiscountPerc));
                        break;

                    default:
                        _result.Sort((x, y) => x.Id.CompareTo(y.Id));
                        break;
                }

                return Ok(_result);
            }
            catch
            {
                return BadRequest("There is a problem with the request");
            }
            
        }

        // GET api/v1/products/5
        [HttpGet("{id:int}")]
        public IActionResult GetProduct(int id)
        {
            var product = _products.FirstOrDefault(x => x.Id == id);

            if(product != null)
                return Ok(product);

            return BadRequest($"Product with id: {id} does not exist!");
        }

        // POST api/v1/products
        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDto product)
        {
            var existing_product = _products.FirstOrDefault(x => x.Id == product.Id);

            if (existing_product == null)
            {
                _products.Add(product);
                return Ok($"Product with id: {product.Id} created");
            }

            return BadRequest($"Can not create product. Product with id: {product.Id} exists!");
        }

        // PUT api/v1/products/5
        [HttpPut("{id:int}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDto product)
        {
            var existing_product = _products.FirstOrDefault(x => x.Id == id);

            if(existing_product != null)
            {
                int index = _products.FindIndex(ind => ind.Id.Equals(id));

                if (index > -1)
                    _products[index] = product;

                return Ok($"Product with id: {id} updated");
            }

            return BadRequest($"Can not update product. Product with id: {id} does not exist!");
        }

        // DELETE api/v1/products/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            ProductDto product = _products.FirstOrDefault(x => x.Id == id);

            bool IsRemoved = _products.Remove(product);

            if (!IsRemoved)
            {
                return NotFound($"Product with id: {id} does not exist!");
            }

            return Ok($"Product with id: {id} removed successfully");
        }
    }
}
