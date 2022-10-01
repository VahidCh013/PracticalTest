// PartnerManagement - PM.Api.SmartRepair.Test.Helper - MediatorHelper.cs
// created on 2022/05/10

using MediatR;
using Moq;

namespace PracticalTest.Write.TestHelpers
{
  public class MediatorHelper:IMediator
  {
    protected MediatorHelper()
    {
      
    }
    private readonly IMediator _mediator;

    public MediatorHelper(IMediator mediator)
    {
      _mediator = mediator;
    }

    public async Task<TResponse> Send<TResponse>( IRequest<TResponse> request, CancellationToken cancellationToken = new CancellationToken() ) => await _mediator.Send(request, cancellationToken);

    public async Task<object?> Send( object request, CancellationToken cancellationToken = new CancellationToken() )
    {
      switch (request)
      {
        case IRequest command:
          await _mediator.Send(command, cancellationToken);
          break;
        default:
          throw new InvalidOperationException($"{nameof(request)} should implement {nameof(IRequest)}");
      }

      return Unit.Value;
    }

    public async Task Publish( object notification, CancellationToken cancellationToken = new CancellationToken() ) =>await _mediator.Publish(notification, cancellationToken);

    public async Task Publish<TNotification>( TNotification notification, CancellationToken cancellationToken = new CancellationToken() ) where TNotification : INotification => await _mediator.Publish(notification, cancellationToken);
    
    public static IMediator Initialize()
    {
      var       mock     = new Mock<MediatorHelper>(MockBehavior.Loose, new Mock<MediatorHelper>(MockBehavior.Loose).Object);
      IMediator mediator = mock.Object;
      return mediator;
    }
  }
}