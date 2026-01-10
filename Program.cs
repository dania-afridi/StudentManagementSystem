using StudentManagementSystem.Services;

class Program
{
    static void Main()
    {
        // Test Mode(no database)
        //IDataService dataService = new FakeDataService();

        // Normal Mode(database)
        IDataService dataService = new StudentDbService();

        MenuService menu = new MenuService(dataService);
        menu.Start();
    }
}
