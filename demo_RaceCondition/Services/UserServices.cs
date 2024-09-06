// Services/UserService.cs
using demo_RaceCondition.Models;
using System.Collections.Generic;
using System.Linq;

public class UserService
{
    private readonly List<User> _users = new();
    private User _currentUser;

    public UserService()
    {
        // Khởi tạo dữ liệu giả
        _users.Add(new User { Id = 1,Name="admin",Password="123", Email = "admin@example.com", IsEmailConfirmed = true, Role = "Admin" });
        _users.Add(new User { Id = 2, Name = "User", Password = "123", Email = "user@example.com", IsEmailConfirmed = false, Role = "User" });
    }

    public IEnumerable<User> GetAllUsers() => _users;

    public User GetUserById(int id) => _users.FirstOrDefault(u => u.Id == id);

    public void UpdateEmail(int id, string newEmail)
    {
        var user = GetUserById(id);
        if (user != null)
        {
            user.Email = newEmail;
            user.IsEmailConfirmed = false; 
        }
    }

    public void ConfirmEmail(int id)
    {
        var user = GetUserById(id);
        if (user != null)
        {
            user.IsEmailConfirmed = true;
        }
    }

    public void DeleteUser(int id)
    {
        var user = GetUserById(id);
        if (user != null)
        {
            _users.Remove(user);
        }
    }

    public User Login(string email, string password)
    {
        _currentUser = _users.FirstOrDefault(u => u.Email == email && u.Password==password); // Mật khẩu giả lập
        return _currentUser;
    }

    public User GetCurrentUser() => _currentUser;
}
