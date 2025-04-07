using Microsoft.AspNetCore.Mvc;
using ToDoList.Model;
using ToDoList.Service;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Model.Base.CustomActionResult<List<TaskResponseModel>> result = await _taskService.GetAllTasksAsync();
            return View(result.Data);  // Render a Razor View
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            Model.Base.CustomActionResult<TaskResponseModel> result = await _taskService.GetTaskByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound();  // Returns 404 page

            return View(result.Data);  // Render Details.cshtml
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new TaskRequestModel());  // Empty request model for form
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskRequestModel request)
        {
            if (!ModelState.IsValid)
                return View(request);  // Re-show form with errors

            Model.Base.CustomActionResult result = await _taskService.AddTaskAsync(request);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.ResponseDesc);
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Model.Base.CustomActionResult<TaskResponseModel> result = await _taskService.GetTaskByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound();

            // Convert ResponseModel to RequestModel for editing
            TaskRequestModel requestModel = new TaskRequestModel
            {
                Title = result.Data.Title,
                Description = result.Data.Description,
                IsCompleted = result.Data.IsCompleted
            };

            return View(requestModel);
        }

        // POST: /tasks/edit/{id}
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, TaskRequestModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            Model.Base.CustomActionResult result = await _taskService.UpdateTaskAsync(id, request);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.ResponseDesc);
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            Model.Base.CustomActionResult result = await _taskService.DeleteTaskAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.ResponseDesc);
            }

            return NotFound(result.ResponseDesc);
        }
    }
}