﻿using AutoMapper;
using LocacaoCarro.Aplicacao.Modelos.Usuarios;
using LocacaoCarro.Dominio.ObjetosValor;
using LocacaoCarro.Dominio.Entidades.Usuarios;

namespace LocacaoCarro.Aplicacao.Mapeamentos
{
    public class OperadorMap : Profile
    {
        public OperadorMap()
        {
            CreateMap<Operador, OperadorModel>()
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome.PrimeiroNome))
                .ForMember(dest => dest.Sobrenome, m => m.MapFrom(src => src.Nome.Sobrenome))
                .ForMember(dest => dest.Matricula, m => m.MapFrom(src => src.Matricula.Numero));

            CreateMap<OperadorModel, Operador>()
                .ForMember(dest => dest.Matricula, m => m.MapFrom(src => src.Matricula))
                .ConstructUsing(src =>
                    new Operador(
                        new Matricula(src.Matricula),
                        new Nome(src.Nome, src.Sobrenome))
                    );
        }
    }
}
