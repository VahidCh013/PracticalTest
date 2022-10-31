using PracticalTest.Domain.Write.Common;
using PracticalTest.Domain.Write.Common.Mediator;

namespace PracticalTest.Infrastructure.Blogs.Payloads;

public record AddBlogPayload( long? id,
    IEnumerable<UserError>? Errors = null) : Payload(Errors);