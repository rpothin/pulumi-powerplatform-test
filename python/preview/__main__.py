import pulumi
import rpothin_powerplatform as pp

config = pulumi.Config()
resource = config.require("resource")

if resource == "environment":
    r = pp.Environment(
        "preview",
        display_name=config.get("displayName") or "Preview Test",
        location=config.require("location"),
        environment_type=config.require("environmentType"),
    )
elif resource == "environment-group":
    r = pp.EnvironmentGroup(
        "preview",
        display_name=config.require("displayName"),
    )
elif resource == "dlp-policy":
    r = pp.DlpPolicy(
        "preview",
        name=config.require("name"),
    )
elif resource == "billing-policy":
    r = pp.BillingPolicy(
        "preview",
        name=config.require("name"),
        location=config.require("location"),
    )
elif resource == "managed-environment":
    r = pp.ManagedEnvironment(
        "preview",
        environment_id=config.require("environmentId"),
    )
elif resource == "environment-backup":
    r = pp.EnvironmentBackup(
        "preview",
        environment_id=config.require("environmentId"),
        label=config.require("label"),
    )
elif resource == "role-assignment":
    r = pp.RoleAssignment(
        "preview",
        principal_object_id=config.require("principalObjectId"),
        principal_type=config.require("principalType"),
        role_definition_id=config.require("roleDefinitionId"),
    )
elif resource == "isv-contract":
    r = pp.IsvContract(
        "preview",
        name=config.require("name"),
        geo=config.require("geo"),
    )
elif resource == "environment-settings":
    r = pp.EnvironmentSettings(
        "preview",
        environment_id=config.require("environmentId"),
    )
else:
    raise ValueError(f"Unknown resource: {resource}")

pulumi.export("id", r.id)
