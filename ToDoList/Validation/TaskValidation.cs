using ToDoList.Model.Base;
using ToDoList.Model;

namespace ToDoList.Validation
{
    public interface ITaskValidation
    {
        Task<CustomActionResult> Validate(TaskRequestModel task);

    }
    public class TaskValidation: ITaskValidation
    {
        public async Task<CustomActionResult> Validate(TaskRequestModel task)
        {
            CustomActionResult result = new CustomActionResult() { IsSuccess = true };

            if (string.IsNullOrWhiteSpace(task.Title))
            {
                return new CustomActionResult
                {
                    IsSuccess = false,
                    ResponseDesc = "Title is required."
                };
            }

            if (string.IsNullOrWhiteSpace(task.Description))
            {
                return new CustomActionResult
                {
                    IsSuccess = false,
                    ResponseDesc = "Description is required."
                };
            }

            return result;
        }

       
    }
}
