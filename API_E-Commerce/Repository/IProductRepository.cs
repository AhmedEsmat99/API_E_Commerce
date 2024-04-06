using API_E_Commerce.Model;
using System.Collections.Generic;

namespace API_E_Commerce.Repository
{
    public interface IProductRepository
    {
        List<Products> getAllByIdCategorie(int id);
    }
}