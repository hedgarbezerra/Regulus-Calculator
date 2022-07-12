using Microsoft.Extensions.DependencyInjection;

namespace Regulus.Common.Vinculadores
{
    public interface IVinculadorDependencia
    {
        void Vincular(IServiceCollection services);
    }
}
