using System;
using MyChecklists.Common;

namespace MyChecklists.ViewModels
{
    public class TodoItem
    {
        public String Title { get; private set; }

        public bool Checked { get; set; }

        public RelayCommand Toggle { get; private set; }

        public TodoItem(String title)
        {
            this.Title = title;
            this.Toggle = new RelayCommand(() =>
            {
                // persist
                var isChecked = this.Checked;
            });
        }
    }
}