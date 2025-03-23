namespace ToDoList.Model.Base
{
    public class CustomActionResult
    {
        public bool IsSuccess { get; set; }
        public string ResponseDesc { get; set; }
        public decimal ResponseType { get; set; }

    }
    public class CustomActionResultConvert<T> : CustomActionResult<T>
    {
        public object ErrorObject { get; set; }


    }
    public class CustomActionResult<T> : CustomActionResult
    {
        public T Data { get; set; }
    }
}
