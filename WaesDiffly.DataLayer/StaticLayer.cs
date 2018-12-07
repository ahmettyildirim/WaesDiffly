using System.Collections.Generic;
using System.Linq;
using WaesDiffly.Model.Constants;
using WaesDiffly.Model.Models;
using WaesDifflyModel.Models;
/// <summary>
/// Defines save and get operations on static list 
/// </summary>
namespace WaesDiffly.DataLayer
{
    public class StaticLayer : AbstractDataLayer
    {
        /// <summary>
        /// static list for used in project
        /// </summary>
        private static List<DiffObj> objList = new List<DiffObj>();

        /// <summary>
        /// Adds new item to static list
        /// </summary>
        /// <param name="obj">New item to be added</param>
        /// <returns>It always returns response message which is success</returns>
        public override ResponseMessage CreateNewObj(DiffObj obj)
        {
            objList.Add(obj);
            return new ResponseMessage();
        }
        /// <summary>
        /// Select diffObj using given Id
        /// </summary>
        /// <param name="id">Primary key for getting diffobj</param>
        /// <returns>If value exist, return success response message with value otherwise returns record not found error.</returns>
        public override ResponseMessage<DiffObj> GetDiffObj(int id)
        {
            var item =  objList.FirstOrDefault(p => p.Id == id);
            return item != null ? new ResponseMessage<DiffObj>() { Message = item } : new ResponseMessage<DiffObj>().RecordNotFoundError();
        }
        /// <summary>
        /// Update existing value with new values using given Id
        /// </summary>
        /// <param name="id">For select existing Item</param>
        /// <param name="record">Value to be updated</param>
        /// <returns>If value cannot be found, returns record notfound error. Otherwise, returns success response message</returns>
        public override ResponseMessage UpdateCurrentObj(int id, DiffRecord record)
        {
            var objResponse = GetDiffObj(id);
            if (objResponse.ReturnCode != RetCode.Success)
            {
                return objResponse;
            }
            var obj = objResponse.Message;
            if(record.Side == DiffSide.Left)
            {
                obj.LeftRecord = record;
            }
            else
            {
                obj.RightRecord = record;
            }
            return new ResponseMessage();
        }
    }
}
