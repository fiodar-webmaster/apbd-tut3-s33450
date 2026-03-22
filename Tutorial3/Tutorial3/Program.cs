using Tutorial3.Logic;
using Tutorial3.UI;

Console.WriteLine("Welcome to the equipment rental service!");

var repository = new RentalRepository();
var service = new  RentalService(repository);
Seeder.Seed(repository);

var ui = new RentalUI(repository, service);
ui.Run();
