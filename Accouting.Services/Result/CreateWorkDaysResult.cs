using Accounting.DAL.Result.Provider.Base;
using Accounting.Domain.Models;

namespace Accouting.Domain.Managers.Result
{
    public class CreateWorkDaysResult : BaseResult<IList<WorkDay>>
    {
        public CreateWorkDaysResult(bool succed, IList<WorkDay> Data, OperationStatuses operationStatus, IList<BetEmployee> employees, HoursDaysInWorkMonth hoursDaysInWorkMonth) : base(succed, Data, operationStatus)
        {
            Employees = employees;
            HoursDaysInWorkMonth = hoursDaysInWorkMonth;
        }
        public IList<BetEmployee> Employees { get; set; }
        public HoursDaysInWorkMonth HoursDaysInWorkMonth { get; set; }
    }
}
