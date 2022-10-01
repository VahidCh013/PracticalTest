﻿using CSharpFunctionalExtensions;
using PracticalTest.Domain.Write.Common;
using PracticalTest.Domain.Write.Users;
using PracticalTest.Domain.Write.ValueObjects;


namespace PracticalTest.Domain.Write.Blogs;

public class Blog : AggregateEntity,ITimeAudit
{

    private readonly List<BlogPost> _blogPosts;
    public Name Name { get;}
    public string Description { get; }

    
    public virtual User User { get;  }


    protected Blog()
    {
        
    }
    private Blog(Name name, string description,User user)
    {
        Name = name;
        Description = description;
        _blogPosts = new List<BlogPost>();
        User = user;
    }

    private List<BlogPost> BlogPosts => _blogPosts;
    public DateTimeOffset CreatedOn { get; }
    public string CreatedBy { get; }
    public DateTimeOffset ModifiedOn { get; }
    public string ModifiedBy { get; }

    public static Result<Blog> Create(Name name,string description,User user)
    =>Result.FailureIf( string.IsNullOrEmpty( description ), description, "Description name must not be an empty string or whitespace" )
        .Map( dt => new Blog( name, description,user ) );
}