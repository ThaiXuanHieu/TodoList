using System;
using System.Collections.Generic;

namespace TodoList.Api.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsComplete { get; set; }
        public List<Step> Steps { get; set; }
    }
}