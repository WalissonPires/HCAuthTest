using HotChocolate.Resolvers;
using Microsoft.AspNetCore.Authorization;

namespace HCAuthTest
{
    public class AssinaturaValidaHandler : AuthorizationHandler<AssinaturaValidaRequiment, IResolverContext>
    {
        public override Task HandleAsync(AuthorizationHandlerContext context)
        {
            var bypassForTeste = false;
            if (bypassForTeste)
            {
                context.Requirements.ToList().ForEach(requirement => context.Succeed(requirement));
                return Task.CompletedTask;
            }

            return base.HandleAsync(context);
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AssinaturaValidaRequiment requirement, IResolverContext resolverContext)
        {
            if (CheckAssinaturaValida(resolverContext))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }

        private bool CheckAssinaturaValida(IResolverContext resolverContext)
        {
            // complex logic here

            return true;
        }
    }
}
