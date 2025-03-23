using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Model;
using ToDoList.Model.Base;
using static ToDoList.Repository.TaskRepository;

namespace ToDoList.Repository
{
    public interface ITaskRepository
    {
        Task<CustomActionResult<List<TaskModel>>> GetAllTasksAsync();
        Task<CustomActionResult<TaskModel>> GetTaskByIdAsync(Guid id);
        Task<CustomActionResult> AddTaskAsync(TaskModel task);
        Task<CustomActionResult> UpdateTaskAsync(TaskModel task);
        Task<CustomActionResult> DeleteTaskAsync(Guid id);
    }

    public class TaskRepository : ITaskRepository
    {
        private readonly TodoContext _context;

        public TaskRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<CustomActionResult<List<TaskModel>>> GetAllTasksAsync()
        {
            try
            {
                var tasks = await _context.Tasks.ToListAsync();
                return new CustomActionResult<List<TaskModel>>
                {
                    IsSuccess = true,
                    ResponseDesc = "Tasks retrieved successfully.",
                    Data = tasks
                };
            }
            catch (Exception ex)
            {
                return new CustomActionResult<List<TaskModel>>
                {
                    IsSuccess = false,
                    ResponseDesc = $"Error retrieving tasks: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<CustomActionResult<TaskModel>> GetTaskByIdAsync(Guid id)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task == null)
                {
                    return new CustomActionResult<TaskModel>
                    {
                        IsSuccess = false,
                        ResponseDesc = "Task not found.",
                        Data = null
                    };
                }

                return new CustomActionResult<TaskModel>
                {
                    IsSuccess = true,
                    ResponseDesc = "Task retrieved successfully.",
                    Data = task
                };
            }
            catch (Exception ex)
            {
                return new CustomActionResult<TaskModel>
                {
                    IsSuccess = false,
                    ResponseDesc = $"Error retrieving task: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<CustomActionResult> AddTaskAsync(TaskModel task)
        {
            try
            {
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();

                return new CustomActionResult
                {
                    IsSuccess = true,
                    ResponseDesc = "Task added successfully."
                };
            }
            catch (Exception ex)
            {
                return new CustomActionResult
                {
                    IsSuccess = false,
                    ResponseDesc = $"Error adding task: {ex.Message}"
                };
            }
        }

        public async Task<CustomActionResult> UpdateTaskAsync(TaskModel task)
        {
            try
            {
                _context.Entry(task).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new CustomActionResult
                {
                    IsSuccess = true,
                    ResponseDesc = "Task updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new CustomActionResult
                {
                    IsSuccess = false,
                    ResponseDesc = $"Error updating task: {ex.Message}"
                };
            }
        }

        public async Task<CustomActionResult> DeleteTaskAsync(Guid id)
        {
            try
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
            catch (Exception ex)
            {
                return new CustomActionResult
                {
                    IsSuccess = false,
                    ResponseDesc = $"Error deleting task: {ex.Message}"
                };
            }
        }
    }
}

