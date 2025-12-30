using StudentManagementSystem.Services;

class Program
{
    static void Main()
    {
        MenuService menuService = new MenuService();
        menuService.Start();

       // StudentDbService db = new StudentDbService();
       // db.RunSqlTests();
    }
}
