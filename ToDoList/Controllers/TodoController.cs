using Microsoft.AspNetCore.Mvc;
using ToDoList.Model;
using ToDoList.Service;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = await _taskService.GetAllTasksAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.ResponseDesc);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var result = await _taskService.GetTaskByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return NotFound(result.ResponseDesc);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskRequestModel request)
        {
            var result = await _taskService.AddTaskAsync(request);
            if (result.IsSuccess)
            {
                return Ok(result.ResponseDesc);
            }

            return BadRequest(result.ResponseDesc);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskRequestModel request)
        {
            var result = await _taskService.UpdateTaskAsync(id, request);
            if (result.IsSuccess)
            {
                return Ok(result.ResponseDesc);
            }

            return BadRequest(result.ResponseDesc);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.ResponseDesc);
            }

            return NotFound(result.ResponseDesc);
        }
    }
}