using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.ViewModels.ProductVms;

namespace WebApplication1.Repository.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<ProductVm>> GetAllProducts();
        Task<ProductVm> GetProductById(int id);
        Task<IEnumerable<ProductVm>> SearchProducts(string searchTerm);
    }
}
