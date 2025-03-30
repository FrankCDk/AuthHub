using MediatR;

namespace AuthHub.Infrastructure.Dependency.Behaviours
{
    public class ExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
			try
			{
				return await next();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
        }
    }
}
