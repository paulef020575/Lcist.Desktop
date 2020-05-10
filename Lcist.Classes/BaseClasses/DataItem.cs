using System.Data.Common;


namespace Lcist.Classes.BaseClasses
{
    public abstract class DataItem
    {
        protected abstract void ReadItemProperties(DbDataReader reader);
        public abstract string GetDescription();
    }
}
