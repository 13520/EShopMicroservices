namespace BuildingBlocks.Exception
{
    public class NotFoundException : IOException
    {
        public NotFoundException(string message) : base(message)
        {

        }

        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }

    }
}
