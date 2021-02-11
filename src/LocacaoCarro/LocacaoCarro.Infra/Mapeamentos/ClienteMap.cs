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
                .ForMember(dest => dest.Nome, m => m.Ignore())
                .ForMember(dest => dest.Endereco, m => m.Ignore())
                .ForMember(dest => dest.HashSenha, m => m.MapFrom(src => src.HashSenha))
                .ForMember(dest => dest.Cpf, m => m.MapFrom(src => new Cpf(src.Cpf)))
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