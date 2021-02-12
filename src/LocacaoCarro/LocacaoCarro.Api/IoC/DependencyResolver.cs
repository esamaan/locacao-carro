using LocacaoCarro.Aplicacao;
using LocacaoCarro.Aplicacao.Interfaces;
using LocacaoCarro.Dominio.Repositorios;
using LocacaoCarro.Infra.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace LocacaoCarro.Api.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            RegisterApplications(services);
            RegisterRepositories(services);
        }

        private static void RegisterApplications(IServiceCollection services)
        {
            services
                .AddScoped<IUsuarioApplicacao, UsuarioApplicacao>()
                .AddScoped<IVeiculoAplicacao, VeiculoAplicacao>()
            ;
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services
                .AddScoped<IClienteRepositorio, ClienteRepositorio>()
                .AddScoped<IOperadorRepositorio, OperadorRepositorio>()
                .AddScoped<ICategoriaRepositorio, CategoriaRepositorio>()
                .AddScoped<IMarcaRepositorio, MarcaRepositorio>()
                .AddScoped<IModeloRepositorio, ModeloRepositorio>()
                .AddScoped<IVeiculoRepositorio, VeiculoRepositorio>()
            ;
        }
    }
}
