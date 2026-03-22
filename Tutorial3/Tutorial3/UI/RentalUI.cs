using System.Xml;
using Tutorial3.Logic;
using Tutorial3.Models.Equipment;
using Tutorial3.Models.Users;

namespace Tutorial3.UI;

public class RentalUI
{
    private RentalRepository _repository;
    private RentalService _service;

    public RentalUI(RentalRepository repository, RentalService service)
    {
        _repository = repository;
        _service = service;
    }

    public void Run()
    {
        bool run = true;
        while (run)
        {
            DisplayMenu();
            string line =  Console.ReadLine();

            switch (line)
            {
                case "1" : HandleAddUser(); break;
                case "2" : HandleAddEquipment(); break;
                case "3" : HandleDisplayAllEquipment(); break;
                case "4" : HandleDisplayAvailableEquipment(); break;
            }
            
        }
    }

    public void DisplayMenu()
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

    public void HandleAddUser()
    {
        Console.WriteLine("Select user type:");
        Console.WriteLine("Enter 1 for \"Student\", 2 for \"Employee\"");
        string userType = Console.ReadLine();
        if (!"12".Contains(userType))
        {
            Console.WriteLine("Invalid user type. Returning to the main menu");
            return;
        }
        
        Console.WriteLine("Enter the user first name");
        string firstName = Console.ReadLine();
        Console.WriteLine("Enter the user last name");
        string lastName = Console.ReadLine();
        Console.WriteLine("Enter the user email address");
        string email = Console.ReadLine();
        
        if (userType == "1") _repository.AddUser( new Student(firstName, lastName, email));
        else _repository.AddUser(new Employee(firstName, lastName, email));
    }

    public void HandleAddEquipment()
    {
        Console.WriteLine("Select equipment type:");
        Console.WriteLine("Enter 1 for \"Laptop\", 2 for \"Camera\", 3 for \"Projector\"");
        string equipmentType = Console.ReadLine();
        if (!"123".Contains(equipmentType))
        {
            Console.WriteLine("Invalid equipment type. Returning to the main menu");
            return;
        }
        
        Console.WriteLine("Enter equipment name");
        string equipmentName = Console.ReadLine();

        if (equipmentType == "1")
        {
            Console.WriteLine("Enter resolution with a space (e.g. 1920 1080)");
            string[] parts = Console.ReadLine().Split(' ');
            int width = int.Parse(parts[0]);
            int height = int.Parse(parts[1]);
            Console.WriteLine("Enter the processor name");
            string processorName = Console.ReadLine();
            
            _repository.AddEquipment(new Laptop(equipmentName, new ScreenResolution(width, height),  processorName));
        }
        
        else if (equipmentType == "2")
        {
            Console.WriteLine("Enter focal length (number)");
            string focalLength = Console.ReadLine();
            Console.WriteLine("Does it has bluetooth? 1: yes, 2: no ");
            string bluetooth = Console.ReadLine();
            _repository.AddEquipment(new Camera(equipmentName, int.Parse(focalLength), bluetooth == "1"));
        }
        
        else if (equipmentType == "3")
        {
            Console.WriteLine("Enter brightness (number)");
            int brightness =  int.Parse(Console.ReadLine());
            Console.WriteLine("Does it has speakers? 1: yes, 2: no ");
            string speakers = Console.ReadLine();
            _repository.AddEquipment(new Projector(equipmentName, brightness, speakers == "1"));
        }

        else Console.WriteLine("Invalid equipment type. Returning to the main menu");
    }

    public void HandleDisplayAllEquipment()
    {

        if (_repository.EquipmentCount == 0)
        {
            Console.WriteLine("No equipment found");
            return;
        }
        
        foreach (var equipment in _repository.GetEquipment())
        {
            Console.WriteLine(equipment.Name + (equipment.IsAvailable ? " (available)" : " (not available)"));
        }
    }

    public void HandleDisplayAvailableEquipment()
    {
        bool noAvailableEquipment = true;
        foreach (var equipment in _repository.GetEquipment())
        {
            if (equipment.IsAvailable)
            {
                Console.WriteLine(equipment.Name);
                noAvailableEquipment = false;
            }
        }

        if (noAvailableEquipment)
        {
            Console.WriteLine("No equipment available currently");
        }
    }
}