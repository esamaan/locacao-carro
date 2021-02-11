using Flunt.Validations;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LocacaoCarro.Dominio.ObjetosValor
{
    public class Cpf : ObjetoValor
    {
        public Cpf(string numero)
        {
            Numero = FiltrarApenasDigitos(numero);

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Numero, nameof(Cpf.Numero), "CPF não pode ser nulo ou branco")
                .HasLen(Numero, 11, nameof(Cpf.Numero), "CPF deve conter 11 posições")
                .IsDigit(Numero, nameof(Cpf.Numero), "CPF deve conter apenas números")
                .IsTrue(VerificarCpfValido(Numero), nameof(Cpf.Numero), "CPF inválido"));
        }

        public string Numero { get; private set; }

        public override string ToString()
        {
            return Numero;
        }

        private string FiltrarApenasDigitos(string entrada)
        {
            return !string.IsNullOrWhiteSpace(entrada) ? string.Join("", entrada.Where(char.IsDigit)) : string.Empty;
        }

        private bool VerificarCpfValido(string numeroCpf)
        {
            if (string.IsNullOrWhiteSpace(numeroCpf) || VerificaPossuiCaracterInvalido(numeroCpf))
                return false;

            numeroCpf = numeroCpf.Replace("-", "").Replace(".", "");
            string numeroCpfFormatado = ObterCpfFormatado(numeroCpf, false);
            char primeiroDigitoCpf = numeroCpfFormatado[0];
            if (numeroCpfFormatado.All(digitoCpf => digitoCpf == primeiroDigitoCpf))
                return false;

            string baseCpf = numeroCpfFormatado.Substring(0, numeroCpfFormatado.Length - 2);
            string digitosVerificadoresCpf = ObterDigitosVerificadoresCpf(baseCpf);
            if (numeroCpfFormatado != baseCpf + digitosVerificadoresCpf)
                return false;

            return true;
        }

        private bool VerificaPossuiCaracterInvalido(string numeroDocumento)
        {
            return numeroDocumento.Any(caractere => char.IsLetter(caractere) || char.IsSymbol(caractere));
        }

        private string ObterCpfFormatado(string numeroCpf, bool usarSeparadores)
        {
            if (string.IsNullOrWhiteSpace(numeroCpf))
                return null;

            int tamanhoCpfFormatado = usarSeparadores ? 14 : 11;
            if (numeroCpf.Length == tamanhoCpfFormatado)
                return numeroCpf;

            string numeroCpfFormatado = numeroCpf.PadLeft(11, '0');
            return usarSeparadores ? Regex.Replace(numeroCpfFormatado, "(\\d{3})(\\d{3})(\\d{3})(\\d{2})$", "$1.$2.$3-$4") : numeroCpfFormatado;
        }

        private string ObterDigitosVerificadoresCpf(string baseCpf)
        {
            StringBuilder digitosVerificadores = new StringBuilder();
            StringBuilder numeroCpfCompleto = new StringBuilder(baseCpf);
            while (numeroCpfCompleto.Length < 11)
            {
                int multiplicador = 9;
                int somaDigitos = 0;
                for (int i = numeroCpfCompleto.Length - 1; i >= 0; i--)
                {
                    somaDigitos += Convert.ToInt32(Convert.ToString(numeroCpfCompleto[i])) * multiplicador;
                    if (multiplicador != 0)
                        multiplicador -= 1;
                }

                int novoDigito = somaDigitos % 11;
                if (novoDigito >= 10)
                    novoDigito = 0;

                digitosVerificadores.Append(novoDigito);
                numeroCpfCompleto.Append(novoDigito);
            }

            return digitosVerificadores.ToString();
        }
    }
}