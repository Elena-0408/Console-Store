
namespace Store.Entities
{
  public abstract class Roles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsLoginIn { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} \n" +
                   $"Name: {Name} \n" +
                   $"Phone: {Phone} \n" +
                   $"Email: {Email} \n" +
                   $"Password: {Password} \n";
        }
    }
}
