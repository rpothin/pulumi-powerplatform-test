package myproject;

import com.pulumi.Pulumi;
import io.github.rpothin.powerplatform.Environment;
import io.github.rpothin.powerplatform.EnvironmentArgs;
import io.github.rpothin.powerplatform.inputs.DataverseArgs;
import io.github.rpothin.powerplatform.outputs.Dataverse;

public class App {
    public static void main(String[] args) {
        Pulumi.run(ctx -> {
            var env = new Environment("test-env", EnvironmentArgs.builder()
                .displayName("SDK Test")
                .location("unitedstates")
                .environmentType("Sandbox")
                // Allow Copilot / Bing Search features in this environment (top-level)
                .allowBingSearch(false)
                // Allow data to move across geographic regions, e.g. for AI features (top-level)
                .allowMovingDataAcrossRegions(false)
                // Dataverse block — include this to provision a Dataverse database.
                // Omit the block entirely to create an environment without Dataverse.
                .dataverse(DataverseArgs.builder()
                    // currencyCode and languageCode are required when dataverse is specified
                    .currencyCode("USD")
                    .languageCode(1033.0) // 1033 = English
                    // Put the environment into admin-only mode (blocks regular users)
                    .administrationModeEnabled(false)
                    // Allow background operations to continue while administration mode is active
                    .backgroundOperationEnabled(true)
                    // Optional Dataverse fields — uncomment and supply values as needed:
                    // .domainName("my-env")                                    // Custom domain prefix
                    // .securityGroupId("00000000-0000-0000-0000-000000000000") // AAD group restricting access
                    // .templates(List.of("D365_CDSSampleApp"))                 // Provisioning templates (immutable)
                    // .templateMetadata("{\"PostProvisioningPackages\": []}")   // Template metadata JSON (immutable)
                    .build())
                // Other optional top-level inputs — uncomment and supply values as needed:
                // .cadence("Moderate")                                          // "Frequent" or "Moderate" (immutable)
                // .ownerId("00000000-0000-0000-0000-000000000000")             // AAD user/group GUID
                // .billingPolicyId("00000000-0000-0000-0000-000000000000")     // Billing policy link
                // .environmentGroupId("00000000-0000-0000-0000-000000000000")  // Environment group ID
                // .azureRegion("eastus")                                        // Specific Azure region (immutable)
                // .linkedAppType("Canvas")                                      // "Canvas" or "ModelDriven"
                // .linkedAppId("00000000-0000-0000-0000-000000000000")         // Linked app GUID
                .build());

            ctx.export("envId", env.id());
            ctx.export("envDisplayName", env.displayName());
            ctx.export("envState", env.state());
            ctx.export("envType", env.environmentType());
            ctx.export("envLocation", env.location());
            // Computed outputs from the Dataverse block
            ctx.export("envDataverseUrl", env.dataverse().applyValue(d -> d.flatMap(v -> v.url()).orElse("")));
            ctx.export("envDataverseOrganizationId", env.dataverse().applyValue(d -> d.flatMap(v -> v.organizationId()).orElse("")));
            ctx.export("envDataverseUniqueName", env.dataverse().applyValue(d -> d.flatMap(v -> v.uniqueName()).orElse("")));
            ctx.export("envDataverseVersion", env.dataverse().applyValue(d -> d.flatMap(v -> v.version()).orElse("")));
        });
    }
}
