using WaesDifflyModel.Models;
namespace WaesDiffly.Model.Models
{
    /// <summary>
    /// Defines new record properties with side and content
    /// </summary>
    public class DiffRecord
    {
        public DiffSide Side { get; }
        public string Base64Value { get; }

        public DiffRecord(DiffSide side, string base64Value)
        {
            Side = side;
            Base64Value = base64Value;
        }
    }
}
