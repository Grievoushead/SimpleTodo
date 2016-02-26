using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MyChecklists.Dtos;
using SQLite;

namespace MyChecklists.Infra
{
    //This class for perform all database CRUD operations 
    public class DatabaseHelperClass
    {
        SQLiteConnection dbConn;

        //Create Tabble 
        public async Task<bool> onCreate(string DB_PATH)
        {
            try
            {
                if (!CheckFileExists(DB_PATH).Result)
                {
                    using (dbConn = new SQLiteConnection(DB_PATH))
                    {
                        dbConn.CreateTable<Contacts>();
                    }
                }
                return true;
            }
            catch
            {
                return false;
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

        public List<TodoListDto> GetLists()
        {
            var result = new List<TodoListDto>();
            using (var db = new SQLiteConnection(App.DB_PATH))
            {
                result = db.Table<TodoListDto>().ToList<TodoListDto>();
            }

            return result;
        }

        public List<TodoItemDto> GetTodos(string id)
        {
            var result = new List<TodoItemDto>();
            using (var db = new SQLiteConnection(App.DB_PATH))
            {
                result = db.Table<TodoItemDto>().Where(x => x.TodoListId == id).ToList<TodoItemDto>();
            }

            return result;
        }

        public void InsertList(TodoListDto list)
        {
            using (var db = new SQLiteConnection(App.DB_PATH))
            {
                db.RunInTransaction(() =>
                {
                    db.Insert(list);
                });
            }
        }

        public void InsertItem(TodoItemDto item)
        {
            using (var db = new SQLiteConnection(App.DB_PATH))
            {
                db.RunInTransaction(() =>
                {
                    db.Insert(item);
                });
            }
        }

        public void UpdateItem(TodoItemDto item)
        {
            using (var db = new SQLiteConnection(App.DB_PATH))
            {
                var existingconact = db.Query<TodoItemDto>("select * from TodoItemDto where Id ='" + item.Id + "'").FirstOrDefault();
                if (existingconact != null)
                {
                    existingconact.Checked = item.Checked;
                    db.RunInTransaction(() =>
                    {
                        db.Update(existingconact);
                    });
                }
            }
        }

        public void DeleteItem(String id)
        {
            using (var db = new SQLiteConnection(App.DB_PATH))
            {
                var existingconact = db.Query<TodoItemDto>("select * from TodoItemDto where Id ='" + id + "'").FirstOrDefault();
                if (existingconact != null)
                {
                    db.RunInTransaction(() =>
                    {
                        db.Delete(existingconact);
                    });
                }
            }
        }

        public void DeleteList(String id)
        {
            using (var db = new SQLiteConnection(App.DB_PATH))
            {
                var existingconact = db.Query<TodoListDto>("select * from TodoListDto where Id ='" + id + "'").FirstOrDefault();
                if (existingconact != null)
                {
                    db.RunInTransaction(() =>
                    {
                        db.Delete(existingconact);
                    });
                }
            }
        }



        // Retrieve the specific contact from the database. 
        public Contacts ReadContact(int contactid)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingconact = dbConn.Query<Contacts>("select * from Contacts where Id =" + contactid).FirstOrDefault();
                return existingconact;
            }
        }
        // Retrieve the all contact list from the database. 
        public ObservableCollection<Contacts> ReadContacts()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<Contacts> myCollection = dbConn.Table<Contacts>().ToList<Contacts>();
                ObservableCollection<Contacts> ContactsList = new ObservableCollection<Contacts>(myCollection);
                return ContactsList;
            }
        }

        //Update existing conatct 
        public void UpdateContact(Contacts contact)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingconact = dbConn.Query<Contacts>("select * from Contacts where Id =" + contact.Id).FirstOrDefault();
                if (existingconact != null)
                {
                    existingconact.Name = contact.Name;
                    existingconact.PhoneNumber = contact.PhoneNumber;
                    existingconact.CreationDate = contact.CreationDate;
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Update(existingconact);
                    });
                }
            }
        }
        // Insert the new contact in the Contacts table. 
        public void Insert(Contacts newcontact)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Insert(newcontact);
                });
            }
        }

        //Delete specific contact 
        public void DeleteContact(int Id)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingconact = dbConn.Query<Contacts>("select * from Contacts where Id =" + Id).FirstOrDefault();
                if (existingconact != null)
                {
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Delete(existingconact);
                    });
                }
            }
        }
        //Delete all contactlist or delete Contacts table 
        public void DeleteAllContact()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                //dbConn.RunInTransaction(() => 
                //   { 
                dbConn.DropTable<Contacts>();
                dbConn.CreateTable<Contacts>();
                dbConn.Dispose();
                dbConn.Close();
                //}); 
            }
        }
    }

}