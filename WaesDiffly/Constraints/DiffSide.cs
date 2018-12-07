using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http.Routing;

namespace WaesDiffly.Constraints
{
    /// <summary>
    /// It's a constraint that controls if given url is a side property or not. 
    /// </summary>
    public class DiffSide : IHttpRouteConstraint
    {
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName,
            IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            string paramVal = values[parameterName].ToString();
            if(paramVal.ToLower().Equals(WaesDifflyModel.Models.DiffSide.Left.ToString().ToLower()) 
                || paramVal.ToLower().Equals(WaesDifflyModel.Models.DiffSide.Right.ToString().ToLower())){
                return true;
            }
            return false;
        }
    }
}