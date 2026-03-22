namespace Tutorial3.Models.Users;

public abstract class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    
    protected  User(string name, string surname, string email)
    {
        Name = name;
        Surname =  surname;
        Email = email;
    }
}