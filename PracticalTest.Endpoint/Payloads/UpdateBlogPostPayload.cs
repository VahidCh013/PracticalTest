using PracticalTest.Domain.Write.Common;
using PracticalTest.Domain.Write.Common.Mediator;

namespace PracticalTest.Endpoint.Payloads;

public record UpdateBlogPostPayload( long? id,
    IEnumerable<UserError>? Errors = null) : Payload(Errors);