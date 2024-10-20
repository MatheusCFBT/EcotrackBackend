namespace EcotrackBusiness.Validations.DocValidation
{
    public class DigitoVerificador
    {
        // Armazena o numero base sobre o qual os calculos de digito verificador serao feitos
        private string _numero;
        
        // Define a constante Modulo, que sera usada no calculo do digito verificador
        private const int Modulo = 11;

        // Lista de multiplicadores padrão usados no calculo do digito verificador
        private readonly List<int> _multiplicadores = [2, 3, 4, 5, 6, 7, 8, 9];
        
        // Dictionary de substituicoes opcionais
        private readonly Dictionary<int, string> _substituicoes = [];

        // Define se o complementar do mOdulo serA utilizado no cAlculo
        private readonly bool _complementarDoModulo = true;

        // Inicializa a instancia da classe com o numero base que sera utilizado para o calculo do digito verificador
        public DigitoVerificador(string numero)
        {
                _numero = numero;
        }

        // Permite configurar os multiplicadores que serao usados durante o calculo. Substitui os multiplicadores padrao
        public DigitoVerificador ComMultiplicadoresDeAte(int primeiroMultiplicador, int ultimoMultiplicador)
        {
            _multiplicadores.Clear();
            for(var i = primeiroMultiplicador; i <= ultimoMultiplicador; i++)
            {
                _multiplicadores.Add(i);
            }
            
            return this;
        }

        // Adiciona substituicoes de digitos ao Dictionary _substituicoes. Isso permite que certos resultados sejam substituidos por valores especificos, como em casos de excecao
        public DigitoVerificador Substituindo(string substituto, params int[] digitos)
        {
            foreach(var digito in digitos)
            {
                _substituicoes[digito] = substituto;
            }

            return this;
        }

        // Adiciona um digito ao final do número base
        public void AddDigito(string digito)
        {
            _numero = string.Concat(_numero,digito);
        }

        public string CalculaDigito()
        {
            return !(_numero.Length > 0) ? "" : GetDigitSum();
        }

        // Realiza o calculo da soma dos digitos do numero base multiplicado pelos respectivos multiplicadores e calcula o modulo para encontrar o digito verificador
        private string GetDigitSum()
        {
            var soma = 0;
            for(int i = _numero.Length -1, m = 0; i >= 0; i--)
            {
                var produto = (int)char.GetNumericValue(_numero[i]) * _multiplicadores[m];
                soma += produto;

                if (++m >= _multiplicadores.Count) m = 0;
            }

            var mod = (soma % Modulo);
            var resultado = _complementarDoModulo ? Modulo - mod : mod;

            return _substituicoes.ContainsKey(resultado) ? _substituicoes[resultado] : resultado.ToString();
        }
    }
}