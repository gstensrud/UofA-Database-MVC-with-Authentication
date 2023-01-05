namespace EXSM3935_C_Sharp_Final_Project.Models.Exceptions
{
    public class Validation : Exception
    {
        // Can store other exceptions. It's Exception-Inception!
        public List<Exception> SubExceptions { get; set; } = new List<Exception>();

        // Override message to display a summary of any sub-exceptions.
        public override string Message => $"There are {(SubExceptions.Count > 0 ? SubExceptions.Count : "no")} exceptions.";

        // A quick property to determine if we have any exceptions currently.
        public bool Any => SubExceptions.Any();

        // Constructors.
        public Validation() : base() { }

        public Validation(string message) : base(message)
        {
            SubExceptions.Add(new Exception(message));
        }
    }
}