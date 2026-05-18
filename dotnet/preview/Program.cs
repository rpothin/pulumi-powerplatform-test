using System;
using System.Collections.Generic;
using Pulumi;
using Pulumi.Powerplatform;
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
    throw new InvalidOperationException($"Unknown resource: {resource}");
});
