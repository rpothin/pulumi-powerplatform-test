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

        // New inputs — parity with Terraform provider
        // Allow Copilot / Bing Search features in this environment
        AllowBingSearch = false,
        // Allow data to move across geographic regions (e.g. for AI features)
        AllowMovingDataAcrossRegions = false,
        // Put the environment into admin-only mode (blocks regular users)
        AdministrationModeEnabled = false,
        // Allow background operations to continue while administration mode is active
        BackgroundOperationEnabled = false,

        // Inputs that require pre-existing resources — uncomment and supply values as needed:
        // Cadence = "Moderate",                                  // "Frequent" or "Moderate" (immutable)
        // OwnerId = "00000000-0000-0000-0000-000000000000",      // AAD user/group GUID
        // SecurityGroupId = "00000000-0000-0000-0000-000000000000", // AAD group restricting access
        // BillingPolicyId = "00000000-0000-0000-0000-000000000000", // Billing policy link
        // EnvironmentGroupId = "00000000-0000-0000-0000-000000000000", // Environment group ID
        // AzureRegion = "eastus",                                // Specific Azure region (immutable)
        // Templates = new[] { "D365_CDSSampleApp" },             // Provisioning templates (immutable)
        // TemplateMetadata = "{\"PostProvisioningPackages\": []}", // Template metadata JSON (immutable)
        // LinkedAppType = "Canvas",                              // "Canvas" or "ModelDriven"
        // LinkedAppId = "00000000-0000-0000-0000-000000000000",  // Linked app GUID
    });

    return new Dictionary<string, object?>
    {
        ["envId"] = env.Id,
        // New computed outputs
        ["envOrganizationId"] = env.OrganizationId,
        ["envUniqueName"] = env.UniqueName,
        ["envDataverseVersion"] = env.DataverseVersion,
        ["envLinkedAppUrl"] = env.LinkedAppUrl,
    };
});
