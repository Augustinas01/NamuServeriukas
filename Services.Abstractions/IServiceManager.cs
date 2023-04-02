using Services.Abstractions.Facades;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IFactorioService FactorioService { get; }
    }
}
