using System;

namespace MyChecklists.Dtos
{
    public class TodoItemDto : BaseDto
    {
        public String Title { get; set; }

        public Boolean Checked { get; set; }

        public String TodoListId { get; set; }
    }
}