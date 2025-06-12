using System;
using System.Threading.Tasks;
using CustomerRelationship.Domain.SeedWork;

namespace CustomerRelationship.Application.Common
{
    public class BaseService
    {
        protected readonly INotification _notification;
        public BaseService(INotification notification)
        {
            _notification = notification;
        }

        protected async Task<T> ExecuteAsync<T>(Func<Task<T>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                _notification.Notify("An error occurred: " + ex.Message);
                return default(T);
            }
        }

        protected void Notify(string message)
        {
            _notification.Notify(message);
        }
    }
}
