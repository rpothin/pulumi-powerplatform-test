using Pulumi;
using Pulumi.Powerplatform;
using Pulumi.Powerplatform.Inputs;
using Environment = Pulumi.Powerplatform.Environment;
using EnvironmentArgs = Pulumi.Powerplatform.EnvironmentArgs;

return await Deployment.RunAsync(() =>
{
    var env = new Environment("test-env", new EnvironmentArgs
    {
        DisplayName = "SDK Test",
        Location = "unitedstates",
        EnvironmentType = "Sandbox",
        // Allow Copilot / Bing Search features in this environment (top-level)
        AllowBingSearch = false,
        // Allow data to move across geographic regions, e.g. for AI features (top-level)
        AllowMovingDataAcrossRegions = false,

        // Dataverse block — include this to provision a Dataverse database.
        // Omit the block entirely to create an environment without Dataverse.
        Dataverse = new EnvironmentDataverseArgs
        {
            // CurrencyCode and LanguageCode are required when Dataverse is specified
            CurrencyCode = "USD",
            LanguageCode = 1033, // 1033 = English
            // Put the environment into admin-only mode (blocks regular users)
            AdministrationModeEnabled = false,
            // Allow background operations to continue while administration mode is active
            BackgroundOperationEnabled = true,
            // Optional Dataverse fields — uncomment and supply values as needed:
            // DomainName = "my-env",                                    // Custom domain prefix
            // SecurityGroupId = "00000000-0000-0000-0000-000000000000", // AAD group restricting access
            // Templates = new[] { "D365_CDSSampleApp" },                // Provisioning templates (immutable)
            // TemplateMetadata = "{\"PostProvisioningPackages\": []}",   // Template metadata JSON (immutable)
        },

        // Other optional top-level inputs — uncomment and supply values as needed:
        // Cadence = "Moderate",                                          // "Frequent" or "Moderate" (immutable)
        // OwnerId = "00000000-0000-0000-0000-000000000000",             // AAD user/group GUID
        // BillingPolicyId = "00000000-0000-0000-0000-000000000000",     // Billing policy link
        // EnvironmentGroupId = "00000000-0000-0000-0000-000000000000",  // Environment group ID
        // AzureRegion = "eastus",                                        // Specific Azure region (immutable)
        // LinkedAppType = "Canvas",                                      // "Canvas" or "ModelDriven"
        // LinkedAppId = "00000000-0000-0000-0000-000000000000",         // Linked app GUID
    });

    return new Dictionary<string, object?>
    {
        ["envId"] = env.Id,
        ["envDisplayName"] = env.DisplayName,
        ["envState"] = env.State,
        ["envType"] = env.EnvironmentType,
        ["envLocation"] = env.Location,
        // Computed outputs from the Dataverse block
        ["envDataverseUrl"] = env.Dataverse.Apply(d => d?.Url ?? ""),
        ["envDataverseOrganizationId"] = env.Dataverse.Apply(d => d?.OrganizationId ?? ""),
        ["envDataverseUniqueName"] = env.Dataverse.Apply(d => d?.UniqueName ?? ""),
        ["envDataverseVersion"] = env.Dataverse.Apply(d => d?.Version ?? ""),
    };
});
