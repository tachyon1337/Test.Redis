using System;
using Microsoft.Practices.Unity;
using RevStack.Pattern;
using RevStack.Redis;
using Test.Redis.Models;

namespace Test.Redis.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        private const string REDIS_HOST = "54.227.253.243";
        private const int REDIS_PORT = 6379;

        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {

            container.RegisterType<RedisDataContext, RedisDataContext>(new PerRequestLifetimeManager(), new InjectionConstructor(REDIS_HOST,REDIS_PORT));
            container.RegisterType<IRepository<SampleModel, Guid>, RedisRepository<SampleModel, Guid>>();
            container.RegisterType<IService<SampleModel, Guid>, Service<SampleModel, Guid>>();
            container.RegisterType<IUnitOfWork<SampleModel, Guid>, RedisUnitOfWork<SampleModel, Guid>>();
        }
    }
}
