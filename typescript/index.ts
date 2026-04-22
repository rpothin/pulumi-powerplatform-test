import * as pp from "@rpothin/powerplatform";

const env = new pp.Environment("test-env", {
    displayName: "SDK Test",
    location: "unitedstates",
    environmentType: "Sandbox",
    // Allow Copilot / Bing Search features in this environment (top-level)
    allowBingSearch: false,
    // Allow data to move across geographic regions, e.g. for AI features (top-level)
    allowMovingDataAcrossRegions: false,

    // Dataverse block — include this to provision a Dataverse database.
    // Omit the block entirely to create an environment without Dataverse.
    dataverse: {
        // currencyCode and languageCode are required when dataverse is specified
        currencyCode: "USD",
        languageCode: 1033, // 1033 = English
        // Put the environment into admin-only mode (blocks regular users)
        administrationModeEnabled: false,
        // Allow background operations to continue while administration mode is active
        backgroundOperationEnabled: true,
        // Optional Dataverse fields — uncomment and supply values as needed:
        // domainName: "my-env",                                   // Custom domain prefix
        // securityGroupId: "00000000-0000-0000-0000-000000000000", // AAD group restricting access
        // templates: ["D365_CDSSampleApp"],                        // Provisioning templates (immutable)
        // templateMetadata: '{"PostProvisioningPackages": []}',    // Template metadata JSON (immutable)
    },

    // Other optional top-level inputs — uncomment and supply values as needed:
    // cadence: "Moderate",                                          // "Frequent" or "Moderate" (immutable)
    // ownerId: "00000000-0000-0000-0000-000000000000",             // AAD user/group GUID owning this environment
    // billingPolicyId: "00000000-0000-0000-0000-000000000000",     // Billing policy link
    // environmentGroupId: "00000000-0000-0000-0000-000000000000",  // Environment group ID
    // azureRegion: "eastus",                                        // Specific Azure region (immutable)
    // linkedAppType: "Canvas",                                      // "Canvas" or "ModelDriven"
    // linkedAppId: "00000000-0000-0000-0000-000000000000",         // Linked app GUID
    // enterprisePolicies: [],                                       // List of EnterprisePolicy objects
});

export const envId = env.id;
export const envState = env.state;
export const envType = env.environmentType;
export const envLocation = env.location;

// Computed outputs from the Dataverse block
export const envDataverseUrl = env.dataverse.apply(d => d?.url);
export const envOrganizationId = env.dataverse.apply(d => d?.organizationId);
export const envUniqueName = env.dataverse.apply(d => d?.uniqueName);
export const envDataverseVersion = env.dataverse.apply(d => d?.version);
