using System.Threading.Tasks;
using ToDoList.Model;
using ToDoList.Model.Base;
using ToDoList.Repository;
using ToDoList.Validation;

namespace ToDoList.Service
{
    public interface ITaskService
    {
        Task<CustomActionResult<List<TaskModel>>> GetAllTasksAsync();
        Task<CustomActionResult<TaskModel>> GetTaskByIdAsync(Guid id);
        Task<CustomActionResult> AddTaskAsync(TaskModel task);
        Task<CustomActionResult> UpdateTaskAsync(TaskModel task);
        Task<CustomActionResult> DeleteTaskAsync(Guid id);
    }
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskValidation _taskValidation;

        public TaskService(ITaskRepository taskRepository, ITaskValidation taskValidation)
        {
            _taskRepository = taskRepository;
            _taskValidation = taskValidation;
        }

        public async Task<CustomActionResult<List<TaskModel>>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllTasksAsync();
        }

        public async Task<CustomActionResult<TaskModel>> GetTaskByIdAsync(Guid id)
        {
            return await _taskRepository.GetTaskByIdAsync(id);
        }

        public async Task<CustomActionResult> AddTaskAsync(TaskModel task)
        {
            CustomActionResult result = new CustomActionResult();
            result = await _taskValidation.Validate(task);
            return await _taskRepository.AddTaskAsync(task);
        }

        public async Task<CustomActionResult> UpdateTaskAsync(TaskModel task)
        {
            CustomActionResult result = new CustomActionResult();
            result = await _taskValidation.Validate(task);
            return await _taskRepository.UpdateTaskAsync(task);
        }

        public async Task<CustomActionResult> DeleteTaskAsync(Guid id)
        {
            return await _taskRepository.DeleteTaskAsync(id);
        }
    }
}
