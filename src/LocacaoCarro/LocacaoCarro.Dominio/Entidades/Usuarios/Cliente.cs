using Flunt.Validations;
using LocacaoCarro.Dominio.Enums;
using LocacaoCarro.Dominio.ObjetosValor;
using System;

namespace LocacaoCarro.Dominio.Entidades.Usuarios
{
    public class Cliente : Usuario
    {
        public Cpf Cpf { get; private set; }
        public Endereco Endereco { get; private set; }
        public DateTime Aniversario { get; private set; }

        public Cliente(Nome nome, Cpf cpf, Endereco endereco, DateTime aniversario, string hashSenha)
            : base(nome, hashSenha)
        {
            Cpf = cpf;
            Endereco = endereco;
            Aniversario = aniversario;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Nome, nameof(Nome), "Nome não pode ser nulo")
                .IsNotNull(Cpf, nameof(Cpf), "CPF não pode ser nulo")
                .IsNotNull(Endereco, nameof(Endereco), "Endereço não pode ser nulo"));

            if (Nome != null)
                AddNotifications(Nome);

            if (Cpf != null)
                AddNotifications(Cpf);

            if (Endereco != null)
                AddNotifications(Endereco);
        }

        public Cliente(Nome nome, Cpf cpf, Endereco endereco, DateTime aniversario)
            : this(nome, cpf, endereco, aniversario, string.Empty)
        {

        }

        public override string ObterPerfil()
        {
            return PerfilUsuario.Cliente.ToString();
        }

        public override string ObterNomeUsuario()
        {
            return Cpf.Numero;
        }
    }
}
