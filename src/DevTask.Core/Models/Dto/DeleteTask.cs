namespace DevTask.Core.Models.Dto
{
    public sealed class DeleteTask
    {
        private DeleteTask(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public static DeleteTask ToDto(string id)
        {
            return new DeleteTask(Guid.Parse(id));
        }
    }
}
