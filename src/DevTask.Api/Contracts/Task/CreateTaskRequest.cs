namespace DevTask.Api.Contracts.Task
{
    public class CreateTaskRequest
    {
        public string Title { get; set; }
        public bool? IsCompleted { get; set; }
        public string Priority { get; set; }
    }
}
