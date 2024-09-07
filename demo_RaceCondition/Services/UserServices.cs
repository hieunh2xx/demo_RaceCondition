// Services/UserService.cs
using demo_RaceCondition.Models;

public class UserService
{
    private demo_bypassContext _bypassContext { get; set; }

    public UserService(demo_bypassContext bypassContext)
    {
        _bypassContext = bypassContext;
    }


    public void UpdateEmail(int id, string newEmail)
    {
        var user = _bypassContext.Acccounts.FirstOrDefault(a => a.Id == id);
        if (user != null)
        {
            user.Email = newEmail;
            user.IsComfirmEmail = false;
        }
    }

    public void ConfirmEmail(int id)
    {
        var user = GetUserById(id);
        if (user != null)
        {
            user.IsComfirmEmail = true;
        }
    }

    private Acccount? GetUserById(int id)
    {
        var user = _bypassContext.Acccounts.FirstOrDefault(a => a.Id == id);
        return user;
    }

    public void DeleteUser(int id)
    {
        var user = GetUserById(id);
        if (user != null)
        {
            _bypassContext.Acccounts.Remove(user);
        }
    }

    public Acccount? Login(string? email, string? password)
    {
        var user = _bypassContext.Acccounts.FirstOrDefault(u => u.Email == email && u.Password == password);
        if (user != null)
            user.Password = null;
        return user;
    }
}
