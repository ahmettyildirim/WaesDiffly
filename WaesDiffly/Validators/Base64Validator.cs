using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using WaesDifflyModel.Constants;
/// <summary>
/// It is a validataor that controls whether given content is base64string or not
/// </summary>
namespace WaesDiffly.Validators
{
    public class Base64Validator
    {
        public sealed class Base64 : ValidationAttribute
        {

            protected override ValidationResult IsValid(object data, ValidationContext validationContext)
            {
                var content = data.ToString().Trim();
                var result = (content.Length % 4 == 0) && Regex.IsMatch(content, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
                if (result)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorText.InvalidBase64Data);
                }
            }

        }
    }
}