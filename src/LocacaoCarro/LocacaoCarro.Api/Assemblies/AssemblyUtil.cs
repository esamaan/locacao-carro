using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace LocacaoCarro.Api.Assemblies
{
    [ExcludeFromCodeCoverage]
    public static class AssemblyUtil
    {
        public static IEnumerable<Assembly> GetCurrentAssemblies()
        {
            return new Assembly[]
            {
                Assembly.Load("LocacaoCarro.Api"),
                Assembly.Load("LocacaoCarro.Aplicacao"),
                Assembly.Load("LocacaoCarro.Dominio"),
                Assembly.Load("LocacaoCarro.Infra")
            };
        }
    }
}
