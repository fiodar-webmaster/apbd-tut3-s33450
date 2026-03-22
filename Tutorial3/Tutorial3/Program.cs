using Tutorial3.Logic;
using Tutorial3.Models.Equipment;
using Tutorial3.Models.Users;
using Tutorial3.UI;

Console.WriteLine("Welcome to the equipment rental service!");

var repository = new RentalRepository();
var service = new  RentalService(repository);
Seeder.Seed(repository);

var ui = new RentalUI(repository, service);
ui.Run();


while (run)
{
    Console.WriteLine("Available commands:");
    Console.WriteLine("1. Add a new user to the system.");
    Console.WriteLine("2. Add a new equipment item of a selected type.");
    Console.WriteLine("3. Display the full list of equipment together with current status.");
    Console.WriteLine("4. Display only equipment currently available for rental.");
    Console.WriteLine("5. Rent equipment to a user.");
    Console.WriteLine("6. Return equipment and calculate a possible late penalty.");
    Console.WriteLine("7. Mark equipment as unavailable, for example because of damage or maintenance.");
    Console.WriteLine("8. Display active rentals for a selected user.");
    Console.WriteLine("9. Display the list of overdue rentals.");
    Console.WriteLine("10. Generate a short summary report of the rental service state.");
    Console.WriteLine("11. Exit");




}




 