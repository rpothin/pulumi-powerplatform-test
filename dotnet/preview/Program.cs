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
    throw new InvalidOperationException($"Unknown resource: {resource}");
});
