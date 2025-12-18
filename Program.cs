using StudentManagementSystem.Services;

class Program
{
    static void Main()
    {
        MenuService menuService = new MenuService();
        menuService.Start();
    }
}
