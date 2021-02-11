using AutoMapper;
using LocacaoCarro.Dominio.Entidades;
using LocacaoCarro.Infra.BDModelos;

namespace LocacaoCarro.Infra.Mapeamentos
{
    public class OperadorMap : Profile
    {
        public OperadorMap()
        {
            CreateMap<OperadorBDModelo, Nome>()
                .ForMember(dest => dest.PrimeiroNome, m => m.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Sobrenome, m => m.MapFrom(src => src.Sobrenome));

            CreateMap<OperadorBDModelo, Operador>()
                .ForMember(dest => dest.HashSenha, m => m.MapFrom(src => src.HashSenha))
                .ForMember(dest => dest.Matricula, m => m.MapFrom(src => src.Matricula))
                .ConstructUsing(src =>
                    new Operador(
                        new Matricula(src.Matricula),
                        new Nome(src.Nome, src.Sobrenome),
                        src.HashSenha)
                    );
        }
    }
}
