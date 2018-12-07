using WaesDiffly.DataLayer;

/// <summary>
/// It s an helper for decide which database will be used. In this exercise, we use StaticLayer. 
/// </summary>
namespace WaesDiffly.CBL.Helper
{
    public static class DatabaseHelperFactory
    {
        public static AbstractDataLayer DataLayer { get; } = new StaticLayer();
    }
}
