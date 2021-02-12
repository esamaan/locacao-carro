using AutoMapper;
using LocacaoCarro.Dominio.ObjetosValor;
using LocacaoCarro.Infra.BDModelos;
using LocacaoCarro.Dominio.Entidades.Veiculos;

namespace LocacaoCarro.Infra.Mapeamentos
{
    public class CategoriaMap : Profile
    {
        public CategoriaMap()
        {
            CreateMap<CategoriaBDModelo, Categoria>()
                .ForMember(dest => dest.Identificador, m => m.Ignore())
                .ForMember(dest => dest.Descricao, m => m.Ignore())
                .ForMember(dest => dest.Preco, m => m.Ignore())
                .ConstructUsing(src =>
                    new Categoria(
                        new Identificador(src.Identificador),
                        new Descricao(src.Descricao),
                        new Preco(src.Preco)
                    )
                );
        }
    }
}