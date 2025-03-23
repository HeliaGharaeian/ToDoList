namespace ToDoList.Model
{
    public class TaskResponseModel
    {
        public Guid Id { get; set; } // Use Guid for the primary key
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
