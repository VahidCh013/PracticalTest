using CSharpFunctionalExtensions;
using PracticalTest.Domain.Write.Blogs;
using PracticalTest.Domain.Write.Common;

namespace PracticalTest.Domain.Write.Users;

public class User:EntityBase
{

    public string UserName { get;  }
    public string Email { get;  }
    public string Password { get;  }
    public string Role { get;  }

    public virtual IList<Blog> Blogs { get;  }

    public User()
    {
        
    }
    private User(string userName, string email, string password, string role)
    {
        UserName = userName;
        Email = email;
        Password = password;
        Role = role;
        Blogs = new List<Blog>();
    }

    public static Result<User> Create(string userName, string email, string password, string role)
        => new User(userName, email, password, role);
}