using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyChecklists.Infra;

namespace MyChecklists.ViewModels
{
    public class TodoListVM
    {
        private DatabaseHelperClass db = new DatabaseHelperClass();

        public string Id { get; set; }

        public string Title { get; private set; }

        public ObservableCollection<TodoItemVM> Todos { get; private set; }

        public void Clean()
        {
            for (int i = this.Todos.Count - 1; i >= 0; i--)
            {
                if (this.Todos[i].Checked)
                {
                    db.DeleteItem(this.Todos[i].Id);
                    this.Todos.RemoveAt(i);
                }
                    
            }
        }

        public TodoListVM(string title, IEnumerable<TodoItemVM> todos)
        {
            this.Title = title;
            this.Todos = new ObservableCollection<TodoItemVM>(todos);
        }
    }
}