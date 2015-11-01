using Ninject.Modules;
using RestSharp;

namespace RestSharpApiDemo.Bootstrap
{
    public class IocModule : NinjectModule
    {
        public override void Load()
        {
            Config config = new Config();

            Bind<IRestClient>().To<RestClient>().WithConstructorArgument(config.GetBaseUrl());   
        }
    }
}