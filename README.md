# Demonstration
### Demonstration: Adding Equipment
Adding different types of equipment dynamically changes the required input fields.

```text
Welcome to the equipment rental service!
======
Available commands:
1. Add a new user to the system.
2. Add a new equipment item of a selected type.
3. Display the full list of equipment together with current status.
4. Display only equipment currently available for rental.
5. Rent equipment to a user.
6. Return equipment and calculate a possible late penalty.
7. Mark equipment as unavailable, for example because of damage or maintenance.
8. Display active rentals for a selected user.
9. Display the list of overdue rentals.
10. Generate a short summary report of the rental service state.
11. Exit
======
> 2
Select equipment type:
Enter 1 for "Laptop", 2 for "Camera", 3 for "Projector"
> 1
Enter equipment name
> Dell MegaLaptop
Enter resolution with a space (e.g. 1920 1080)
> 1920 1080
Enter the processor name
> Inter Core i7
Item added successfully!
======
Available commands:
1. Add a new user to the system.
2. Add a new equipment item of a selected type.
3. Display the full list of equipment together with current status.
4. Display only equipment currently available for rental.
5. Rent equipment to a user.
6. Return equipment and calculate a possible late penalty.
7. Mark equipment as unavailable, for example because of damage or maintenance.
8. Display active rentals for a selected user.
9. Display the list of overdue rentals.
10. Generate a short summary report of the rental service state.
11. Exit
======
> 2
Select equipment type:
Enter 1 for "Laptop", 2 for "Camera", 3 for "Projector"
> 2
Enter equipment name
> Nikon ABC
Enter focal length (number)
> 24
Does it has bluetooth? 1: yes, 2: no 
> 1
Item added successfully!
```
As you can see, they now appear on the list (select option 3 in the menu)

<pre>
> 3
All equipment:
Item name: Asus Vivobook 14
Item type: Laptop
Item physical condition: Good condition
Rental status: Ready for rent
=======
...
<b>Item name: Dell MegaLaptop
Item type: Laptop
Item physical condition: Good condition
Rental status: Ready for rent
=======
Item name: Nikon ABC
Item type: Camera
Item physical condition: Good condition
Rental status: Ready for rent</b>
=======
</pre>

### Demonstration: Adding Users
Demonstrating the addition of different user types (Student and Employee) into the system.

```text
Welcome to the equipment rental service!
======
Available commands:
1. Add a new user to the system.
[... options 2-10 ...]
11. Exit
======
> 1

Select user type:
Enter 1 for "Student", 2 for "Employee"
> 1
Enter the user first name
> Pavel
Enter the user last name
> Abramau
Enter the user email address
> example@gmail.com
User added successfully!

======
[... Main Menu Reappears ...]
======
> 1

Select user type:
Enter 1 for "Student", 2 for "Employee"
> 2
Enter the user first name
> Alex
Enter the user last name
> Black
Enter the user email address
> ablack@gmail.com
User added successfully!

```

### Demonstration: Renting Equipment and Viewing Active Rentals
Demonstrating a successful rental operation and subsequently verifying the user's updated active rental list.

```text
Welcome to the equipment rental service!
======
Available commands:
1. Add a new user to the system.
[... options 2-10 ...]
11. Exit
======
> 5

Select a user:
1. Nikita Pohrebniak (Student)
2. Fiodar Klimovich (Student)
3. John Brown (Employee)
4. Keanu Reeves (Employee)
5. Pavel Abramau (Student)
6. Alex Black (Employee)
Enter selection number: 
> 4

Select a item:
1. Asus Vivobook 14 (Laptop)
2. HP OmniBook 5 (Laptop)
3. Nikon Coolpix (Camera)
4. Sony A7 (Camera)
5. Samsung Freestyle 2nd gen (Projector)
6. Philips NeoPix 100 (Projector)
7. Dell MegaLaptop (Laptop)
8. Nikon ABC (Camera)
Enter selection number: 
> 7

Enter rental duration (days):
> 20
Rental registered!

======
[... Main Menu Reappears ...]
======
> 8

Select a user:
1. Nikita Pohrebniak (Student)
2. Fiodar Klimovich (Student)
3. John Brown (Employee)
4. Keanu Reeves (Employee)
5. Pavel Abramau (Student)
6. Alex Black (Employee)
Enter selection number: 
> 4

Active rentals for the user Keanu Reeves:
Dell MegaLaptop with due date: 4/11/2026 6:45:45 PM
```

### Demonstration: Attempted Invalid Rental (User Limit Exceeded)
Demonstrating business rule enforcement. The system checks if a Student has reached their maximum limit (2 items) and successfully blocks a 3rd rental attempt.

