using API_E_Commerce.Model;
using API_E_Commerce.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Xml.Linq;

namespace API_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdectsController : ControllerBase
    {
        IProductRepository prod;
        ICRUD_Repository<Products> RepoProdect;
        ICRUD_Repository<Categories> RepoCategorie;
        public ProdectsController( ICRUD_Repository<Products> T,
            ICRUD_Repository<Categories> TT,IProductRepository product) 
        {
            this.RepoProdect = T;
            this.RepoCategorie = TT;
            this.prod = product;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(RepoProdect.GetAll());
        }
        [HttpGet("GetAllProductsByIdCategorie")]
        public IActionResult GetAllProductsByIdCategorie(int id)
        {
            return Ok(prod.getAllByIdCategorie(id));
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            Products P = RepoProdect.GetById(id);
            if (P != null)
                return Ok(RepoProdect.GetById(id));
            else
                return BadRequest("This Id does not exist");
        }
        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            var person = RepoProdect.GetByName(r => r.Name == name);
            if (person != null)
                return Ok(person);
            else
                return BadRequest("This name does not exist");
        }
        [HttpPost("Insert")]
        public IActionResult Insert(Products products)
        {
            if (ModelState.IsValid)
            {
                if(products.Id_Categories == 0 || products.Price == 0)
                    return BadRequest("Your Data is incorrect");
                else
                {
                    RepoProdect.Insert(products);
                    return Ok("Data Saved");
                }
            }
            else
                return BadRequest();
        }
        [HttpPut]
        public IActionResult Update(Products products, int id)
        {
            if (ModelState.IsValid)
            {
                if (products.Id_Categories == 0 || products.Price == 0 || id == 0)
                    return BadRequest("Verify that the data is correct ");
                else
                {
                    Products ProduCt = RepoProdect.GetById(id);
                    if (ProduCt == null)
                        return BadRequest("the id categorie is correct");
                    else
                    {
                        var person = RepoProdect.GetByName(r => r.Name == products.Name);
                        Categories categories = RepoCategorie.GetById(products.Id_Categories);
                        if (person == null && categories != null)
                        {
                            products.Id = id;
                            RepoProdect.Update(products, id);
                            return Ok("Data Saved");
                        }
                        return BadRequest("The product name already exists or id categorie is correct ");
                    }
                }
            }
            return BadRequest();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Products p = RepoProdect.GetById(id);
            if (p != null)
            {
                RepoProdect.DeleteById(id);
                return Ok("Save Data");
            }
            return BadRequest("This Id does not exist");
        }
    }
}
