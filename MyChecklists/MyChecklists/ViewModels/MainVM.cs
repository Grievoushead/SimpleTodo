using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MyChecklists.Common;
using MyChecklists.Infra;
using MyChecklists.Views;

namespace MyChecklists.ViewModels
{
    public class MainVM
    {
        //private readonly INavService _NavService;

        public ObservableCollection<TodoList> Lists { get; private set; }

        public TodoList CurrentList { get; set; }

        public RelayCommand Clean { get; private set; }

        public RelayCommand Add { get; private set; }

        public RelayCommand DeleteList { get; private set; }

        public MainVM()
        {
            /*_NavService = new NavService();
            _NavService.Register("Add", typeof(AddView));*/

            this.Lists = new ObservableCollection<TodoList>()
            {
                new TodoList("Home", new List<TodoItem> {new TodoItem("Buy milk"), new TodoItem("Clean"), new TodoItem("Wash")}),
                new TodoList("Work", new List<TodoItem> {new TodoItem("Create presentation"), new TodoItem("Learn React"), new TodoItem("Write code")}),
                new TodoList("Hobbies", new List<TodoItem> {new TodoItem("Start running"), new TodoItem("Go gym"), new TodoItem("Get hobbie")}),
            };

            this.Clean = new RelayCommand(() =>
            {
                // do clean in current list
                this.CurrentList.Clean();
            });

            this.Add = new RelayCommand(async () =>
            {
                //var ls = this.CurrentList;
                //ls.Todos.Add(new TodoItem("test"));
                // do add
                //_NavService.GoTo("Add");
                var dlg = new AddDialog();
                var addVm = new AddVM(this.CurrentList, this.Lists);
                dlg.DataContext = addVm;
                await dlg.ShowAsync();
            });

            this.DeleteList = new RelayCommand(() =>
            {
                this.Lists.Remove(this.CurrentList);
            });
        }
    }
}