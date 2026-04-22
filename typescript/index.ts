import * as pp from "@rpothin/powerplatform";

const env = new pp.Environment("test-env", {
    displayName: "SDK Test",
    location: "unitedstates",
    environmentType: "Sandbox",

    // New inputs — parity with Terraform provider
    // Allow Copilot / Bing Search features in this environment
    allowBingSearch: false,
    // Allow data to move across geographic regions (e.g. for AI features)
    allowMovingDataAcrossRegions: false,
    // Put the environment into admin-only mode (blocks regular users)
    administrationModeEnabled: false,
    // Allow background operations to continue while administration mode is active
    backgroundOperationEnabled: false,

    // Inputs that require pre-existing resources — uncomment and supply values as needed:
    // cadence: "Moderate",                              // Release cadence: "Frequent" or "Moderate" (immutable)
    // ownerId: "00000000-0000-0000-0000-000000000000", // AAD user/group GUID owning this environment
    // securityGroupId: "00000000-0000-0000-0000-000000000000", // AAD group restricting access
    // billingPolicyId: "00000000-0000-0000-0000-000000000000", // Billing policy link
    // environmentGroupId: "00000000-0000-0000-0000-000000000000", // Environment group ID
    // azureRegion: "eastus",                            // Specific Azure region (immutable)
    // templates: ["D365_CDSSampleApp"],                 // Provisioning templates (immutable)
    // templateMetadata: '{"PostProvisioningPackages": []}', // Template metadata JSON (immutable)
    // linkedAppType: "Canvas",                          // "Canvas" or "ModelDriven"
    // linkedAppId: "00000000-0000-0000-0000-000000000000", // Linked app GUID
    // enterprisePolicies: [],                           // List of EnterprisePolicy objects
});

export const envId = env.id;

// New computed outputs
export const envOrganizationId = env.organizationId;
export const envUniqueName = env.uniqueName;
export const envDataverseVersion = env.dataverseVersion;
export const envLinkedAppUrl = env.linkedAppUrl;
