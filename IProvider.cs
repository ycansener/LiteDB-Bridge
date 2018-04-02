using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paspartooo.Master.Libs.Data
{
    public interface IProvider<T>
    {
        bool DeleteItem(Guid id);
        bool DeleteItem(T item);
        T GetItemById(Guid id);
        List<T> GetAllItems();
        bool UpdateItem(T item);
        Guid InsertItem(T item);
    }
}
