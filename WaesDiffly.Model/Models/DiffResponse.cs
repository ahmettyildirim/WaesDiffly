using System.Collections.Generic;
namespace WaesDiffly.Model.Models
{
    /// <summary>
    /// Defines return object type. Includes status of comparing and if there are, list of differences.
    /// </summary>
    public class DiffResponse
    {
        public string Status { get; set; }
        public List<DiffResult> DiffResultList { get; set; }
    }
}
