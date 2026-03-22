using Tutorial3.Models.Equipment;
using Tutorial3.Models.Users;

namespace Tutorial3.Logic;

public class RentalService
{
    private RentalRepository _repository;

    public RentalService(RentalRepository repository)
    {
        _repository = repository;
    }
    
    
    public bool RentItem(User user, Equipment item, DateTime borrowDate, DateTime dueDate)
    {
        int activeUserRentals = _repository.GetActiveRentalsForUser(user).Count;
        if (activeUserRentals < (user is Student ? RentalPolicy.MaxStudentRentals : RentalPolicy.MaxEmployeeRentals)
            && item.IsAvailable)
        {
            _repository.AddRental(new RentalAct(user,  item, borrowDate, dueDate));
            item.IsAvailable = false;
            return true;
        }
        return false;
    }

    public void ReturnItem(RentalAct rental, DateTime returnDate)
    {
        rental.ReturnDate = returnDate;
        rental.BorrowedItem.IsAvailable = true;
        if (returnDate.Date > rental.DueDate.Date)
        {
            TimeSpan difference = returnDate.Date - rental.DueDate.Date;
            rental.Fee = (RentalPolicy.LateFeeDaily) *  difference.Days;
        }
        else
        {
            rental.Fee = 0;
        }
    }
}