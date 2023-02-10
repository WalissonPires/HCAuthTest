# Hot Chocolate sample project

The project tries to perform authorization validation based on the IResolverContext as shown in the documentation at: [IResolverContext within an AuthorizationHandler](https://chillicream.com/docs/hotchocolate/v13/security/authorization/#iresolvercontext-within-an-authorizationhandler).
But it doesn't work as expected.

# Run the project

- Run the project with `dotnet run`.
- Go to the swagger page at `https://localhost:7266/swagger/index.html`
- Log in to the `POST /auth` route with any username and password.
- Copy the token
- Access the graphql UI at `https://localhost:7266/graphql/`
- Configure the token
- Make a graphql query
``` graphql
query {
     book {
       title
     }
}
```

It was expected that the `HandleRequirementAsync` method in the `AssinaturaValidaHandler` class was called and the validation was done.

But the method is not called. Only `HandleAsync` is called.

Just for testing you can set the `bypassForTeste` variable to true and see that the query will be authorized.
```diff
public override Task HandleAsync(AuthorizationHandlerContext context)
{
-    var bypassForTeste = false;
+    var bypassForTeste = true;
    if (bypassForTest)
    {
        context.Requirements.ToList().ForEach(requirement => context.Succeed(requirement));
        return Task.CompletedTask;
    }

    return base.HandleAsync(context);
}
```

But this method is not what I need for my logic. I need to access the `IResolverContext`.