using Tutorial3.Models.Equipment;
using Tutorial3.Models.Users;

namespace Tutorial3.Logic;

public class RentalRepository
{
    private List<User> _users = new();
    private List<Equipment> _equipment = new();
    private List<RentalAct> _rentals = new();

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public void AddEquipment(Equipment equipment)
    {
        _equipment.Add(equipment);
    }

    public void AddRental(RentalAct rental)
    {
        _rentals.Add(rental);
    }

    public List<User> GetUsers() => _users;
    
    public List<Equipment> GetEquipment() => _equipment;

    public List<RentalAct> GetRentals() => _rentals;

    public List<RentalAct> GetOverdueRentals() {
        return _rentals
            .Where(r => r.ReturnDate == null && r.DueDate < DateTime.Now)
            .ToList();
    }

    public List<RentalAct> GetActiveRentalsForUser(User user)
    {
        return _rentals
            .Where(r => r.Borrower == user && r.ReturnDate == null)
            .ToList();
    }
    
    public int UserCount => _users.Count;
    public int EquipmentCount => _equipment.Count;
    public int RentalCount => _rentals.Count;
    public int AvailableEquipmentCount => _equipment.Count(e => e.IsAvailable);
    public int ActiveRentalsCount => _rentals.Count(r => r.ReturnDate == null);
}