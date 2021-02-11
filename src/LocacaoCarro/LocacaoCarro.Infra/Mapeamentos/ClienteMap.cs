using AutoMapper;
using LocacaoCarro.Dominio.Entidades;
using LocacaoCarro.Dominio.ObjetosValor;
using LocacaoCarro.Infra.BDModelos;

namespace LocacaoCarro.Infra.Mapeamentos
{
    public class ClienteMap : Profile
    {
        public ClienteMap()
        {
            CreateMap<ClienteBDModelo, Nome>()
                .ForMember(dest => dest.PrimeiroNome, m => m.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Sobrenome, m => m.MapFrom(src => src.Sobrenome));

            CreateMap<ClienteBDModelo, Endereco>()
                .ForMember(dest => dest.Cep, m => m.MapFrom(src => src.Cep))
                .ForMember(dest => dest.Logradouro, m => m.MapFrom(src => src.Logradouro))
                .ForMember(dest => dest.Numero, m => m.MapFrom(src => src.Numero))
                .ForMember(dest => dest.Complemento, m => m.MapFrom(src => src.Complemento))
                .ForMember(dest => dest.Cidade, m => m.MapFrom(src => src.Cidade))
                .ForMember(dest => dest.Estado, m => m.MapFrom(src => src.Estado));

            CreateMap<ClienteBDModelo, Cliente>()
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src))
                .ForMember(dest => dest.HashSenha, m => m.MapFrom(src => src.HashSenha))
                .ForMember(dest => dest.Cpf, m => m.MapFrom(src => src.Cpf))
                .ForMember(dest => dest.Aniversario, m => m.MapFrom(src => src.Aniversario))
                .ConstructUsing(src =>
                    new Cliente(
                        new Nome(src.Nome, src.Sobrenome),
                        new Cpf(src.Cpf),
                        new Endereco(src.Cep, src.Logradouro, src.Numero, src.Complemento, src.Cidade, src.Estado),
                        src.Aniversario,
                        src.HashSenha)
                    );
        }
    }
}