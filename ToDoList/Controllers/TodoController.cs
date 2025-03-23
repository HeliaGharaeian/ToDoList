using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToDoList.Model;
using ToDoList.Service;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> CreateTask([FromBody] TaskModel task)
        {
            var result = await _taskService.AddTaskAsync(task);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
            }

            return BadRequest(result.ResponseDesc); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskModel task)
        {
            if (id != task.Id)
            {
                return BadRequest("Task ID mismatch."); 
            }

            var result = await _taskService.UpdateTaskAsync(task);
            if (result.IsSuccess)
            {
                return NoContent(); 
            }

            return BadRequest(result.ResponseDesc);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (result.IsSuccess)
            {
                return NoContent(); 
            }

            return NotFound(result.ResponseDesc); 
        }
    }
}