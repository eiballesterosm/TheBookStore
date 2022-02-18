using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using TheBookStore.Contracts;
using TheBookStore.Datastores;

namespace TheBookStore.Infrastructure
{
    public class ServiceResolver : IDependencyResolver
    {
        static IUnitOfWork store = new SampleDataStore();

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.BaseType == typeof(ApiController))
            {
                return Activator.CreateInstance(serviceType, store);
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }
    }
}