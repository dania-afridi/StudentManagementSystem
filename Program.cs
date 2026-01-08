using StudentManagementSystem.Services;

class Program
{
    static void Main()
    {
        IDataService dataService = new StudentDbService();
        MenuService menu = new MenuService(dataService);
        menu.Start();
    }
}
