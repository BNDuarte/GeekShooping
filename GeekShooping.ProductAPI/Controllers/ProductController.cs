﻿using GeekShooping.ProductAPI.Data.ValueObjects;
using GeekShooping.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShooping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            var products = await _repository.FindAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(long id)
        {
            ProductVO product = await _repository.FindById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductVO>> Create([FromBody] ProductVO vo)
        {
            if (vo == null)
            {
                return BadRequest();
            }

            ProductVO product = await _repository.Create(vo);
            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO vo)
        {
            if (vo == null)
            {
                return BadRequest();
            }

            ProductVO product = await _repository.Update(vo);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            bool status = await _repository.Delete(id);
            if (!status)
            {
                return BadRequest();
            }
            return Ok(status);
        }
    }
}