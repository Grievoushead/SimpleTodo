using System.Collections.Generic;
using TodoApp.Dtos;

namespace TodoApp.ViewModels
{
    public class MainPageVM
    {
        private List<TodoListDto> todoLists = new List<TodoListDto>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        public MainPageVM()
        {
            this.todoLists.Add(new TodoListDto("LITSABBUR", @"Assets/Images/ikea-manuals-1.jpg"));
            this.todoLists.Add(new TodoListDto("TJARDIIS",@"Assets/Images/ikea-manuals-2.jpg"));
            this.todoLists.Add(new TodoListDto("DINDASÜR",@"Assets/Images/ikea-manuals-3.jpg"));
        }

        /// <summary>
        /// Gets the manuals.
        /// </summary>
        public List<TodoListDto> Manuals
        {
            get { return this.todoLists; }
        }
    }
}
