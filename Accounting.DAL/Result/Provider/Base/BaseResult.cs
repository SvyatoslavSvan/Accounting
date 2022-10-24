
namespace Accounting.DAL.Result.Provider.Base
{
    public class BaseResult<T>
    {
        public BaseResult(bool succed, T data)
        {
            Succed = succed;
            Data = data;
        }
        public BaseResult(bool succed, T data, OperationStatuses operationStatus)
        {
            OperationStatus = operationStatus;
            Succed = succed;
            Data = data;
        }
        public bool Succed { get; set; }
        public T Data { get; set;  }
        public OperationStatuses OperationStatus { get; set; }
    }
}
