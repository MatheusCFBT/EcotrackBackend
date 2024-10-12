namespace EcotrackBusiness.Validations.DocValidation
{
     public class Utils
    {
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