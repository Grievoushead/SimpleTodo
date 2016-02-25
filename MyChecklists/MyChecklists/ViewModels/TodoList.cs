using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyChecklists.ViewModels
{
    public class TodoList
    {
        public string Title { get; private set; }

        public ObservableCollection<TodoItem> Todos { get; private set; }

        public void Clean()
        {
            for (int i = this.Todos.Count - 1; i >= 0; i--)
            {
                if (this.Todos[i].Checked)
                    this.Todos.RemoveAt(i);
            }
        }

        public TodoList(string title, IEnumerable<TodoItem> todos)
        {
            this.Title = title;
            this.Todos = new ObservableCollection<TodoItem>(todos);
        }
    }
}