using AutoMapper;
using LocacaoCarro.Dominio.Entidades.Veiculos;
using LocacaoCarro.Dominio.ObjetosValor;
using LocacaoCarro.Infra.BDModelos;

namespace LocacaoCarro.Infra.Mapeamentos
{
    public class ModeloMap : Profile
    {
        public ModeloMap()
        {
            CreateMap<ModeloBDModelo, Modelo>()
                .ForMember(dest => dest.Identificador, m => m.Ignore())
                .ForMember(dest => dest.Descricao, m => m.Ignore())
                .ForMember(dest => dest.Marca, m => m.Ignore())
                .ForMember(dest => dest.Combustivel, m => m.Ignore())
                .ForMember(dest => dest.Categoria, m => m.Ignore())
                .ForMember(dest => dest.LitrosBagageiro, m => m.MapFrom(src => src.LitrosBagageiro))
                .ForMember(dest => dest.NumeroOcupantes, m => m.MapFrom(src => src.NumeroOcupantes))
                .ForMember(dest => dest.AnoModelo, m => m.MapFrom(src => src.AnoModelo))
                .ConstructUsing(src =>
                    new Modelo(
                        new Identificador(src.Identificador),
                        new Descricao(src.Descricao),
                        src.LitrosBagageiro,
                        src.NumeroOcupantes,
                        src.AnoModelo,
                        new Marca(new Identificador(src.IdentificadorMarca), new Descricao(src.NomeMarca)),
                        new Combustivel(new Identificador(src.IdentificadorCombustivel), new Descricao(src.NomeCombustivel)),
                        new Categoria(new Identificador(src.IdentificadorMarca), new Descricao(src.DescricaoCategoria), new Preco(src.PrecoCategoria))
                    )
                );
        }
    }
}