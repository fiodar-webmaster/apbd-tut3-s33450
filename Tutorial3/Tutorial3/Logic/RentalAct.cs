using Tutorial3.Models.Equipment;
using Tutorial3.Models.Users;
namespace Tutorial3.Logic;

public class RentalAct
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public User Borrower { get; }
    public Equipment BorrowedItem{ get; }
    public DateTime BorrowDate { get; }
    public DateTime DueDate { get; }
    
    public DateTime? ReturnDate { get; set; }
    public decimal? Fee { get; set; } 
    
    public RentalAct(User borrower, Equipment borrowedItem, DateTime borrowDate, DateTime dueDate)
    {
        Borrower = borrower;
        BorrowedItem = borrowedItem;
        BorrowDate = borrowDate;
        DueDate = dueDate;
    }


}