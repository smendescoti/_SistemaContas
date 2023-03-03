using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentation.Models.Validations
{
    /// <summary>
    /// Validação customizada para campo de senha forte
    /// </summary>
    public class StrongPassword : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            //Verificando se o valor da variável é null (vazio)
            if(value != null)
            {
                //converter para texto (string)
                var password = value.ToString();

                //regras de validação
                return password.Length >= 8 //mínimo de 8 caracteres
                    && password.Length <= 20 //máximo de 20 caracteres
                    && password.Any(char.IsUpper) //1 letra maiúscula
                    && password.Any(char.IsLower) //1 letra minúscula
                    && password.Any(char.IsDigit) //1 dígito numérico
                    && ( //1 dos caracteres especiais abaixo
                        password.Contains("@") ||
                        password.Contains("#") ||
                        password.Contains("$") ||
                        password.Contains("&") ||
                        password.Contains("%") ||
                        password.Contains("!")
                    );
            }

            return false;
        }
    }
}
