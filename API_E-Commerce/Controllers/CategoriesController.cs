using API_E_Commerce.Model;
using API_E_Commerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace API_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase 
    {
        ICRUD_Repository<Categories> Categorie;
        public CategoriesController(ICRUD_Repository<Categories> _Repository) 
        { 
            this.Categorie = _Repository;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(Categorie.GetAll());
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            Categories categories = Categorie.GetById(id);
            if (categories != null)
                return Ok(Categorie.GetById(id));
            else 
                return BadRequest("This Id does not exist");
        }
        [HttpGet("GetName")]
        
        public IActionResult GetByName(string name)
        {
            var person = Categorie.GetByName(r => r.Name == name);
            if (person != null)
                return Ok(person);
            else
                return BadRequest("This name does not exist"); 
        }
        [HttpPost("Insert")]
        [Authorize(Roles = "Admin")]
        public IActionResult Insert(Categories categories)
        {
            if (ModelState.IsValid)
            {
                var person = Categorie.GetByName(r => r.Name == categories.Name);
                if(person == null)
                {
                    Categorie.Insert(categories);
                    return Ok("Data Saved");
                }
                else
                    return BadRequest("The Name Categorie Already exist");
            }
            else
                return BadRequest(ModelState);
        }
        [HttpPut]
        public IActionResult Update(Categories categories , int id)
        {
            if (ModelState.IsValid)
            {
                if(id  != 0)
                {
                    var person = Categorie.GetByName(r => r.Name == categories.Name);
                    Categories categories1 = Categorie.GetById(id);
                    if (person == null || person.Name == categories.Name && categories1 != null)
                    {
                        categories.Id = id;
                        Categorie.Update(categories, id);
                        return Ok("Data Saved");
                    }
                    else
                        return BadRequest("The Categorie name already exists or id categorie is correct");
                }else
                    return BadRequest("This Id does not exist");
            }
            return BadRequest();
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            Categories C = Categorie.GetById(id);
            if(C != null)
            {
                Categorie.DeleteById(id);
                return Ok("Save Data");
            }
            return BadRequest("This Id does not exist");
        }
    }
}
