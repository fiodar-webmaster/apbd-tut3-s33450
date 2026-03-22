using System.Xml;
using Tutorial3.Logic;
using Tutorial3.Models.Equipment;
using Tutorial3.Models.Users;

namespace Tutorial3.UI;

public class RentalUI
{
    private RentalRepository _repository;
    private RentalService _service;
    private bool _run = true;

    public RentalUI(RentalRepository repository, RentalService service)
    {
        _repository = repository;
        _service = service;
    }

    public void Run()
    {
        while (_run)
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
                default: Console.WriteLine("Invalid option. Please try again."); break;
            }

        }
    }

    public void DisplayMenu()
    {
        Console.WriteLine("======");
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
        Console.WriteLine("======");
        
    }


    private T? GetSelection<T>(List<T> list, string message)
    {
        if (list.Count == 0) return default;
        Console.WriteLine(message);
        for (int i = 0; i < list.Count; i++)
        {
            string info = list[i] switch
            {
                User u => $"{u.Name} {u.Surname} ({u.GetType().Name})",
                Equipment e => $"{e.Name} ({e.GetType().Name})",
                RentalAct r => $"{r.BorrowedItem.Name} (Rented by {r.Borrower.Name} {r.Borrower.Surname})",
                _ => list[i]?.ToString() ?? ""
            };
            Console.WriteLine($"{i+1}. {info}");
        }

        Console.WriteLine("Enter selection number: ");
        int choice = int.Parse(Console.ReadLine());
        if (choice > 0 && choice <= list.Count)
        {
            return list[choice - 1];
        }
        return default;
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

        Console.WriteLine("User added successfully!");
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
        
        Console.WriteLine("Item added successfully!");
    }

    public void HandleDisplayAllEquipment()
    {

        if (_repository.EquipmentCount == 0)
        {
            Console.WriteLine("No equipment found");
            return;
        }

        Console.WriteLine("All equipment:");

        foreach (var equipment in _repository.GetEquipment())
        {
            string physicalCondition = equipment.IsAvailable ? "Good condition" : "Damaged/Broken";
            string isRented = _repository.IsCurrentlyRented(equipment) ? "Is being rented currently" : "Ready for rent";
            Console.WriteLine($"Item name: {equipment.Name}");
            Console.WriteLine($"Item type: {equipment.GetType().Name}");
            Console.WriteLine($"Item physical condition: {physicalCondition}");
            Console.WriteLine($"Rental status: {isRented}");
            Console.WriteLine("=======");
            
        }
    }

    public void HandleDisplayAvailableEquipment()
    {

        var availableForRental = _repository.GetEquipment()
            .Where(e => e.IsAvailable && !_repository.IsCurrentlyRented(e))
            .ToList();

        if (availableForRental.Count == 0)
        {
            Console.WriteLine("No equipment available currently");
            return;
        }

        Console.WriteLine("Available equipment:");
        foreach (var item in availableForRental)
        {
            Console.WriteLine($"{item.Name} ({item.GetType().Name})");
        }
    }

    public void RentEquipmentToUser()
    {

        var selectedUser = GetSelection(_repository.GetUsers(), "Select a user:");
        if (selectedUser == null)
        {
            Console.WriteLine("No user selected. Returning to the main menu");
            return;
        }
        
        var availableItems = _repository.GetEquipment()
            .Where(e => e.IsAvailable && !_repository.IsCurrentlyRented(e))
            .ToList();

        var selectedItem = GetSelection(availableItems, "Select a item:");
        if (selectedItem == null)
        {
            Console.WriteLine("No item selected. Returning to the main menu");
            return;
        }

        Console.WriteLine("Enter rental duration (days):");
        int days = int.Parse(Console.ReadLine());

        if (days < 1 || days > RentalPolicy.MaxRentalPeriod)
        {
            days = RentalPolicy.MaxRentalPeriod;
        }
        
        DateTime borrowedDate = DateTime.Now;
        DateTime dueDate = borrowedDate.AddDays(days);

        bool SuccessfulRent = _service.RentItem(selectedUser, selectedItem, borrowedDate, dueDate);

        if (SuccessfulRent)
        {
            Console.WriteLine("Rental registered!");
        }
        else
        {
            Console.WriteLine("Rental not registered! User has too many active rentals");
        }
        
    }

    public void ReturnEquipment()
    {
        var activeRentals = _repository.GetRentals().Where(r => r.ReturnDate == null).ToList();
        if (activeRentals.Count == 0)
        {
            Console.WriteLine("Nothing to return. Returning to the main menu");
            return;
        }
        
        var selectedRental = GetSelection(activeRentals, "Select a rental:");
        if (selectedRental == null)
        {
            Console.WriteLine("No rental selected. Returning to the main menu");
            return;
        }
        _service.ReturnItem(selectedRental, DateTime.Now);
        Console.WriteLine($"Rental returned. Fee: {selectedRental.Fee}");

    }

    public void ToggleAvailability()
    {
        var selectedItem =  GetSelection(_repository.GetEquipment(), "Select an item to toggle availability (damaged/fixed):");

        if (selectedItem == null)
        {
            Console.WriteLine("No item was selected. Returning to the main menu");
            return;
        }
        
        selectedItem.IsAvailable = !selectedItem.IsAvailable;
        Console.WriteLine($"Successfully changed availability of {selectedItem.Name} to {selectedItem.IsAvailable})");

    }

    public void DisplayActiveRentalsForUser()
    {
        
        var selectedUser = GetSelection(_repository.GetUsers(), "Select a user:");
        if (selectedUser == null)
        {
            Console.WriteLine("None of the users was selected. Returning to the main menu.");
            return;
        }
        
        var activeRentals = _repository.GetActiveRentalsForUser(selectedUser);

        if (activeRentals.Count == 0)
        {
            Console.WriteLine("No active rentals for this user");
            return;
        }

        Console.WriteLine($"Active rentals for the user {selectedUser.Name} {selectedUser.Surname}:");
        foreach (var activeRental in activeRentals)
        {
            Console.WriteLine($"{activeRental.BorrowedItem.Name} with due date: {activeRental.DueDate}");
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

        Console.WriteLine("Overdue rentals:");
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
        _run = false;
    }
}