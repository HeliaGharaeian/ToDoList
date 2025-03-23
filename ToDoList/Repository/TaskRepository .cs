using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Model;
using ToDoList.Model.Base;
using static ToDoList.Repository.TaskRepository;

namespace ToDoList.Repository
{
    public interface ITaskRepository
    {
        Task<CustomActionResult<List<TaskResponseModel>>> GetAllTasksAsync();
        Task<CustomActionResult<TaskResponseModel>> GetTaskByIdAsync(Guid id);
        Task<CustomActionResult> AddTaskAsync(TaskRequestModel task);
        Task<CustomActionResult> UpdateTaskAsync(Guid id, TaskRequestModel request);
        Task<CustomActionResult> DeleteTaskAsync(Guid id);
    }

    public class TaskRepository : ITaskRepository
    {
        private readonly TodoContext _context;

        public TaskRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<CustomActionResult<List<TaskResponseModel>>> GetAllTasksAsync()
        {
            var tasks = await _context.Tasks.ToListAsync();
            var response = tasks.Select(t => new TaskResponseModel
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedDate = t.CreatedDate,
                UpdatedDate = t.UpdatedDate
            }).ToList();

            return new CustomActionResult<List<TaskResponseModel>>
            {
                IsSuccess = true,
                ResponseDesc = "Tasks retrieved successfully.",
                Data = response
            };
        }

        public async Task<CustomActionResult<TaskResponseModel>> GetTaskByIdAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return new CustomActionResult<TaskResponseModel>
                {
                    IsSuccess = false,
                    ResponseDesc = "Task not found.",
                    Data = null
                };
            }

            var response = new TaskResponseModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedDate = task.CreatedDate,
                UpdatedDate = task.UpdatedDate
            };

            return new CustomActionResult<TaskResponseModel>
            {
                IsSuccess = true,
                ResponseDesc = "Task retrieved successfully.",
                Data = response
            };
        }

        public async Task<CustomActionResult> AddTaskAsync(TaskRequestModel request)
        {
            var task = new TaskResponseModel
            {
                Id = Guid.NewGuid(), // Automatically generate the ID
                Title = request.Title,
                Description = request.Description,
                IsCompleted = request.IsCompleted,
                CreatedDate = DateTime.UtcNow, // Set the creation timestamp
                UpdatedDate = null // UpdatedDate is initially null
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return new CustomActionResult
            {
                IsSuccess = true,
                ResponseDesc = "Task added successfully."
            };
        }

        public async Task<CustomActionResult> UpdateTaskAsync(Guid id, TaskRequestModel request)
        {
            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null)
            {
                return new CustomActionResult
                {
                    IsSuccess = false,
                    ResponseDesc = "Task not found."
                };
            }

            existingTask.Title = request.Title;
            existingTask.Description = request.Description;
            existingTask.IsCompleted = request.IsCompleted;
            existingTask.UpdatedDate = DateTime.UtcNow; // Set the update timestamp

            _context.Entry(existingTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new CustomActionResult
            {
                IsSuccess = true,
                ResponseDesc = "Task updated successfully."
            };
        }

        public async Task<CustomActionResult> DeleteTaskAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return new CustomActionResult
                {
                    IsSuccess = false,
                    ResponseDesc = "Task not found."
                };
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return new CustomActionResult
            {
                IsSuccess = true,
                ResponseDesc = "Task deleted successfully."
            };
        }
    }
}

