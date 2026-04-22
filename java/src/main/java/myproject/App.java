package myproject;

import com.pulumi.Pulumi;
import io.github.rpothin.powerplatform.Environment;
import io.github.rpothin.powerplatform.EnvironmentArgs;

public class App {
    public static void main(String[] args) {
        Pulumi.run(ctx -> {
            var env = new Environment("test-env", EnvironmentArgs.builder()
                .displayName("SDK Test")
                .location("unitedstates")
                .environmentType("Sandbox")
                // New inputs — parity with Terraform provider
                // Allow Copilot / Bing Search features in this environment
                .allowBingSearch(false)
                // Allow data to move across geographic regions (e.g. for AI features)
                .allowMovingDataAcrossRegions(false)
                // Put the environment into admin-only mode (blocks regular users)
                .administrationModeEnabled(false)
                // Allow background operations to continue while administration mode is active
                .backgroundOperationEnabled(false)
                // Inputs that require pre-existing resources — uncomment and supply values as needed:
                // .cadence("Moderate")                                    // "Frequent" or "Moderate" (immutable)
                // .ownerId("00000000-0000-0000-0000-000000000000")        // AAD user/group GUID
                // .securityGroupId("00000000-0000-0000-0000-000000000000") // AAD group restricting access
                // .billingPolicyId("00000000-0000-0000-0000-000000000000") // Billing policy link
                // .environmentGroupId("00000000-0000-0000-0000-000000000000") // Environment group ID
                // .azureRegion("eastus")                                  // Specific Azure region (immutable)
                // .templates(List.of("D365_CDSSampleApp"))                // Provisioning templates (immutable)
                // .templateMetadata("{\"PostProvisioningPackages\": []}")  // Template metadata JSON (immutable)
                // .linkedAppType("Canvas")                                // "Canvas" or "ModelDriven"
                // .linkedAppId("00000000-0000-0000-0000-000000000000")    // Linked app GUID
                .build());

            ctx.export("envId", env.id());
            // New computed outputs
            ctx.export("envOrganizationId", env.organizationId());
            ctx.export("envUniqueName", env.uniqueName());
            ctx.export("envDataverseVersion", env.dataverseVersion());
            ctx.export("envLinkedAppUrl", env.linkedAppUrl());
        });
    }
}
