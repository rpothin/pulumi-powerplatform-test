package main

import (
	pp "github.com/rpothin/pulumi-powerplatform/sdk/go/powerplatform"
	"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
)

func main() {
	pulumi.Run(func(ctx *pulumi.Context) error {
		env, err := pp.NewEnvironment(ctx, "test-env", &pp.EnvironmentArgs{
			DisplayName:     pulumi.String("SDK Test"),
			Location:        pulumi.String("unitedstates"),
			EnvironmentType: pulumi.String("Sandbox"),

			// New inputs — parity with Terraform provider
			// Allow Copilot / Bing Search features in this environment
			AllowBingSearch: pulumi.Bool(false),
			// Allow data to move across geographic regions (e.g. for AI features)
			AllowMovingDataAcrossRegions: pulumi.Bool(false),
			// Put the environment into admin-only mode (blocks regular users)
			AdministrationModeEnabled: pulumi.Bool(false),
			// Allow background operations to continue while administration mode is active
			BackgroundOperationEnabled: pulumi.Bool(false),

			// Inputs that require pre-existing resources — uncomment and supply values as needed:
			// Cadence:           pulumi.String("Moderate"),                          // "Frequent" or "Moderate" (immutable)
			// OwnerId:           pulumi.String("00000000-0000-0000-0000-000000000000"), // AAD user/group GUID
			// SecurityGroupId:   pulumi.String("00000000-0000-0000-0000-000000000000"), // AAD group restricting access
			// BillingPolicyId:   pulumi.String("00000000-0000-0000-0000-000000000000"), // Billing policy link
			// EnvironmentGroupId: pulumi.String("00000000-0000-0000-0000-000000000000"), // Environment group ID
			// AzureRegion:       pulumi.String("eastus"),                            // Specific Azure region (immutable)
			// Templates:         pulumi.StringArray{pulumi.String("D365_CDSSampleApp")}, // Provisioning templates (immutable)
			// TemplateMetadata:  pulumi.String(`{"PostProvisioningPackages": []}`),   // Template metadata JSON (immutable)
			// LinkedAppType:     pulumi.String("Canvas"),                             // "Canvas" or "ModelDriven"
			// LinkedAppId:       pulumi.String("00000000-0000-0000-0000-000000000000"), // Linked app GUID
		})
		if err != nil {
			return err
		}

		ctx.Export("envId", env.ID())

		// New computed outputs
		ctx.Export("envOrganizationId", env.OrganizationId)
		ctx.Export("envUniqueName", env.UniqueName)
		ctx.Export("envDataverseVersion", env.DataverseVersion)
		ctx.Export("envLinkedAppUrl", env.LinkedAppUrl)

		return nil
	})
}
