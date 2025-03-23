namespace ToDoList.Model
{
    public class TaskModel
    {
        public Guid Id { get; set; } // Use Guid for the primary key
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
