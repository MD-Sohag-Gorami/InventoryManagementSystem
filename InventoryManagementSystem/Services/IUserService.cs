namespace InventoryManagementSystem.Services
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenicated();
    }
}