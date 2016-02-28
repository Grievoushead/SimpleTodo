using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyChecklists.Infra;

namespace MyChecklists.ViewModels
{
    public class TodoListVM : BaseVM
    {
        private DatabaseHelperClass db = new DatabaseHelperClass();

        public String Id { get; set; }

        public String Title { get; private set; }

        public String SecondaryTitle
        {
            get
            {
                return String.Format("Done {0} of {1}", this.Todos.Count(x => x.Checked), this.Todos.Count);
            }
        }

        public ObservableCollection<TodoItemVM> Todos { get; private set; }

        public void AddTodo(TodoItemVM todo)
        {
            this.Todos.Add(todo);
            todo.Toggled += this.RerenderSecondaryTitle;
            this.RerenderSecondaryTitle();
        }

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

            this.RerenderSecondaryTitle();
        }

        public TodoListVM(string title, IEnumerable<TodoItemVM> todos)
        {
            this.Title = title;
            this.Todos = new ObservableCollection<TodoItemVM>(todos);
            foreach (var todo in Todos)
            {
                todo.Toggled += RerenderSecondaryTitle;
            }
        }

        private void RerenderSecondaryTitle()
        {
            base.OnPropertyChanged("SecondaryTitle");
        }
    }
}