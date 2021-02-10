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
            CreateMap<ClienteBDModelo, Cliente>()
                .ForMember(dest => dest.Nome.PrimeiroNome, m => m.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Nome.Sobrenome, m => m.MapFrom(src => src.Sobrenome))
                .ForMember(dest => dest.HashSenha, m => m.MapFrom(src => src.HashSenha))
                .ForMember(dest => dest.Cpf, m => m.MapFrom(src => src.Cpf))
                .ForMember(dest => dest.Endereco.Cep, m => m.MapFrom(src => src.Cep))
                .ForMember(dest => dest.Endereco.Logradouro, m => m.MapFrom(src => src.Logradouro))
                .ForMember(dest => dest.Endereco.Numero, m => m.MapFrom(src => src.Numero))
                .ForMember(dest => dest.Endereco.Complemento, m => m.MapFrom(src => src.Complemento))
                .ForMember(dest => dest.Endereco.Cidade, m => m.MapFrom(src => src.Cidade))
                .ForMember(dest => dest.Endereco.Estado, m => m.MapFrom(src => src.Estado))
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