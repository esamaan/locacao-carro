using AutoMapper;
using LocacaoCarro.Api.Modelos;
using LocacaoCarro.Dominio.Entidades;
using LocacaoCarro.Dominio.ObjetosValor;

namespace LocacaoCarro.Api.Mapeamentos
{
    public class ClienteMap : Profile
    {
        public ClienteMap()
        {
            CreateMap<Cliente, EnderecoModel>()
                .ForMember(dest => dest.Cep, m => m.MapFrom(src => src.Endereco.Cep))
                .ForMember(dest => dest.Cidade, m => m.MapFrom(src => src.Endereco.Cidade))
                .ForMember(dest => dest.Complemento, m => m.MapFrom(src => src.Endereco.Complemento))
                .ForMember(dest => dest.Estado, m => m.MapFrom(src => src.Endereco.Estado))
                .ForMember(dest => dest.Logradouro, m => m.MapFrom(src => src.Endereco.Logradouro))
                .ForMember(dest => dest.Numero, m => m.MapFrom(src => src.Endereco.Numero));

            CreateMap<Cliente, ClienteModel>()
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome.PrimeiroNome))
                .ForMember(dest => dest.Sobrenome, m => m.MapFrom(src => src.Nome.Sobrenome))
                .ForMember(dest => dest.Cpf, m => m.MapFrom(src => src.Cpf.Numero))
                .ForMember(dest => dest.Endereco, m => m.MapFrom(src => src))
                .ForMember(dest => dest.Aniversario, m => m.MapFrom(src => src.Aniversario));

            CreateMap<ClienteModel, Cliente>()
                .ForMember(dest => dest.Nome, m => m.Ignore())
                .ForMember(dest => dest.Cpf, m => m.Ignore())
                .ForMember(dest => dest.Endereco, m => m.Ignore())
                .ConstructUsing(src =>
                    new Cliente(
                        new Nome(src.Nome, src.Sobrenome),
                        new Cpf(src.Cpf),
                        new Endereco(src.Endereco.Cep, src.Endereco.Logradouro, src.Endereco.Numero, src.Endereco.Complemento, src.Endereco.Cidade, src.Endereco.Estado),
                        src.Aniversario)
                    );
        }
    }
}