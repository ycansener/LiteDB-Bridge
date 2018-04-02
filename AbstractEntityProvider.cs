using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paspartooo.Master.Libs.Data
{
    public class AbstractEntityProvider<T> : IProvider<T> where T : AbstractEntityModel
    {
        // Edit your db name or just remove the constant keyword and use the magic of the inheritance instead of messing with the core code :)
        public const string DBName = "paspartooo_db";
        public string DBPath = string.Empty;
        public string CompletePath = string.Empty;
        public string TableName = string.Empty;

        public AbstractEntityProvider(string tableName)
        {
            DBPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\NoSql\\";
            CompletePath = DBPath + DBName + ".db";
            TableName = tableName;

            if (!Directory.Exists(DBPath))
            {
                Directory.CreateDirectory(DBPath);
            }
        }

        public virtual bool DeleteItem(Guid id)
        {
            try
            {
                using (var db = new LiteDatabase(CompletePath))
                {
                    // Get a collection (or create, if doesn't exist)
                    var table = db.GetCollection<T>(TableName);
                    table.Delete(id);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual bool DeleteItem(T item)
        {
            return DeleteItem(item.Id);
        }

        public virtual List<T> GetAllItems()
        {
            try
            {
                List<T> items = null;
                using (var db = new LiteDatabase(CompletePath))
                {
                    // Get a collection (or create, if doesn't exist)
                    var table = db.GetCollection<T>(TableName);
                    var queryList = table.FindAll();
                    items = queryList.ToList();

                    return items;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual T GetItemById(Guid id)
        {
            try
            {
                using (var db = new LiteDatabase(CompletePath))
                {
                    // Get a collection (or create, if doesn't exist)
                    var table = db.GetCollection<T>(TableName);
                    var item = table.FindOne(x => x.Id == id);
                    return item;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual Guid InsertItem(T item)
        {
            try
            {
                Guid itemId = Guid.Empty;
                using (var db = new LiteDatabase(CompletePath))
                {
                    var table = db.GetCollection<T>(TableName);
                    item.Id = Guid.NewGuid();
                    table.Insert(item);
                    table.EnsureIndex(x => x.Id);

                    itemId = item.Id;
                }

                return itemId;
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
        }

        public virtual bool UpdateItem(T item)
        {
            throw new NotImplementedException();
        }
    }
}
