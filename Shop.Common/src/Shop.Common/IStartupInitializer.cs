namespace Shop.Common
{
    public interface IStartupInitializer : IInitializer
    {
        void AddInitializer(IInitializer initializer);
    }
}