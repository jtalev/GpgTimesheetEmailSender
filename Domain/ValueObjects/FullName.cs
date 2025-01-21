namespace GpgTimesheetEmailSender.Domain.ValueObjects
{
    public class FullName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public FullName(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentException("First name must not be empty");
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentException("Last name must not be empty");
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
