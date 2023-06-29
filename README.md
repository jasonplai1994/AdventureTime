# AdventureTime

Welcome to AdventureTime, a simple console-based game written in C# utilizing MySQL!

In Order to run this on your local machine you will need the following:
  - Text Editor (preferably Visual Studio)
  - MySQL Server and MySQL Workbench

After Obtaining the necessary software:
  - Open your machines System Environment Variables and inside System Variables save the connection string under the name localhost
      - Your connection string should look like this: Server=localhost;Database=adventuretime;user=root;password=password;
      - Note that MySQL user and password need to be set your user and password when you installed MySQL
  - Next, Open up MySQL Workbench and log into localhost, inside the schemas tab create a new schema called adventuretime
  - If you haven't already, git clone into a directory of your choice and open it in Visual  Studio or the corresponding Text Editor
  - Now, Open up the Migrations Folder and delete everything inside
  - Next, go to Tools -> NuGet Package Manager -> Package Manager Console
  - Inside the Package Manager Console, you need to run the following commands
      - Add-Migration InitialCreate
      - Update-Database
   
  - Finally, in Visual Studio, Build -> Build Solution, this should resolve dependencies

  * You should be able to run ConsoleApp1 and try the game out! *

Important Things To Note:
- SQL Server should be running on your machine
- Schema needs to be created as adventuretime
- Migration need to be added
