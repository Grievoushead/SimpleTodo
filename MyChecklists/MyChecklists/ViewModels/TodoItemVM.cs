using System;
using MyChecklists.Common;
using MyChecklists.Dtos;
using MyChecklists.Infra;

namespace MyChecklists.ViewModels
{
    public class TodoItemVM
    {
        private DatabaseHelperClass db = new DatabaseHelperClass();

        public string Id { get; set; }

        public String Title { get; private set; }

        public bool Checked { get; set; }

        public RelayCommand Toggle { get; private set; }

        public TodoItemVM(String title, Boolean check, String id)
        {
            this.Id = id;
            this.Title = title;
            this.Checked = check;
            this.Toggle = new RelayCommand(() =>
            {
                // persist
                db.UpdateItem(new TodoItemDto()
                {
                    Id = this.Id,
                    Checked = this.Checked
                });
            });
        }
    }
}