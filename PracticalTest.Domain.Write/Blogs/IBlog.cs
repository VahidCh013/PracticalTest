using CSharpFunctionalExtensions;
using PracticalTest.Domain.Write.Common;
using PracticalTest.Domain.Write.Users;
using PracticalTest.Domain.Write.ValueObjects;


namespace PracticalTest.Domain.Write.Blogs;


public interface IBlog
{
    public Name Name { get;}
    public string Description { get; }
    public  User User { get;  }
}