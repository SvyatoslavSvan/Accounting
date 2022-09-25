namespace Accounting.DAL.Result.Provider.Base
{
    public class BaseResult<T>
    {
        public BaseResult(bool succed, T data)
        {
            Succed = succed;
            Data = data;
        }
        public bool Succed { get; }
        public T Data { get; }
    }
}
