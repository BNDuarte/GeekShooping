using GeekShooping.Web.Models;
using GeekShooping.Web.Services.IServices;
using GeekShooping.Web.Utils;

namespace GeekShooping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        private const string basePath = "api/v1/product";

        public ProductService(HttpClient client)
        {
            _client = client??throw new ArgumentNullException(nameof(client));
        }


        public async Task<IEnumerable<ProductModel>> FindAllProducts()
        {
            var response = await _client.GetAsync(basePath);
            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> FindProductById(long id)
        {
            var response = await _client.GetAsync($"{basePath}/{id}");
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> CreateProduct(ProductModel model)
        {
            var response = await _client.PostAsJson(basePath, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<ProductModel>();
            }
            else
            {
                throw new Exception("something went wrong when calling API");
            }
        }

        public async Task<ProductModel> UpdateProduct(ProductModel model)
        {
            var response = await _client.PutAsJsonAsync(basePath, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<ProductModel>();
            }
            else
            {
                throw new Exception("something went wrong when calling API");
            }
        }

        public async Task<bool> DeleteProductById(long id)
        {
            var response = await _client.DeleteAsync($"{basePath}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<bool>();
            }
            else
            {
                throw new Exception("something went wrong when calling API");
            }
        }
    }
}
