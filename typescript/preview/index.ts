import * as pulumi from "@pulumi/pulumi";
import * as pp from "@rpothin/powerplatform";

const config = new pulumi.Config();
const resource = config.require("resource");

if (resource === "environment") {
    const r = new pp.Environment("preview", {
        displayName: config.get("displayName") ?? "Preview Test",
        location: config.require("location"),
        environmentType: config.require("environmentType"),
    });
    pulumi.export("id", r.id);
} else if (resource === "environment-group") {
    const r = new pp.EnvironmentGroup("preview", {
        displayName: config.require("displayName"),
    });
    pulumi.export("id", r.id);
} else if (resource === "dlp-policy") {
    const r = new pp.DlpPolicy("preview", {
        name: config.require("name"),
    });
    pulumi.export("id", r.id);
} else if (resource === "billing-policy") {
    const r = new pp.BillingPolicy("preview", {
        name: config.require("name"),
        location: config.require("location"),
    });
    pulumi.export("id", r.id);
} else if (resource === "managed-environment") {
    const r = new pp.ManagedEnvironment("preview", {
        environmentId: config.require("environmentId"),
    });
    pulumi.export("id", r.id);
} else if (resource === "environment-backup") {
    const r = new pp.EnvironmentBackup("preview", {
        environmentId: config.require("environmentId"),
        label: config.require("label"),
    });
    pulumi.export("id", r.id);
} else if (resource === "role-assignment") {
    const r = new pp.RoleAssignment("preview", {
        principalObjectId: config.require("principalObjectId"),
        principalType: config.require("principalType"),
        roleDefinitionId: config.require("roleDefinitionId"),
    });
    pulumi.export("id", r.id);
} else if (resource === "isv-contract") {
    const r = new pp.IsvContract("preview", {
        name: config.require("name"),
        geo: config.require("geo"),
    });
    pulumi.export("id", r.id);
} else if (resource === "environment-settings") {
    const r = new pp.EnvironmentSettings("preview", {
        environmentId: config.require("environmentId"),
    });
    pulumi.export("id", r.id);
} else {
    throw new Error(`Unknown resource: ${resource}`);
}
