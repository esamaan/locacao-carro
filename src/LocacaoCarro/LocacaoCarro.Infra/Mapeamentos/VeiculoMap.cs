using AutoMapper;
using LocacaoCarro.Dominio.ObjetosValor;
using LocacaoCarro.Infra.BDModelos;
using LocacaoCarro.Dominio.Entidades.Veiculos;
using LocacaoCarro.Dominio.Enums;

namespace LocacaoCarro.Infra.Mapeamentos
{
    public class VeiculoMap : Profile
    {
        public VeiculoMap()
        {
            CreateMap<VeiculoBDModelo, Veiculo>()
                .ForMember(dest => dest.Identificador, m => m.Ignore())
                .ForMember(dest => dest.Placa, m => m.Ignore())
                .ForMember(dest => dest.AnoFabricacao, m => m.MapFrom(src => src.AnoFabricacao))
                .ForMember(dest => dest.IdModelo, m => m.MapFrom(src => src.IdModelo))
                .ForMember(dest => dest.Situacao, m => m.MapFrom(src => src.Situacao))
                .ConstructUsing(src =>
                    new Veiculo(
                        new Identificador(src.Identificador),
                        new Placa(src.Placa),
                        src.AnoFabricacao,
                        src.IdModelo,
                        (SituacaoVeiculo)src.Situacao
                    )
                );
        }
    }
}