using AutoMapper;
using LocacaoCarro.Dominio.ObjetosValor;
using LocacaoCarro.Infra.BDModelos;
using LocacaoCarro.Dominio.Entidades.Veiculos;

namespace LocacaoCarro.Infra.Mapeamentos
{
    public class MarcaMap : Profile
    {
        public MarcaMap()
        {
            CreateMap<MarcaBDModelo, Marca>()
                .ForMember(dest => dest.Identificador, m => m.Ignore())
                .ForMember(dest => dest.Nome, m => m.Ignore())
                .ConstructUsing(src =>
                    new Marca(
                        new Identificador(src.Identificador),
                        new Descricao(src.Nome)
                    )
                );
        }
    }
}