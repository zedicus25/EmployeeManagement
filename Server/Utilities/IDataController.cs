using System.Collections.Generic;

namespace Server.Utilities
{
    public interface IDataController<T>
    {
        List<T> ReadData();

        void SaveData(List<T> data);
    }
}
