using System;
using System.Collections.Generic;
using Pulumi;
using Pulumi.Powerplatform;
using Pulumi.Powerplatform.Components;
using Environment = Pulumi.Powerplatform.Environment;
using EnvironmentArgs = Pulumi.Powerplatform.EnvironmentArgs;

return await Deployment.RunAsync(() =>
{
    var config = new Pulumi.Config();
    var resource = config.Require("resource");

    const string DummyEnvId = "00000000-0000-0000-0000-000000000000";
    const string DummyUuid = "00000000-0000-0000-0000-000000000000";

    if (resource == "environment")
    {
        var r = new Environment("preview", new EnvironmentArgs
        {
            DisplayName = config.Get("displayName") ?? "Preview Test",
            Location = config.Require("location"),
            EnvironmentType = config.Require("environmentType"),
        });
        return new Dictionary<string, object?> { ["id"] = r.Id };
    }
    if (resource == "environment-group")
    {
        var r = new EnvironmentGroup("preview", new EnvironmentGroupArgs
        {
            DisplayName = config.Require("displayName"),
        });
        return new Dictionary<string, object?> { ["id"] = r.Id };
    }
    if (resource == "dlp-policy")
    {
        var r = new DlpPolicy("preview", new DlpPolicyArgs
        {
            Name = config.Require("name"),
        });
        return new Dictionary<string, object?> { ["id"] = r.Id };
    }
    if (resource == "billing-policy")
    {
        var r = new BillingPolicy("preview", new BillingPolicyArgs
        {
            Name = config.Require("name"),
            Location = config.Require("location"),
        });
        return new Dictionary<string, object?> { ["id"] = r.Id };
    }
    if (resource == "managed-environment")
    {
        var r = new ManagedEnvironment("preview", new ManagedEnvironmentArgs
        {
            EnvironmentId = config.Require("environmentId"),
        });
        return new Dictionary<string, object?> { ["id"] = r.Id };
    }
    if (resource == "environment-backup")
    {
        var r = new EnvironmentBackup("preview", new EnvironmentBackupArgs
        {
            EnvironmentId = config.Require("environmentId"),
            Label = config.Require("label"),
        });
        return new Dictionary<string, object?> { ["id"] = r.Id };
    }
    if (resource == "role-assignment")
    {
        var r = new RoleAssignment("preview", new RoleAssignmentArgs
        {
            PrincipalObjectId = config.Require("principalObjectId"),
            PrincipalType = config.Require("principalType"),
            RoleDefinitionId = config.Require("roleDefinitionId"),
        });
        return new Dictionary<string, object?> { ["id"] = r.Id };
    }
    if (resource == "isv-contract")
    {
        var r = new IsvContract("preview", new IsvContractArgs
        {
            Name = config.Require("name"),
            Geo = config.Require("geo"),
        });
        return new Dictionary<string, object?> { ["id"] = r.Id };
    }
    if (resource == "environment-settings")
    {
        var r = new EnvironmentSettings("preview", new EnvironmentSettingsArgs
        {
            EnvironmentId = config.Require("environmentId"),
        });
        return new Dictionary<string, object?> { ["id"] = r.Id };
    }
    if (resource == "tenant-settings")
    {
        var r = new TenantSettings("preview", new TenantSettingsArgs {});
        return new Dictionary<string, object?> { ["id"] = r.Id };
    }
    if (resource == "enterprise-policy-link")
    {
        var r = new EnterprisePolicyLink("preview", new EnterprisePolicyLinkArgs
        {
            EnvironmentId = DummyEnvId,
            PolicyType = "Encryption",
            SystemId = "/regions/unitedstates/providers/Microsoft.PowerPlatform/enterprisePolicies/" + DummyUuid,
        });
        return new Dictionary<string, object?> { ["id"] = r.Id };
    }
    if (resource == "admin-management-application")
    {
        var r = new AdminManagementApplication("preview", new AdminManagementApplicationArgs
        {
            ApplicationId = DummyUuid,
        });
        return new Dictionary<string, object?> { ["id"] = r.Id };
    }
    if (resource == "data-record")
    {
        var r = new DataRecord("preview", new DataRecordArgs
        {
            EnvironmentId = DummyEnvId,
            TableLogicalName = "accounts",
        });
        return new Dictionary<string, object?> { ["id"] = r.Id };
    }
    if (resource == "environment-application-admin")
    {
        var r = new EnvironmentApplicationAdmin("preview", new EnvironmentApplicationAdminArgs
        {
            EnvironmentId = DummyEnvId,
            ApplicationId = DummyUuid,
        });
        return new Dictionary<string, object?> { ["id"] = r.Id };
    }
    if (resource == "get-environments")
    {
        var result = GetEnvironments.Invoke();
        return new Dictionary<string, object?> { ["environments"] = result.Apply(r => r.Environments) };
    }
    if (resource == "get-connectors")
    {
        var result = GetConnectors.Invoke(new GetConnectorsInvokeArgs { EnvironmentId = config.Get("environmentId") ?? DummyEnvId });
        return new Dictionary<string, object?> { ["connectors"] = result.Apply(r => r.Connectors) };
    }
    if (resource == "get-apps")
    {
        var result = GetApps.Invoke(new GetAppsInvokeArgs { EnvironmentId = config.Get("environmentId") ?? DummyEnvId });
        return new Dictionary<string, object?> { ["apps"] = result.Apply(r => r.Apps) };
    }
    if (resource == "get-flows")
    {
        var result = GetFlows.Invoke(new GetFlowsInvokeArgs { EnvironmentId = config.Get("environmentId") ?? DummyEnvId });
        return new Dictionary<string, object?> { ["flows"] = result.Apply(r => r.Flows) };
    }
    if (resource == "get-data-records")
    {
        var result = GetDataRecords.Invoke(new GetDataRecordsInvokeArgs
        {
            EnvironmentId = config.Get("environmentId") ?? DummyEnvId,
            EntityCollection = "accounts",
        });
        return new Dictionary<string, object?> { ["records"] = result.Apply(r => r.Records) };
    }

    // --- AVM-aligned component resources (powerplatform:components:*) ---
    if (resource == "res-environment")
    {
        var r = new ResEnvironment("preview", new ResEnvironmentArgs
        {
            DisplayName = config.Get("displayName") ?? "Preview Component Test",
            Location = config.Require("location"),
            EnvironmentType = config.Get("environmentType") ?? "Sandbox",
        });
        return new Dictionary<string, object?>
        {
            ["resourceId"] = r.ResourceId,
            ["environmentUrl"] = r.EnvironmentUrl,
            ["environmentDisplayName"] = r.EnvironmentDisplayName,
            ["dataverseOrganizationId"] = r.DataverseOrganizationId,
            ["managedEnvironmentId"] = r.ManagedEnvironmentId,
        };
    }
    if (resource == "res-dlp-policy")
    {
        var r = new ResDlpPolicy("preview", new ResDlpPolicyArgs
        {
            DisplayName = config.Get("displayName") ?? "Preview Component DLP Policy",
            RuleSets =
            {
                new Dictionary<string, object?>
                {
                    ["classification"] = "Business",
                    ["connectors"] = new List<object>
                    {
                        new Dictionary<string, object?> { ["id"] = "/providers/Microsoft.PowerApps/apis/shared_office365" },
                    },
                },
            },
        });
        return new Dictionary<string, object?>
        {
            ["resourceId"] = r.ResourceId,
            ["policyName"] = r.PolicyName,
            ["ruleSetCount"] = r.RuleSetCount,
            ["tenantId"] = r.TenantId,
            ["lastModified"] = r.LastModified,
        };
    }
    if (resource == "res-tenant-settings")
    {
        var r = new ResTenantSettings("preview", new ResTenantSettingsArgs
        {
            WalkMeOptOut = true,
        });
        return new Dictionary<string, object?> { ["resourceId"] = r.ResourceId, ["tenantId"] = r.TenantId };
    }
    if (resource == "res-deployment-pipeline")
    {
        var hostEnvironmentId = config.Get("hostEnvironmentId") ?? DummyUuid;
        var devEnvironmentId = config.Get("devEnvironmentId") ?? DummyUuid;
        var testEnvironmentId = config.Get("testEnvironmentId") ?? DummyUuid;
        var r = new ResDeploymentPipeline("preview", new ResDeploymentPipelineArgs
        {
            HostEnvironmentId = hostEnvironmentId,
            PipelineName = config.Get("pipelineName") ?? "PreviewComponentPipeline",
            PipelineDescription = "Pipeline created by the SDK preview test harness",
            DevEnvironmentKey = "dev",
            Environments =
            {
                ["dev"] = new Pulumi.Powerplatform.Components.Inputs.PipelineEnvironmentEntryArgs { Id = devEnvironmentId, Name = "Development" },
                ["test"] = new Pulumi.Powerplatform.Components.Inputs.PipelineEnvironmentEntryArgs { Id = testEnvironmentId, Name = "Test" },
            },
            PipelineStages =
            {
                new Pulumi.Powerplatform.Components.Inputs.PipelineStageConfigArgs { EnvironmentKey = "test", Description = "Promote to test" },
            },
        });
        return new Dictionary<string, object?>
        {
            ["resourceId"] = r.ResourceId,
            ["pipelineId"] = r.PipelineId,
            ["pipelineTeamId"] = r.PipelineTeamId,
            ["deploymentEnvironmentIds"] = r.DeploymentEnvironmentIds,
            ["deploymentStageIds"] = r.DeploymentStageIds,
        };
    }

    // --- New invoke functions ---
    if (resource == "get-dlp-policies")
    {
        var result = GetDlpPolicies.Invoke();
        return new Dictionary<string, object?> { ["count"] = result.Apply(r => r.Policies.Length) };
    }
    if (resource == "get-dlp-policy-migration-config")
    {
        var result = GetDlpPolicyMigrationConfig.Invoke(new GetDlpPolicyMigrationConfigInvokeArgs
        {
            SourcePolicyId = config.Require("sourcePolicyId"),
        });
        return new Dictionary<string, object?>
        {
            ["displayName"] = result.Apply(r => r.DisplayName),
            ["ruleSetCount"] = result.Apply(r => r.RuleSets.Length),
        };
    }
    if (resource == "get-security-roles")
    {
        var result = GetSecurityRoles.Invoke(new GetSecurityRolesInvokeArgs
        {
            EnvironmentId = config.Get("environmentId") ?? DummyEnvId,
        });
        return new Dictionary<string, object?> { ["count"] = result.Apply(r => r.SecurityRoles.Length) };
    }

    throw new InvalidOperationException($"Unknown resource: {resource}");
});
