using System;
using System.Collections.Generic;
using WaesDiffly.CBL.Helper;
using WaesDiffly.Model.Constants;
using WaesDiffly.Model.Models;
using WaesDifflyModel.Constants;

namespace WaesDiffly.CBL
{
    /// <summary>
    /// A class for implementing all business operations and connected to database services.
    /// </summary>
    public class DiffBL
    {
        /// <summary>
        /// Create or update record acccording to given ID
        /// </summary>
        /// <param name="id">Primary key for getting diffobj</param>
        /// <param name="diffRecord">value to be added or updated</param>
        /// <returns>Returns responsemessage according to inner operations success or error.</returns>
        public ResponseMessage AddOrUpdate(int id, DiffRecord diffRecord)
        {
            var objResponse = GetDiffObj(id);
                return objResponse.ReturnCode == RetCode.Success ?
                        UpdateCurrentObj(id, diffRecord) :
                        CreateNewObj(new DiffObj(id, diffRecord));
               
        }

        /// <summary>
        /// It is public a function that compares object if exist in database.
        /// </summary>
        /// <param name="id">Primary key for getting diffobj</param>
        /// <returns>If there is no record with this Id, it returns record not found. If exist, it controls whether both sides are set or not. Evertything is ok, it compare values and return result. </returns>
        public ResponseMessage<DiffResponse> CompareSides(int id)
        {
            var objResponse = GetDiffObj(id);
            if(objResponse.ReturnCode !=  RetCode.Success)
            {
                return new ResponseMessage<DiffResponse>().RecordNotFoundError();
            }
            var obj = objResponse.Message;
            if (obj.LeftRecord == null)
            {
                return new ResponseMessage<DiffResponse>().BusinessError(ErrorText.LeftSideHasNotDefined);
            }
            if (obj.RightRecord == null)
            {
                return new ResponseMessage<DiffResponse>().BusinessError(ErrorText.RightSideHasNotDefined);
            }
            byte[] left = Convert.FromBase64String(obj.LeftRecord.Base64Value);
            byte[] right = Convert.FromBase64String(obj.RightRecord.Base64Value);

            var response = new DiffResponse();
            if(left.Length != right.Length)
            {
                response.Status = InfoText.NotEqualSize;
                return new ResponseMessage<DiffResponse>() { Message = response };
            }
            var result = FindDiff(left, right);
            if(result.Count == 0)
            {
                response.Status = InfoText.Equal;
            }
            else
            {
                response.Status = InfoText.NotEqualContent;
                response.DiffResultList = result;
            }
            return new ResponseMessage<DiffResponse>() { Message = response };
        }

        /// <summary>
        /// Kontrols two byte arrays, if they are equal, or not same size or same size but different contents.
        /// </summary>
        /// <param name="leftBytes">first byte array</param>
        /// <param name="rightBytes">second byte array</param>
        /// <returns>return Equal or Not Equal Size or Not Equal Content(with the list of differences with offset and length))</returns>
        private List<DiffResult> FindDiff(byte[] leftBytes, byte[] rightBytes)
        {
            bool offsetOpen = false;
            List<DiffResult> response = new List<DiffResult>();
            DiffResult result = null;
            for (int i = 0; i < leftBytes.Length; i++)
            {
                if (leftBytes[i] == rightBytes[i])
                {
                    if (offsetOpen)
                    {
                        offsetOpen = false;
                        response.Add(result);
                    }
                    continue;
                }
                if (!offsetOpen)
                {
                    offsetOpen = true;
                    result = new DiffResult();
                    result.Offset = i;
                    result.Length = 1;
                    continue;
                }
                result.Length++;
            }
            if (offsetOpen)
            {
                response.Add(result);
            }
            return response;
        }

        /// <summary>
        /// Select diffObj using given Id
        /// </summary>
        /// <param name="id">Primary key for getting diffobj</param>
        /// <returns>If value exist, return success response message with value otherwise returns record not found error.</returns>
        private ResponseMessage<DiffObj> GetDiffObj(int id)
        {
            return DatabaseHelperFactory.DataLayer.GetDiffObj(id);
        }

        /// <summary>
        /// Adds new item to static list
        /// </summary>
        /// <param name="obj">New item to be added</param>
        /// <returns>It always returns response message which is success</returns>
        private ResponseMessage CreateNewObj(DiffObj obj)
        {
            return DatabaseHelperFactory.DataLayer.CreateNewObj(obj);
            
        }

        /// <summary>
        /// Update existing value with new values using given Id
        /// </summary>
        /// <param name="id">For select existing Item</param>
        /// <param name="record">Value to be updated</param>
        /// <returns>If value cannot be found, returns record notfound error. Otherwise, returns success response message</returns>
        private ResponseMessage UpdateCurrentObj(int id, DiffRecord diffRecord)
        {
            return DatabaseHelperFactory.DataLayer.UpdateCurrentObj(id,diffRecord);
            
        }
    }
}
