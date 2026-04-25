import * as pulumi from "@pulumi/pulumi";
import * as pp from "@rpothin/powerplatform";

const config = new pulumi.Config();
const resource = config.require("resource");

function getResourceId(): pulumi.Output<string> {
    switch (resource) {
        case "environment":
            return new pp.Environment("preview", {
                displayName: config.get("displayName") ?? "Preview Test",
                location: config.require("location"),
                environmentType: config.require("environmentType"),
            }).id;
        case "environment-group":
            return new pp.EnvironmentGroup("preview", {
                displayName: config.require("displayName"),
            }).id;
        case "dlp-policy":
            return new pp.DlpPolicy("preview", {
                name: config.require("name"),
            }).id;
        case "billing-policy":
            return new pp.BillingPolicy("preview", {
                name: config.require("name"),
                location: config.require("location"),
            }).id;
        case "managed-environment":
            return new pp.ManagedEnvironment("preview", {
                environmentId: config.require("environmentId"),
            }).id;
        case "environment-backup":
            return new pp.EnvironmentBackup("preview", {
                environmentId: config.require("environmentId"),
                label: config.require("label"),
            }).id;
        case "role-assignment":
            return new pp.RoleAssignment("preview", {
                principalObjectId: config.require("principalObjectId"),
                principalType: config.require("principalType"),
                roleDefinitionId: config.require("roleDefinitionId"),
            }).id;
        case "isv-contract":
            return new pp.IsvContract("preview", {
                name: config.require("name"),
                geo: config.require("geo"),
            }).id;
        case "environment-settings":
            return new pp.EnvironmentSettings("preview", {
                environmentId: config.require("environmentId"),
            }).id;
        default:
            throw new Error(`Unknown resource: ${resource}`);
    }
}

export const id = getResourceId();
