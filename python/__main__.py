import pulumi
import rpothin_powerplatform as pp

config = pulumi.Config()

# New inputs — parity with Terraform provider
security_group_id = config.get("securityGroupId")
allow_bing_search = config.get_bool("allowBingSearch")
allow_moving_data_across_regions = config.get_bool("allowMovingDataAcrossRegions")
administration_mode_enabled = config.get_bool("administrationModeEnabled")
background_operation_enabled = config.get_bool("backgroundOperationEnabled")
cadence = config.get("cadence")
owner_id = config.get("ownerId")

env = pp.Environment(
    "test-env",
    display_name=config.get("displayName") or "SDK Test",
    location=config.require("location"),
    environment_type=config.require("environmentType"),
    # New inputs — parity with Terraform provider
    # AAD group GUID that restricts who can access the environment
    **({"security_group_id": security_group_id} if security_group_id is not None else {}),
    # Allow Copilot / Bing Search features in this environment
    **({"allow_bing_search": allow_bing_search} if allow_bing_search is not None else {}),
    # Allow data to move across geographic regions (e.g. for AI features)
    **({"allow_moving_data_across_regions": allow_moving_data_across_regions} if allow_moving_data_across_regions is not None else {}),
    # Put the environment into admin-only mode (blocks regular users)
    **({"administration_mode_enabled": administration_mode_enabled} if administration_mode_enabled is not None else {}),
    # Allow background operations to continue while administration mode is active
    **({"background_operation_enabled": background_operation_enabled} if background_operation_enabled is not None else {}),
    # Release cadence for environment updates: "Frequent" or "Moderate"
    **({"cadence": cadence} if cadence is not None else {}),
    # AAD user/group GUID that owns the environment
    **({"owner_id": owner_id} if owner_id is not None else {}),
    # Inputs that require pre-existing resources — uncomment and supply values as needed:
    # billing_policy_id=config.get("billingPolicyId"),          # Billing policy link
    # environment_group_id=config.get("environmentGroupId"),    # Environment group ID
    # azure_region=config.get("azureRegion"),                   # Specific Azure region (immutable)
    # templates=["D365_CDSSampleApp"],                          # Provisioning templates (immutable)
    # template_metadata='{"PostProvisioningPackages": []}',     # Template metadata JSON (immutable)
    # linked_app_type=config.get("linkedAppType"),              # "Canvas" or "ModelDriven"
    # linked_app_id=config.get("linkedAppId"),                  # Linked app GUID
    # enterprise_policies=[],                                   # List of EnterprisePolicy objects
)

pulumi.export("envId", env.id)
pulumi.export("envState", env.state)
pulumi.export("envType", env.environment_type)
pulumi.export("envLocation", env.location)

# New computed outputs
pulumi.export("envOrganizationId", env.organization_id)
pulumi.export("envUniqueName", env.unique_name)
pulumi.export("envDataverseVersion", env.dataverse_version)
pulumi.export("envLinkedAppUrl", env.linked_app_url)
