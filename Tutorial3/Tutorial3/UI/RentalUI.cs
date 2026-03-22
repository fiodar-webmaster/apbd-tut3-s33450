using System.Globalization;
using System.Xml;
using Tutorial3.Logic;
using Tutorial3.Models.Equipment;
using Tutorial3.Models.Users;

namespace Tutorial3.UI;

public class RentalUI
{
    private RentalRepository _repository;
    private RentalService _service;
    private bool run = true;

    public RentalUI(RentalRepository repository, RentalService service)
    {
        _repository = repository;
        _service = service;
    }

    public void Run()
    {
        while (run)
        {
            DisplayMenu();
            string line = Console.ReadLine();

            switch (line)
            {
                case "1": HandleAddUser(); break;
                case "2": HandleAddEquipment(); break;
                case "3": HandleDisplayAllEquipment(); break;
                case "4": HandleDisplayAvailableEquipment(); break;
                case "5": RentEquipmentToUser(); break;
                case "6": ReturnEquipment(); break;
                case "7": ToggleAvailability(); break;
                case "8": DisplayActiveRentalsForUser(); break;
                case "9": DisplayOverDueRentals(); break;
                case "10" : DisplayReport(); break;
                case "11" : Exit(); break;
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

        if (userType == "1") _repository.AddUser(new Student(firstName, lastName, email));
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

            _repository.AddEquipment(new Laptop(equipmentName, new ScreenResolution(width, height), processorName));
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
            int brightness = int.Parse(Console.ReadLine());
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

    public void RentEquipmentToUser()
    {
        var users = _repository.GetUsers();
        if (users.Count == 0)
        {
            Console.WriteLine("No users available. Add a user first. Returning to the main menu.");
            return;
        }

        Console.WriteLine("Select a user");
        for (int i = 0; i < users.Count; i++)
        {
            Console.WriteLine($"{i + 1} : {users[i].Name} {users[i].Surname} ({users[i].GetType().Name})");
        }

        int userChoice = int.Parse(Console.ReadLine());
        if (userChoice < 1 || userChoice > users.Count)
        {
            Console.WriteLine("Invalid selection. Returning to the main menu.");
            return;
        }

        var selectedUser = users[userChoice - 1];

        var equipment = _repository.GetEquipment();
        if (equipment.Count == 0)
        {
            Console.WriteLine("No equipment available. Add an item first. Returning to the main menu.");
            return;
        }

        Console.WriteLine("Select an equipment item");

        for (int i = 0; i < equipment.Count; i++)
        {
            Console.WriteLine($"{i + 1} : {equipment[i].Name} {equipment[i].GetType().Name}");
        }

        int itemChoice = int.Parse(Console.ReadLine());
        if (itemChoice < 1 || itemChoice > equipment.Count)
        {
            Console.WriteLine("Invalid selection. Returning to the main menu.");
            return;
        }

        var selectedItem = equipment[itemChoice - 1];

        Console.Write("Enter rent period (days)");
        DateTime dueDate = DateTime.Now.AddDays(int.Parse(Console.ReadLine()));

        bool successfulRent = _service.RentItem(selectedUser, selectedItem, DateTime.Now, dueDate);

        if (successfulRent) Console.WriteLine("Success! Returning to the main menu.");
        else
            Console.WriteLine("Rental could not be completed. Possible reasons: too many active rentals for this user");

    }

    public void ReturnEquipment()
    {
        var activeRentals = _repository.GetRentals().Where(r => r.ReturnDate == null).ToList();
        if (activeRentals.Count == 0)
        {
            Console.WriteLine("No currently active rentals");
            return;
        }

        Console.WriteLine("Select a rental to return");
        for (int i = 0; i < activeRentals.Count; i++)
        {
            var rental = activeRentals[i];
            Console.WriteLine(
                $"{i + 1} {rental.BorrowedItem.Name}  ({rental.Borrower.Name} {rental.Borrower.Surname}) due to {rental.DueDate}");
        }

        int rentalNumber = int.Parse(Console.ReadLine());
        if (rentalNumber < 1 || rentalNumber > activeRentals.Count)
        {
            Console.WriteLine("Invalid selection. Returning to the main menu.");
            return;
        }

        var selectedRental = activeRentals[rentalNumber - 1];
        _service.ReturnItem(selectedRental, DateTime.Now);

        Console.WriteLine($"Rental returned. Fee: {selectedRental.Fee}");

    }

    public void ToggleAvailability()
    {

        Console.WriteLine("Select an equipment item to mark unavailable.");

        var equipment = _repository.GetEquipment();
        if (equipment.Count == 0)
        {
            Console.WriteLine("No equipment available. Add an item first. Returning to the main menu.");
            return;
        }

        Console.WriteLine("Select an equipment item");

        for (int i = 0; i < equipment.Count; i++)
        {
            Console.WriteLine(
                $"{i + 1} : {equipment[i].Name} {equipment[i].GetType().Name}  ({(equipment[i].IsAvailable ? "available" : "not available")})");
        }

        int itemChoice = int.Parse(Console.ReadLine());
        if (itemChoice < 1 || itemChoice > equipment.Count)
        {
            Console.WriteLine("Invalid selection. Returning to the main menu.");
            return;
        }

        var selectedItem = equipment[itemChoice - 1];

        selectedItem.IsAvailable = !selectedItem.IsAvailable;
        Console.WriteLine($"{selectedItem.Name} is now {(selectedItem.IsAvailable ? "available" : "not available")}");

    }

    public void DisplayActiveRentalsForUser()
    {
        var users = _repository.GetUsers();
        if (users.Count == 0)
        {
            Console.WriteLine("No users available. Add a user first. Returning to the main menu.");
            return;
        }

        Console.WriteLine("Select a user");
        for (int i = 0; i < users.Count; i++)
        {
            Console.WriteLine($"{i + 1} : {users[i].Name} {users[i].Surname} ({users[i].GetType().Name})");
        }

        int userChoice = int.Parse(Console.ReadLine());
        if (userChoice < 1 || userChoice > users.Count)
        {
            Console.WriteLine("Invalid selection. Returning to the main menu.");
            return;
        }

        var selectedUser = users[userChoice - 1];

        var activeRentals = _repository.GetActiveRentalsForUser(selectedUser);

        if (activeRentals.Count == 0)
        {
            Console.WriteLine("No active rentals for this user");
            return;
        }

        foreach (var activeRental in activeRentals)
        {
            Console.WriteLine($"{activeRental.BorrowedItem} with due date: {activeRental.DueDate}");
        }
    }

    public void DisplayOverDueRentals()
    {
        var overdueRentals = _repository.GetRentals()
            .Where(r => r.ReturnDate == null && DateTime.Now > r.DueDate)
            .ToList();

        if (overdueRentals.Count == 0)
        {
            Console.WriteLine("No overdue rentals found in the system");
            return;
        }
        
        foreach (var r in overdueRentals)
        {
            int daysLate = (DateTime.Now.Date - r.DueDate.Date).Days;
            Console.WriteLine($"{r.BorrowedItem.Name} {r.Borrower.Name} {r.Borrower.Surname} {r.DueDate} (late for {daysLate} days))");
        }
    }

    public void DisplayReport()
    {
        Console.WriteLine("System report:");
        Console.WriteLine($"Total users: {_repository.UserCount}");
        Console.WriteLine($"Total items: {_repository.EquipmentCount}");
        Console.WriteLine($"Total rentals: {_repository.RentalCount}");
        Console.WriteLine($"Available items: {_repository.AvailableEquipmentCount}");
        Console.WriteLine($"Active rentals: {_repository.ActiveRentalsCount}");
        
    }

    public void Exit()
    {
        Console.WriteLine("Shutting the system down...");
        run = false;
    }

}