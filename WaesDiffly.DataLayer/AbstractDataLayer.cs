using WaesDiffly.Model.Constants;
using WaesDiffly.Model.Models;
/// <summary>
/// And abstract class for define necessarry functions for all derived database envireonment (mongo, sql or just static variable)
/// </summary>
namespace WaesDiffly.DataLayer
{
    public abstract class AbstractDataLayer
    {
        abstract public ResponseMessage<DiffObj> GetDiffObj(int id);
        abstract public ResponseMessage CreateNewObj (DiffObj obj);
        abstract public ResponseMessage UpdateCurrentObj (int id, DiffRecord record);
    }
}
