using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyChecklists.Common;

namespace MyChecklists.ViewModels
{
    public class AddVM
    {
        private readonly ObservableCollection<TodoList> lists;

        private readonly TodoList currentList;

        public String Title { get; set; }

        public Boolean AddNewList { get; set; }

        public RelayCommand Add { get; private set; }

        public AddVM() { }

        public AddVM(TodoList currentList, ObservableCollection<TodoList> lists)
        {
            this.currentList = currentList;
            this.lists = lists;
            this.Add = new RelayCommand(() =>
            {
                if (this.AddNewList)
                {
                    this.lists.Add(new TodoList(this.Title, new List<TodoItem>()));
                }
                else
                {
                    this.currentList.Todos.Add(new TodoItem(this.Title));
                }
            });
        }
    }
}