using Tutorial3.Models.Equipment;
using Tutorial3.Models.Users;

namespace Tutorial3.Logic;

public class Seeder
{
    public static void Seed(RentalRepository repository)
    {
        repository.AddEquipment(new Laptop("Asus Vivobook 14", new ScreenResolution(2560, 1440), "Intel core i5"));
        repository.AddEquipment(new Laptop("HP OmniBook 5", new ScreenResolution(1920, 1080), "AMD Ryzen 5 3600"));
        repository.AddEquipment(new Camera("Nikon Coolpix", 28, true));
        repository.AddEquipment(new Camera("Sony A7", 24, false));
        repository.AddEquipment(new Projector("Samsung Freestyle 2nd gen", 200, true));
        repository.AddEquipment(new Projector("Philips NeoPix 100", 190, false));

        repository.AddUser(new Student("Nikita", "Pohrebniak", "qwerty@gmail.com"));
        repository.AddUser(new Student("Fiodar", "Klimovich", "s33450@pjwstk.edu.pl"));
        repository.AddUser(new Employee("John", "Brown", "johnbrown@outlook.com"));
        repository.AddUser(new Employee("Keanu", "Reeves", "realjohnwick@gmail.com"));
    }
}