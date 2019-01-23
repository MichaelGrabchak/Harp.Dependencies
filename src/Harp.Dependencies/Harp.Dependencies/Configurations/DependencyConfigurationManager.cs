namespace Harp.Dependencies.Configurations
{
    public static class DependencyConfigurationManager
    {
        public static void AddConfiguration<T>() where T : IDependencyConfiguration, new()
        {
            IDependencyConfiguration dependencyConfig = Container.Resolve<T>();

            dependencyConfig.Configure();
        }
    }
}
