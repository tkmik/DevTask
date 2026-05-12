namespace DevTask.Core.Models.Dto
{
    public sealed class CompleteTask
    {
        private CompleteTask(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public static CompleteTask ToDto(string id)
        {
            return new CompleteTask(Guid.Parse(id));
        }
    }
}
