using DevTask.Core.Models.Entity;

namespace DevTask.Core.Models.Dto
{
    public sealed class CreateTask
    {
        public string Title { get; private set; }
        public bool IsCompleted { get; private set; }
        public PriorityType Priority { get; private set; }

        private CreateTask(string title, bool isCompleted, PriorityType priority)
        {
            Title = title;
            IsCompleted = isCompleted;
            Priority = priority;
        }

        public static CreateTask ToDto(string title, bool? isCompleted, string priority)
        {
            return new CreateTask(title, isCompleted ?? false, Enum.Parse<PriorityType>(priority));
        }
    }
}
