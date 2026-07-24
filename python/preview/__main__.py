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
    pulumi.export("environments", result.environments)
elif resource == "get-connectors":
    env_id = config.get("environmentId") or _DUMMY_ENV_ID
    result = pp.get_connectors(environment_id=env_id)
    pulumi.export("connectors", result.connectors)
elif resource == "get-apps":
    env_id = config.get("environmentId") or _DUMMY_ENV_ID
    result = pp.get_apps(environment_id=env_id)
    pulumi.export("apps", result.apps)
elif resource == "get-flows":
    env_id = config.get("environmentId") or _DUMMY_ENV_ID
    result = pp.get_flows(environment_id=env_id)
    pulumi.export("flows", result.flows)
elif resource == "get-data-records":
    env_id = config.get("environmentId") or _DUMMY_ENV_ID
    result = pp.get_data_records(environment_id=env_id, entity_collection="accounts")
    pulumi.export("records", result.records)
# --- AVM-aligned component resources (powerplatform:components:*) ---
elif resource == "res-environment":
    r = pp.components.ResEnvironment(
        "preview",
        pp.components.ResEnvironmentArgs(
            display_name=config.get("displayName") or "Preview Component Test",
            location=config.require("location"),
            environment_type=config.get("environmentType") or "Sandbox",
        ),
    )
    pulumi.export("resourceId", r.resource_id)
    pulumi.export("environmentUrl", r.environment_url)
    pulumi.export("environmentDisplayName", r.environment_display_name)
    pulumi.export("dataverseOrganizationId", r.dataverse_organization_id)
    pulumi.export("managedEnvironmentId", r.managed_environment_id)
elif resource == "res-dlp-policy":
    r = pp.components.ResDlpPolicy(
        "preview",
        pp.components.ResDlpPolicyArgs(
            display_name=config.get("displayName") or "Preview Component DLP Policy",
            rule_sets=[
                {
                    "classification": "Business",
                    "connectors": [
                        {"id": "/providers/Microsoft.PowerApps/apis/shared_office365"},
                    ],
                },
            ],
        ),
    )
    pulumi.export("resourceId", r.resource_id)
    pulumi.export("policyName", r.policy_name)
    pulumi.export("ruleSetCount", r.rule_set_count)
    pulumi.export("tenantId", r.tenant_id)
    pulumi.export("lastModified", r.last_modified)
elif resource == "res-tenant-settings":
    r = pp.components.ResTenantSettings(
        "preview",
        pp.components.ResTenantSettingsArgs(
            walk_me_opt_out=True,
        ),
    )
    pulumi.export("resourceId", r.resource_id)
    pulumi.export("tenantId", r.tenant_id)
elif resource == "res-deployment-pipeline":
    host_environment_id = config.get("hostEnvironmentId") or _DUMMY_ENV_ID
    dev_environment_id = config.get("devEnvironmentId") or _DUMMY_ENV_ID
    test_environment_id = config.get("testEnvironmentId") or _DUMMY_ENV_ID
    r = pp.components.ResDeploymentPipeline(
        "preview",
        pp.components.ResDeploymentPipelineArgs(
            host_environment_id=host_environment_id,
            pipeline_name=config.get("pipelineName") or "PreviewComponentPipeline",
            pipeline_description="Pipeline created by the SDK preview test harness",
            dev_environment_key="dev",
            environments={
                "dev": pp.components.PipelineEnvironmentEntryArgs(
                    id=dev_environment_id,
                    name="Development",
                ),
                "test": pp.components.PipelineEnvironmentEntryArgs(
                    id=test_environment_id,
                    name="Test",
                ),
            },
            pipeline_stages=[
                pp.components.PipelineStageConfigArgs(
                    environment_key="test",
                    description="Promote to test",
                ),
            ],
        ),
    )
    pulumi.export("resourceId", r.resource_id)
    pulumi.export("pipelineId", r.pipeline_id)
    pulumi.export("pipelineTeamId", r.pipeline_team_id)
    pulumi.export("deploymentEnvironmentIds", r.deployment_environment_ids)
    pulumi.export("deploymentStageIds", r.deployment_stage_ids)
# --- New invoke functions ---
elif resource == "get-dlp-policies":
    result = pp.get_dlp_policies()
    pulumi.export("policies", result.policies)
elif resource == "get-dlp-policy-migration-config":
    source_policy_id = config.require("sourcePolicyId")
    result = pp.get_dlp_policy_migration_config(source_policy_id=source_policy_id)
    pulumi.export("displayName", result.display_name)
    pulumi.export("ruleSetCount", len(result.rule_sets or []))
elif resource == "get-security-roles":
    env_id = config.get("environmentId") or _DUMMY_ENV_ID
    result = pp.get_security_roles(environment_id=env_id)
    pulumi.export("securityRoles", result.security_roles)
else:
    raise ValueError(f"Unknown resource: {resource}")
