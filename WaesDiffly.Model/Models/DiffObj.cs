using WaesDifflyModel.Models;
namespace WaesDiffly.Model.Models
{
    /// <summary>
    /// Defines a diff object ontains Id and both left and right sides content.
    /// </summary>
    public class DiffObj
    {
        public int Id { get; }
        public DiffRecord LeftRecord{ get; set; }
        public DiffRecord RightRecord { get; set; }

        public DiffObj(int id)
        {
            Id = id;
        }

        public DiffObj(int id, DiffRecord record)
        {
            Id = id;
            if(record.Side == DiffSide.Left)
            {
                LeftRecord = record;
            }
            else
            {
                RightRecord = record;
            }
        }
    }
}
