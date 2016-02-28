using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyChecklists.Common;
using MyChecklists.Dtos;
using MyChecklists.Infra;
using MyChecklists.Views;

namespace MyChecklists.ViewModels
{
    public class MainVM
    {
        private DatabaseHelperClass db = new DatabaseHelperClass();

        public ObservableCollection<TodoListVM> Lists { get; private set; }

        public TodoListVM CurrentList { get; set; }

        public RelayCommand Clean { get; private set; }

        public RelayCommand Add { get; private set; }

        public RelayCommand DeleteList { get; private set; }

        public MainVM()
        {
            // get all lists and todos from db
            var lists = db.GetLists();
            var todos = db.GetTodos();

            // map onto viewmodels
            var listsModel = new List<TodoListVM>();

            foreach (var list in lists)
            {
                TodoListDto curList = list;
                var curTodos = todos.Where(x => x.TodoListId == curList.Id).ToList();

                var listModel = new TodoListVM(
                        list.Title,
                        curTodos.Select(x => new TodoItemVM(x.Title, x.Checked, x.Id))
                    );

                listModel.Id = list.Id;
                listsModel.Add(listModel);
            }

            this.Lists = new ObservableCollection<TodoListVM>(listsModel);

            // actions
            this.Clean = new RelayCommand(() =>
            {
                // do clean in current list
                this.CurrentList.Clean();
            });

            this.Add = new RelayCommand(async () =>
            {
                var dlg = new AddDialog();
                var addVm = new AddVM(this.CurrentList, this.Lists);
                dlg.DataContext = addVm;
                await dlg.ShowAsync();
            });

            this.DeleteList = new RelayCommand(() =>
            {
                db.DeleteList(this.CurrentList.Id);
                this.Lists.Remove(this.CurrentList);
            });
        }
    }
}