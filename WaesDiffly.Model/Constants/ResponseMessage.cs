using System;
using System.Runtime.Serialization;
using WaesDifflyModel.Constants;
namespace WaesDiffly.Model.Constants
{
    /// <summary>
    /// Generic response message class for using in every object. 
    /// This is not for this project spesific, I use this class on every project I implement. 
    /// </summary>
    [DataContract]
    public class ResponseMessage<T> : ResponseMessage
    {
        public ResponseMessage()
            : base()
        {

        }

        public ResponseMessage(ResponseMessage response)
            : base(response)
        {

        }

        [DataMember]
        public T Message { get; set; }

        #region IResponseMessage Members

        public new ResponseMessage<T> BusinessError()
        {
            base.BusinessError();
            return this;
        }
        public new ResponseMessage<T> BusinessError(string errorMessage)
        {
            base.BusinessError(errorMessage);
            return this;
        }
        public new ResponseMessage<T> RecordNotFoundError()
        {
            base.RecordNotFoundError();
            return this;
        }

        #endregion
    }

    [DataContract]
    public class ResponseMessage : IResponseMessage
    {
        public ResponseMessage()
        {
            this.ReturnCode = RetCode.Success;
            this.ErrorType = ResponseErrorType.NoError;
            this.ErrorMessage = String.Empty;
        }
        public ResponseMessage(ResponseMessage response)
        {
            this.ReturnCode = response.ReturnCode;
            this.ErrorType = response.ErrorType;
            this.ErrorMessage = response.ErrorMessage;
        }

        [DataMember]
        public RetCode ReturnCode { get; set; }

        [DataMember]
        public ResponseErrorType ErrorType { get; set; }

        [DataMember]
        public string ErrorCode { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public int TotalRecordCount { get; set; }

        #region IResponseMessage Members

        public ResponseMessage BusinessError()
        {
            this.ReturnCode = RetCode.Fail;
            this.ErrorType = ResponseErrorType.Business;
            this.ErrorMessage = ErrorText.SystemError;

            return this;
        }
        public ResponseMessage BusinessError(string errorMessage)
        {
            this.ReturnCode = RetCode.Fail;
            this.ErrorType = ResponseErrorType.Business;
            this.ErrorMessage = errorMessage;

            return this;
        }
        public ResponseMessage RecordNotFoundError()
        {
            this.ReturnCode = RetCode.RecordNotFound;
            this.ErrorType = ResponseErrorType.Business;
            this.ErrorMessage = ErrorText.RecordNotFound;

            return this;
        }

        #endregion
    }

    public interface IResponseMessage
    {
        ResponseMessage BusinessError();
        ResponseMessage BusinessError(string errorMessage);
        ResponseMessage RecordNotFoundError();
    }

    [DataContract]
    public enum ResponseErrorType
    {
        [EnumMember]
        NoError,
        [EnumMember]
        System,
        [EnumMember]
        Business
    }

    [DataContract]
    public enum RetCode
    {
        [EnumMember]
        Success = 1,
        [EnumMember]
        Fail,
        [EnumMember]
        RecordNotFound,
        [EnumMember]
        DuplicateRecord,
    }
}
