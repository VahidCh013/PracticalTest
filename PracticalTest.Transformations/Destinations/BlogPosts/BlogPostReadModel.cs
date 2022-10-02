﻿namespace PracticalTest.Transformations.Destinations.BlogPosts;

public class BlogPostReadModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Desciption { get; set; }
    public string UserEmail { get; set; }
    public string Content { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public string Tags { get; set; }
}