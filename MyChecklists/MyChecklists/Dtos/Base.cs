using System;

namespace MyChecklists.Dtos
{
    public class BaseDto
    {
        [SQLite.PrimaryKey]
        public String Id { get; set; } 
    }
}