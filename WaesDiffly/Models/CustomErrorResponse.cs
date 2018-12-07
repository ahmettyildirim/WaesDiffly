using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WaesDiffly.Models
{
    public class CustomErrorResponse
    {
        public string ErrorMessage { get; set; }
        public string ErrorAction { get; set; }
        public string ErrorController { get; set; }
    }
}