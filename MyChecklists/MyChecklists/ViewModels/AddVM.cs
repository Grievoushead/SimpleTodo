using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyChecklists.Common;
using MyChecklists.Dtos;
using MyChecklists.Infra;

namespace MyChecklists.ViewModels
{
    public class AddVM
    {
        private DatabaseHelperClass db = new DatabaseHelperClass();

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
                    
                    var listDto = new TodoListDto()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = this.Title
                    };

                    var listModel = new TodoList(this.Title, new List<TodoItem>());
                    listModel.Id = listDto.Id;

                    db.InsertList(listDto);
                    this.lists.Add(listModel);
                }
                else
                {
                    

                    var itemDto = new TodoItemDto()
                    {
                        Checked = false,
                        Id = Guid.NewGuid().ToString(),
                        Title = this.Title,
                        TodoListId = currentList.Id
                    };

                    var itemModel = new TodoItem(this.Title, false, itemDto.Id);
                    itemModel.Id = itemDto.Id;

                    db.InsertItem(itemDto);

                    this.currentList.Todos.Add(itemModel);
                }
            });
        }
    }
}