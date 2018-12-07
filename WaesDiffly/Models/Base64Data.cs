using System.ComponentModel.DataAnnotations;
using static WaesDiffly.Validators.Base64Validator;

/// <summary>
/// defines model for json request, and includes validators like required and valid base64 string
/// </summary>
namespace WaesDiffly.Models
{
    public class Base64Data
    {
        [Required]
        [Base64]
        public string Content { get; set; }
    }
}