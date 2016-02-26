using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyChecklists.Dtos;
using MyChecklists.StartUp;
using SQLite;

namespace MyChecklists.Infra
{
    // This class for perform all database CRUD operations 
    public class DatabaseHelperClass
    {
        public bool CreateIfNotExists(string DB_PATH)
        {
            try
            {
                if (!CheckFileExists(DB_PATH).Result)
                {
                    using (var db = new SQLiteConnection(DB_PATH))
                    {
                        db.CreateTable<TodoListDto>();
                        db.CreateTable<TodoItemDto>();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public List<TodoListDto> GetLists()
        {
            var result = new List<TodoListDto>();
            using (var db = new SQLiteConnection(DbConfig.DB_PATH))
            {
                result = db.Table<TodoListDto>().ToList<TodoListDto>();
            }

            return result;
        }

        public List<TodoItemDto> GetTodos()
        {
            var result = new List<TodoItemDto>();
            using (var db = new SQLiteConnection(DbConfig.DB_PATH))
            {
                result = db.Table<TodoItemDto>()/*.Where(x => x.TodoListId == id)*/.ToList<TodoItemDto>();
            }

            return result;
        }

        public void InsertList(TodoListDto list)
        {
            using (var db = new SQLiteConnection(DbConfig.DB_PATH))
            {
                db.RunInTransaction(() =>
                {
                    db.Insert(list);
                });
            }
        }

        public void InsertItem(TodoItemDto item)
        {
            using (var db = new SQLiteConnection(DbConfig.DB_PATH))
            {
                db.RunInTransaction(() =>
                {
                    db.Insert(item);
                });
            }
        }

        public void UpdateItem(TodoItemDto item)
        {
            using (var db = new SQLiteConnection(DbConfig.DB_PATH))
            {
                db.RunInTransaction(() =>
                {
                    db.Execute(string.Format("update TodoItemDto set Checked={0} where Id='{1}'", item.Checked ? 1 : 0, item.Id));
                });
            }
        }

        public void DeleteItem(String id)
        {
            using (var db = new SQLiteConnection(DbConfig.DB_PATH))
            {
                db.RunInTransaction(() =>
                {
                    db.Execute(string.Format("delete from TodoItemDto where Id='{0}'", id));
                });
            }
        }

        public void DeleteList(String id)
        {
            using (var db = new SQLiteConnection(DbConfig.DB_PATH))
            {
                db.RunInTransaction(() =>
                {
                    db.Execute(string.Format("delete from TodoListDto where Id='{0}'", id));
                });
            }
        }

        // Flush all tables
        public void FlushAll()
        {
            using (var db = new SQLiteConnection(DbConfig.DB_PATH))
            {
                //dbConn.RunInTransaction(() => 
                //   { 
                db.DropTable<TodoListDto>();
                db.DropTable<TodoItemDto>();
                db.CreateTable<TodoListDto>();
                db.CreateTable<TodoItemDto>();
                db.Dispose();
                db.Close();
                //}); 
            }
        }

        private async Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}