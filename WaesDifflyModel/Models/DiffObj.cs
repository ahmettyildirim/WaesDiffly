using System;
using System.Collections.Generic;
using System.Text;

namespace WaesDiffly.Model.Models
{
    public class DiffObj
    {

        public int Id { get; }
        public DiffSide Side  { get;  }
        public string Base64Value { get; }

        public DiffObj(int id, DiffSide side, string base64Value)
        {
            Id = id;
            Side = side;
            Base64Value = base64Value;
        }
    }
}
