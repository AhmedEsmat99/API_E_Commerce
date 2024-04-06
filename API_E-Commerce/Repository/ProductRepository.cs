using API_E_Commerce.Model;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace API_E_Commerce.Repository
{
    public class ProductRepository : IProductRepository
    {
        Context db;
        ICRUD_Repository<Categories> Catg;
        public ProductRepository(ICRUD_Repository<Categories> catg, Context db)
        {
            Catg = catg;
            this.db = db;
        }

        public List<Products> getAllByIdCategorie(int id)
        {
            var c = Catg.GetById(id);
            List<Products> products = db.products.Where(p => p.Id_Categories == c.Id).ToList();
            return products;
        }
    }
}
