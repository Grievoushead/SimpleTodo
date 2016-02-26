using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyChecklists.Common;
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
            var listsModel = new List<TodoListVM>();
            var lists = db.GetLists();
            foreach (var list in lists)
            {
                var todos = db.GetTodos(list.Id);
                var listModel = new TodoListVM(list.Title, todos.Select(x=>new TodoItemVM(x.Title, x.Checked, x.Id)));
                listModel.Id = list.Id;
                listsModel.Add(listModel);
            }

            this.Lists = new ObservableCollection<TodoListVM>(listsModel);

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