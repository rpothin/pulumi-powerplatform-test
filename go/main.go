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
			// Allow Copilot / Bing Search features in this environment (top-level)
			AllowBingSearch: pulumi.Bool(false),
			// Allow data to move across geographic regions, e.g. for AI features (top-level)
			AllowMovingDataAcrossRegions: pulumi.Bool(false),

			// Dataverse block — include this to provision a Dataverse database.
			// Omit the block entirely to create an environment without Dataverse.
			Dataverse: &pp.DataverseArgs{
				// CurrencyCode and LanguageCode are required when Dataverse is specified
				CurrencyCode: pulumi.String("USD"),
				LanguageCode: pulumi.Float64(1033), // 1033 = English
				// Put the environment into admin-only mode (blocks regular users)
				AdministrationModeEnabled: pulumi.Bool(false),
				// Allow background operations to continue while administration mode is active
				BackgroundOperationEnabled: pulumi.Bool(true),
				// Optional Dataverse fields — uncomment and supply values as needed:
				// DomainName:      pulumi.String("my-env"),                                   // Custom domain prefix
				// SecurityGroupId: pulumi.String("00000000-0000-0000-0000-000000000000"),     // AAD group restricting access
				// Templates:       pulumi.StringArray{pulumi.String("D365_CDSSampleApp")},    // Provisioning templates (immutable)
				// TemplateMetadata: pulumi.String(`{"PostProvisioningPackages": []}`),        // Template metadata JSON (immutable)
			},

			// Other optional top-level inputs — uncomment and supply values as needed:
			// Cadence:            pulumi.String("Moderate"),                                  // "Frequent" or "Moderate" (immutable)
			// OwnerId:            pulumi.String("00000000-0000-0000-0000-000000000000"),      // AAD user/group GUID
			// BillingPolicyId:    pulumi.String("00000000-0000-0000-0000-000000000000"),      // Billing policy link
			// EnvironmentGroupId: pulumi.String("00000000-0000-0000-0000-000000000000"),      // Environment group ID
			// AzureRegion:        pulumi.String("eastus"),                                    // Specific Azure region (immutable)
			// LinkedAppType:      pulumi.String("Canvas"),                                    // "Canvas" or "ModelDriven"
			// LinkedAppId:        pulumi.String("00000000-0000-0000-0000-000000000000"),      // Linked app GUID
		})
		if err != nil {
			return err
		}

		ctx.Export("envId", env.ID())
		ctx.Export("envDisplayName", env.DisplayName)
		ctx.Export("envState", env.State)
		ctx.Export("envType", env.EnvironmentType)
		ctx.Export("envLocation", env.Location)

		// Computed outputs from the Dataverse block
		ctx.Export("envDataverseUrl", env.Dataverse.ApplyT(func(d *pp.Dataverse) string {
			if d == nil {
				return ""
			}
			if d.Url == nil {
				return ""
			}
			return *d.Url
		}).(pulumi.StringOutput))
		ctx.Export("envDataverseOrganizationId", env.Dataverse.ApplyT(func(d *pp.Dataverse) string {
			if d == nil {
				return ""
			}
			if d.OrganizationId == nil {
				return ""
			}
			return *d.OrganizationId
		}).(pulumi.StringOutput))
		ctx.Export("envDataverseUniqueName", env.Dataverse.ApplyT(func(d *pp.Dataverse) string {
			if d == nil {
				return ""
			}
			if d.UniqueName == nil {
				return ""
			}
			return *d.UniqueName
		}).(pulumi.StringOutput))
		ctx.Export("envDataverseVersion", env.Dataverse.ApplyT(func(d *pp.Dataverse) string {
			if d == nil {
				return ""
			}
			if d.Version == nil {
				return ""
			}
			return *d.Version
		}).(pulumi.StringOutput))

		return nil
	})
}
