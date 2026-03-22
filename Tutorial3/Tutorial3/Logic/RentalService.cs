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
        bool allowedAnotherBorrowing = activeUserRentals <
                                       (user is Student
                                           ? RentalPolicy.MaxStudentRentals
                                           : RentalPolicy.MaxEmployeeRentals);
        bool notRented = !_repository.IsCurrentlyRented(item);
        bool notDamaged = item.IsAvailable;

        if (allowedAnotherBorrowing && notDamaged && notRented)
        {
            _repository.AddRental(new RentalAct(user,  item, borrowDate, dueDate));
            return true;
        }
        return false;
    }

    public void ReturnItem(RentalAct rental, DateTime returnDate)
    {
        rental.ReturnDate = returnDate;
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