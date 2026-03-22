namespace Tutorial3.Models.Users;

public class Employee : User
{
    public override string UserType  => "Employee";
    
    public Employee(string name, string surname, string email) : base(name, surname, email) {}
}