import pulumi
import rpothin_powerplatform as pp

config = pulumi.Config()

# Optional top-level inputs
cadence = config.get("cadence")
owner_id = config.get("ownerId")

env = pp.Environment(
    "test-env",
    display_name=config.get("displayName") or "SDK Test",
    location=config.require("location"),
    environment_type=config.require("environmentType"),
    # Allow Copilot / Bing Search features in this environment (top-level)
    allow_bing_search=False,
    # Allow data to move across geographic regions, e.g. for AI features (top-level)
    allow_moving_data_across_regions=False,
    # Release cadence for environment updates: "Frequent" or "Moderate" (immutable)
    **({"cadence": cadence} if cadence is not None else {}),
    # AAD user/group GUID that owns the environment
    **({"owner_id": owner_id} if owner_id is not None else {}),
    # Dataverse block — include this to provision a Dataverse database.
    # Omit the block entirely to create an environment without Dataverse.
    dataverse={
        # currencyCode and languageCode are required when dataverse is specified
        "currencyCode": "USD",
        "languageCode": 1033,  # 1033 = English
        # Put the environment into admin-only mode (blocks regular users)
        "administrationModeEnabled": False,
        # Allow background operations to continue while administration mode is active
        "backgroundOperationEnabled": True,
        # Optional Dataverse fields — uncomment and supply values as needed:
        # "domainName": "my-env",                              # Custom domain prefix
        # "securityGroupId": "00000000-0000-0000-0000-000000000000",  # AAD group restricting access
        # "templates": ["D365_CDSSampleApp"],                  # Provisioning templates (immutable)
        # "templateMetadata": '{"PostProvisioningPackages": []}',     # Template metadata JSON (immutable)
    },
    # Other optional top-level inputs that require pre-existing resources:
    # billing_policy_id=config.get("billingPolicyId"),         # Billing policy link
    # environment_group_id=config.get("environmentGroupId"),   # Environment group ID
    # azure_region=config.get("azureRegion"),                  # Specific Azure region (immutable)
    # linked_app_type=config.get("linkedAppType"),             # "Canvas" or "ModelDriven"
    # linked_app_id=config.get("linkedAppId"),                 # Linked app GUID
    # enterprise_policies=[],                                  # List of EnterprisePolicy objects
)

pulumi.export("envId", env.id)
pulumi.export("envDisplayName", env.display_name)
pulumi.export("envState", env.state)
pulumi.export("envType", env.environment_type)
pulumi.export("envLocation", env.location)

# Computed outputs from the Dataverse block
pulumi.export("envDataverseUrl",              env.dataverse.apply(lambda d: d.get("url", "") if d else ""))
pulumi.export("envDataverseOrganizationId",   env.dataverse.apply(lambda d: d.get("organizationId", "") if d else ""))
pulumi.export("envDataverseUniqueName",       env.dataverse.apply(lambda d: d.get("uniqueName", "") if d else ""))
pulumi.export("envDataverseVersion",          env.dataverse.apply(lambda d: d.get("version", "") if d else ""))
