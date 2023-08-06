using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Core.Interfaces
{
    public interface IServiceInstaller
    {
        void Install(IServiceCollection services , IConfiguration configuration);
    }
}
