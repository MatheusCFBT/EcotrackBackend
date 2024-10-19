namespace EcotrackBusiness.Validations.DocValidation
{
     public class Utils
    {
        // Verifica se os digitos do Cpf sao apenas numeros
        public static string ApenasNumeros(string valor)
        {
            var onlyNumber = "";

            foreach(var digito in valor)
            {
                if (char.IsDigit(digito))
                {
                    onlyNumber += digito;
                }
            }

            return onlyNumber.Trim();
        }
    }
}