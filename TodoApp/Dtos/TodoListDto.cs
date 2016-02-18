using System;
using System.Collections.Generic;

namespace TodoApp.Dtos
{
    public class TodoListDto
    {
        public String Title { get; private set; }

        public List<string> Todos { get; private set; } 

        public String BackgroundImage { get; private set; }

        public TodoListDto(String title, String backgroundImage)
        {
            this.Title = title;
            this.BackgroundImage = backgroundImage;
            this.Todos = new List<string>(){"Buy a car", "Buy a house", "Go vacation"};
        }
    }
}
