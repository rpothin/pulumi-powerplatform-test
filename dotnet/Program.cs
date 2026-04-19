using Pulumi;
using Pulumi.Powerplatform;
using Environment = Pulumi.Powerplatform.Environment;
using EnvironmentArgs = Pulumi.Powerplatform.EnvironmentArgs;

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
