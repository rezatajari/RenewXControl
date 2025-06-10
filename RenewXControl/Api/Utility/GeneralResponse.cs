namespace RenewXControl.Api.Utility
{
    public class GeneralResponse<T> 
    {
        private GeneralResponse(T data,string message,bool isSuccess)
        {
            Data = data;
            Message = message;
            IsSuccess = isSuccess;
        }
        private GeneralResponse(string message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }
        public bool IsSuccess { get;private set; }
        public string Message { get;private set; }
        public T Data { get; private set; }

        public static GeneralResponse<T> Success(T data, string message = "Operation successful", bool isSuccess = true)
            => new GeneralResponse<T>(data, message, isSuccess);

        public static GeneralResponse<T> Failure(string message = "Operation failed", bool isSuccess = false)
            => new GeneralResponse<T>(message,isSuccess);
    }
}
