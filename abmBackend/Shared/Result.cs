namespace abm.Shared
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }


        public Result<T> Ok(T data)
        {
            return new Result<T> { Success = true, Data = data };
        }

        public Result<T> Fail(string message)
        {
            return new Result<T> { Success = false, Message = message };
        }


    }
}