```text
Welcome to the equipment rental service!
======
Available commands:
1. Add a new user to the system.
[... options 2-10 ...]
11. Exit
======
> 8

Select a user:
1. Nikita Pohrebniak (Student)
2. Fiodar Klimovich (Student)
3. John Brown (Employee)
4. Keanu Reeves (Employee)
5. Pavel Abramau (Student)
6. Alex Black (Employee)
Enter selection number: 
> 2

Active rentals for the user Fiodar Klimovich:
Asus Vivobook 14 with due date: 3/27/2026 6:48:54 PM
Samsung Freestyle 2nd gen with due date: 3/28/2026 6:49:06 PM

======
[... Main Menu Reappears ...]
======
> 5

Select a user:
1. Nikita Pohrebniak (Student)
2. Fiodar Klimovich (Student)
3. John Brown (Employee)
4. Keanu Reeves (Employee)
5. Pavel Abramau (Student)
6. Alex Black (Employee)
Enter selection number: 
> 2

Select a item:
1. HP OmniBook 5 (Laptop)
2. Nikon Coolpix (Camera)
3. Sony A7 (Camera)
4. Philips NeoPix 100 (Projector)
5. Nikon ABC (Camera)
Enter selection number: 
> 1

Enter rental duration (days):
> 10
Rental not registered! User has too many active rentals
```

### Demonstration: Returning Equipment On Time + Rental is no longer considered active
Demonstrating a successful return operation completed before the due date (resulting in a 0 penalty fee) and verifying the rental is cleared from the user's active list.

```text
Welcome to the equipment rental service!
======
Available commands:
1. Add a new user to the system.
[... options 2-10 ...]
11. Exit
======
> 6

Select a rental:
1. Dell MegaLaptop (Rented by Keanu Reeves)
2. Asus Vivobook 14 (Rented by Fiodar Klimovich)
3. Samsung Freestyle 2nd gen (Rented by Fiodar Klimovich)
Enter selection number: 
> 1
Rental returned. Fee: 0

======
[... Main Menu Reappears ...]
======
> 8

Select a user:
1. Nikita Pohrebniak (Student)
2. Fiodar Klimovich (Student)
3. John Brown (Employee)
4. Keanu Reeves (Employee)
5. Pavel Abramau (Student)
6. Alex Black (Employee)
Enter selection number: 
> 4

No active rentals for this user
```

### Demonstration: Delayed Return and Penalty Calculation
Demonstrating the identification of an overdue rental and the subsequent return operation that successfully calculates and applies a late penalty fee.

```text
Welcome to the equipment rental service!
======
Available commands:
1. Add a new user to the system.
[... options 2-10 ...]
11. Exit
======
> 9

Overdue rentals:
Philips Ultra 200 James May 3/20/2026 7:01:20 PM (late for 2 days)

======
[... Main Menu Reappears ...]
======
> 6

Select a rental:
1. Philips Ultra 200 (Rented by James May)
Enter selection number: 
> 1

Rental returned. Fee: 6
```

### Demonstration: System State Report
Demonstrating the generation of a high-level summary report of the rental service state. *(Note: This snippet is from a separate application run using the `Seeder`, so the total counts here reflect the initial seeded data rather than the exact sequence of items added in the previous manual examples.)*

```text
Welcome to the equipment rental service!
======
Available commands:
1. Add a new user to the system.
[... options 2-10 ...]
11. Exit
======
> 10

System report:
Total users: 4
Total items: 6
Total rentals: 3
Available items: 6
Active rentals: 2
```

# How To Run The Application
In terminal, while being in the project root directory, run the command 
```
dotnet run
```
and enjoy!

# Design decisions

1. Class Responsibilities

I split the project into a few clear layers so that each class has only one main job:

  - Models (User, Equipment, RentalAct) are designed to simply model and store data. None of those conducts any calculations
  - RentalUI: This class is completely in charge of the console text and console UI. It prints the menus and reads the user's input, but it doesn't make any business decisions.
  - RentalService: This handles the actual business rules. If we need to check if a student has too many active rentals, or calculate a penalty fee for a late return, it happens here.
  - RentalPolicy: defines such constants as maximum rentals for each user type, etc.
  - RentalRepository: used for storing and managing actual users, equipment inventory, rentals
  - Seeder: used for repository prepopulation

2. Reducing Coupling

I wanted to make sure the classes weren't too tightly connected. For example, RentalUI doesn't create its own data lists or its own service. Instead, I create the RentalRepository and RentalService first, and then pass them into the UI's constructor. This means the UI just uses whatever service it is given, making the code much easier to change or test later without breaking the menu system.

3. Cohesion

While building the UI, I noticed I was writing the same for loops every time I needed the user to pick an item, a user, or a rental from a list. To keep related code together (high cohesion) and avoid repeating myself, I created a generic helper method called GetSelection<T>. Now, all the logic for printing a numbered list and safely reading the user's choice lives in one single place.

4. Separating Physical Status from Rental Status

One of my main domain design choices was how to handle whether an item is "Available". I realized that an item being physically broken is different from an item being actively rented.
Because of this, the Equipment.IsAvailable boolean is only used for damage/broken item. To figure out if an item can actually be rented right now, the system checks if it is physically okay AND checks the repository to make sure nobody else currently holds it (!_repository.IsCurrentlyRented(e)). This prevents bugs where we might return an item but forget to flip a status boolean back to true.
  

