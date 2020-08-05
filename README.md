# About the Project

Taekwondo instructor needs a database to keep track his hundreds of students. So in the project we used the ASP.NET MVC Pattern to design the requirements.

# Technologies Used

- [ASP.NET](https://dotnet.microsoft.com/apps/aspnet/mvc)
- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-2019)

# Requirements

- There is only one instructor who teaches all classes and he is the only user
- Student data includes: <b>name, date of birth, the date they joined the school, mobile number, email, and address</b>.
- School wants to track also the students’ parents. We need to store also parents’ mobile phone and email address. Parents may also be students.
- Students should not be deleted from the system but they should be either <b>Active<b> or </b>Inactive</b> (for those who drop out).
- Instructor wants to keep track of his inventory, such as, belts, uniforms, weapons, etc. These should include the <b>product name, product description, cost price and selling price</b>.
- Students pay fees for membership, tests, purchasing products, etc. The system must be able to keep track of all money received from students. The system should be able to show this information for any given date, e.g. by month or within a range
- Class is offered at specific times and days of the week. For example, one class might be on Mondays and Thursdays from 5:00-6:00 pm is an intermediate-level class. Another class might be on Mondays, Wednesdays and Fridays from 6:00-7:00 pm is a beginner-level class, etc.
- Students may attend any class at any level during each week so there is no expectation that any particular student will attend any particular class session. Therefore, the actual attendance of students at each individual class meeting must be tracked.
- Each student holds a rank in the martial arts. The belt color and the date the student awarded each rank needs to be kept in the database. 
- A given rank may be held by many students. While it is customary to think of a student as having a single rank, it is necessary to track each student’s progress through the ranks. Therefore, every rank that a student attains is kept in the system. New students joining the school are automatically given a <b>white</b> belt rank (default).
- Sample ranks (the user can enter any ranks in the Ranks table) are:
  - White
  - Yellow
  - Green
  - Blue
  - Red
  - Black

#UML Class Diagram

![UML](/UML.png)
