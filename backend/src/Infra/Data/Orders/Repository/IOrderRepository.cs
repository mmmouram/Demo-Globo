using System.Threading.Tasks;
using CustomerRelationship.Domain.Orders;
using CustomerRelationship.Domain;

namespace CustomerRelationship.Infra.Data.Orders.Repository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
    }
}
