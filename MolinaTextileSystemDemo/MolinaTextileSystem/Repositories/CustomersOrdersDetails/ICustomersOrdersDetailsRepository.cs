using MolinaTextileSystem.Models;

namespace MolinaTextileSystem.Repositories.CustomersOrdersDetails
{
	public interface ICustomersOrdersDetailsRepository
	{
		void Add(CustomerOrderDetailModel customerOrderDetailModel);
		void Delete(int id);
		void Edit(CustomerOrderDetailModel customerOrderDetailModel);
		IEnumerable<CustomerOrderDetailModel> GetAll();
		IEnumerable<CustomerOrderModel> GetAllCustomerOrder();
		IEnumerable<ProductModel> GetAllProduct();
		CustomerOrderDetailModel? GetById(int id);
	}
}
