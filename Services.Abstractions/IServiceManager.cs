using Services.Abstractions.Factorio;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IFactorioService FactorioService { get; }
    }
}
