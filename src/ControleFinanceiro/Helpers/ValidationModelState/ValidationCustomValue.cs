using System.ComponentModel.DataAnnotations;


namespace ControleFinanceiro.Api.Helpers.ValidationModelState
{
    public class ValidationCustomValue : ValidationAttribute
    {
        public ValidationCustomValue(string error)
        {
            ErrorMessage = error;
        }


        public override bool IsValid(object value)
        {

            var teste = value.ToString().StartsWith("0");

            if (teste)
                return false;
           
            return true;


        }
    }
}
