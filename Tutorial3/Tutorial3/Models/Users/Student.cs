namespace Tutorial3.Models.Users;

public class Student : User
{
    public override string UserType  => "Student";
    
    public Student(string name, string surname, string email) : base(name, surname, email) {}
}