using Pulumi;
using Pulumi.Powerplatform;

return await Deployment.RunAsync(() =>
{
    var env = new Environment("test-env", new EnvironmentArgs
    {
        DisplayName = "SDK Test",
        Location = "unitedstates",
        EnvironmentType = "Sandbox",
    });

    return new Dictionary<string, object?> { ["envId"] = env.Id };
});
