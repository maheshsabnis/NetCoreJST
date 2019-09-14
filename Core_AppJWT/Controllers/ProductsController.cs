using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core_AppJWT.Models;
using Core_AppJWT.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Core_AppJWT.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IRepository<Product, int> repository;
        public ProductsController(IRepository<Product, int> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var prds = repository.Get();
            return Ok(prds);
        }

        [HttpPost]
     
        public IActionResult Post(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.Create(product);
                return Ok(product);
            }
            return BadRequest(ModelState);
        }
    }
}