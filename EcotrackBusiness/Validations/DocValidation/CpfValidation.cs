namespace EcotrackBusiness.Validations.DocValidation
{
    public class CpfValidation
    {
        // Define uma constante que representa o tamanho exato de um Cpf valido 
        public const int TamanhoCpf = 11;

        // Valida se o CPF fornecido e valido, aplicando uma serie de verificacoes.
        public static bool Validar(string cpf)
        {
            // Normaliza o Cpf para conter apenas números utilizando o método Utils.ApenasNumeros.
            var cpfNumeros = Utils.ApenasNumeros(cpf);

            // Verifica o tamanho do CPF
            if(!TamanhoValido(cpfNumeros)) return false;

            // Verifica se contem digitos repetidos e a validade dos digitos verificadores
            return !TemDigitosRepetidos(cpfNumeros) && TemDigitosValidos(cpfNumeros);
        }

        // Verifica se a string fornecida tem exatamente 11 caracteres
        private static bool TamanhoValido(string valor)
        {
            // Retorna true se o tamanho for igual a TamanhoCpf (11 digitos), e false caso contrario
            return valor.Length == TamanhoCpf;
        }

        // Verifica se o CPF é composto por digitos repetidos
        private static bool TemDigitosRepetidos(string valor)
        {
            // Utiliza uma lista de números inválidos com todos os dígitos iguais para fazer a verificação
            string[] invalidNumbers =
            [
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            ];
            return invalidNumbers.Contains(valor);
        }

        // Valida os dois ultimos dígitos do CPF (dígitos verificadores) com base nos primeiros 9 digitos
        private static bool TemDigitosValidos(string valor)
        {   
            // Extrai os primeiros 9 digitos do CPF (sem os digitos verificadores)
            var number = valor.Substring(0, TamanhoCpf - 2);

            // Utiliza a classe DigitoVerificador para calcular os digitos verificadores com base em multiplicadores e substituicoes
            var digitoVerificador = new DigitoVerificador(number)
                .ComMultiplicadoresDeAte(2, 11)
                .Substituindo("0", 10, 11);

            var firstDigit = digitoVerificador.CalculaDigito();

            digitoVerificador.AddDigito(firstDigit);

            var secondDigit = digitoVerificador.CalculaDigito();

            // Compara os dois dígitos calculados com os dois dígitos finais do CPF original
            return string.Concat(firstDigit, secondDigit) == valor.Substring(TamanhoCpf - 2, 2);
        }
    }
}