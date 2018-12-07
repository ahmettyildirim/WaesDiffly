using System;
using WaesDiffly.Model.Constants;
using WaesDiffly.Model.Models;
/// <summary>
/// Not implemented database option. 
/// </summary>
namespace WaesDiffly.DataLayer
{
    public class MongoLayer : AbstractDataLayer
    {
        public override ResponseMessage CreateNewObj(DiffObj obj)
        {
            throw new NotImplementedException();
        }

        public override ResponseMessage<DiffObj> GetDiffObj(int id)
        {
            throw new NotImplementedException();
        }

        public override ResponseMessage UpdateCurrentObj(int id, DiffRecord record)
        {
            throw new NotImplementedException();
        }
    }
}
