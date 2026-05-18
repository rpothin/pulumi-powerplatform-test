import pulumi
import rpothin_powerplatform as pp

config = pulumi.Config()
resource = config.require("resource")

_DUMMY_ENV_ID = "00000000-0000-0000-0000-000000000000"
_DUMMY_UUID = "00000000-0000-0000-0000-000000000000"

if resource == "environment":
    r = pp.Environment(
        "preview",
        display_name=config.get("displayName") or "Preview Test",
        location=config.require("location"),
        environment_type=config.require("environmentType"),
    )
    pulumi.export("id", r.id)
elif resource == "environment-group":
    r = pp.EnvironmentGroup(
        "preview",
        display_name=config.require("displayName"),
    )
    pulumi.export("id", r.id)
elif resource == "dlp-policy":
    r = pp.DlpPolicy(
        "preview",
        name=config.require("name"),
    )
    pulumi.export("id", r.id)
elif resource == "billing-policy":
    r = pp.BillingPolicy(
        "preview",
        name=config.require("name"),
        location=config.require("location"),
    )
    pulumi.export("id", r.id)
elif resource == "managed-environment":
    r = pp.ManagedEnvironment(
        "preview",
        environment_id=config.require("environmentId"),
    )
    pulumi.export("id", r.id)
elif resource == "environment-backup":
    r = pp.EnvironmentBackup(
        "preview",
        environment_id=config.require("environmentId"),
        label=config.require("label"),
    )
    pulumi.export("id", r.id)
elif resource == "role-assignment":
    r = pp.RoleAssignment(
        "preview",
        principal_object_id=config.require("principalObjectId"),
        principal_type=config.require("principalType"),
        role_definition_id=config.require("roleDefinitionId"),
    )
    pulumi.export("id", r.id)
elif resource == "isv-contract":
    r = pp.IsvContract(
        "preview",
        name=config.require("name"),
        geo=config.require("geo"),
    )
    pulumi.export("id", r.id)
elif resource == "environment-settings":
    r = pp.EnvironmentSettings(
        "preview",
        environment_id=config.require("environmentId"),
    )
    pulumi.export("id", r.id)
elif resource == "tenant-settings":
    r = pp.TenantSettings("preview")
    pulumi.export("id", r.id)
elif resource == "enterprise-policy-link":
    r = pp.EnterprisePolicyLink(
        "preview",
        environment_id=_DUMMY_ENV_ID,
        policy_type="Encryption",
        system_id=f"/regions/unitedstates/providers/Microsoft.PowerPlatform/enterprisePolicies/{_DUMMY_UUID}",
    )
    pulumi.export("id", r.id)
elif resource == "admin-management-application":
    r = pp.AdminManagementApplication(
        "preview",
        application_id=_DUMMY_UUID,
    )
    pulumi.export("id", r.id)
elif resource == "data-record":
    r = pp.DataRecord(
        "preview",
        environment_id=_DUMMY_ENV_ID,
        table_logical_name="accounts",
    )
    pulumi.export("id", r.id)
elif resource == "environment-application-admin":
    r = pp.EnvironmentApplicationAdmin(
        "preview",
        environment_id=_DUMMY_ENV_ID,
        application_id=_DUMMY_UUID,
    )
    pulumi.export("id", r.id)
elif resource == "get-environments":
    result = pp.get_environments()
    pulumi.export("environments", result["environments"])
elif resource == "get-connectors":
    env_id = config.get("environmentId") or _DUMMY_ENV_ID
    result = pp.get_connectors(environment_id=env_id)
    pulumi.export("connectors", result["connectors"])
elif resource == "get-apps":
    env_id = config.get("environmentId") or _DUMMY_ENV_ID
    result = pp.get_apps(environment_id=env_id)
    pulumi.export("apps", result["apps"])
elif resource == "get-flows":
    env_id = config.get("environmentId") or _DUMMY_ENV_ID
    result = pp.get_flows(environment_id=env_id)
    pulumi.export("flows", result["flows"])
elif resource == "get-data-records":
    env_id = config.get("environmentId") or _DUMMY_ENV_ID
    result = pp.get_data_records(environment_id=env_id, entity_collection="accounts")
    pulumi.export("records", result.value["records"])
else:
    raise ValueError(f"Unknown resource: {resource}")
