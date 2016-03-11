using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
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
            todo.Toggled += this.TodoItemToggled;
            this.TodoItemToggled(todo.Id);
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
                todo.Toggled += TodoItemToggled;
            }
        }

        private void TodoItemToggled(string id)
        {
            this.ReorderTodos(id);
            this.RerenderSecondaryTitle();
        }

        private void ReorderTodos(string id)
        {
            /*// sort checked at the end
            if (this.Todos.Any(x => x.Checked))
            {
                var newIndex = 0;
                var oldIndex = 0;
                for (int i = 0; i < this.Todos.Count; i++)
                {
                    if (this.Todos[i].Id == id)
                    {
                        oldIndex = i;
                        if (this.Todos[i].Checked)
                        {
                            if (i < this.Todos.Count - 1 && this.Todos[i + 1].Checked)
                            {
                                // already in first of the checked
                                newIndex = oldIndex;
                                break;
                            }

                            // move to the position of first checked todo
                            var firstCheckedTodo = this.Todos.FirstOrDefault(x => x.Checked && x.Id != id);
                            if (firstCheckedTodo == null)
                            {
                                // move to the end
                                //this.Todos.Move(i, this.Todos.Count - 1);
                                newIndex = this.Todos.Count - 1;
                            }
                            else
                            {
                                var firstCheckedTodoPos = this.Todos.IndexOf(firstCheckedTodo);
                                newIndex = firstCheckedTodoPos;
                                //this.Todos.Move(i, firstCheckedTodoPos);
                            }
                            
                        }
                        else
                        {
                            // move to the top
                            //this.Todos.Move(i, 0);
                            newIndex = 0;
                        }
                        break;
                    }
                }

                if (newIndex != oldIndex)
                {
                    CoreWindow.GetForCurrentThread().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        this.Todos.Move(oldIndex, newIndex);
                    });

                }
                    
                //base.OnPropertyChanged("Todos");
            }*/
            this.Todos = new ObservableCollection<TodoItemVM>(this.Todos.OrderBy(x => x.Checked));
            base.OnPropertyChanged("Todos");
        }

        private void RerenderSecondaryTitle()
        {
            base.OnPropertyChanged("SecondaryTitle");
        }
    }
}