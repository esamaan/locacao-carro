using AutoMapper;
using LocacaoCarro.Dominio.ObjetosValor;
using LocacaoCarro.Dominio.Entidades.Usuarios;
using LocacaoCarro.Infra.BDModelos;

namespace LocacaoCarro.Infra.Mapeamentos
{
    public class OperadorMap : Profile
    {
        public OperadorMap()
        {
            CreateMap<OperadorBDModelo, Operador>()
                .ForMember(dest => dest.Nome, m => m.Ignore())
                .ForMember(dest => dest.HashSenha, m => m.MapFrom(src => src.HashSenha))
                .ForMember(dest => dest.Matricula, m => m.MapFrom(src => new Matricula(src.Matricula)))
                .ConstructUsing(src =>
                    new Operador(
                        new Matricula(src.Matricula),
                        new Nome(src.Nome, src.Sobrenome),
                        src.HashSenha)
                    );

        }
    }
}
