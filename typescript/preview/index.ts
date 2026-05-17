import * as pulumi from "@pulumi/pulumi";
import * as pp from "@rpothin/powerplatform";

const config = new pulumi.Config();
const resource = config.require("resource");

const DUMMY_ENV_ID = "00000000-0000-0000-0000-000000000000";
const DUMMY_UUID = "00000000-0000-0000-0000-000000000000";

switch (resource) {
    case "environment": {
        const r = new pp.Environment("preview", {
            displayName: config.get("displayName") ?? "Preview Test",
            location: config.require("location"),
            environmentType: config.require("environmentType"),
        });
        pulumi.export("id", r.id);
        break;
    }
    case "environment-group": {
        const r = new pp.EnvironmentGroup("preview", {
            displayName: config.require("displayName"),
        });
        pulumi.export("id", r.id);
        break;
    }
    case "dlp-policy": {
        const r = new pp.DlpPolicy("preview", {
            name: config.require("name"),
        });
        pulumi.export("id", r.id);
        break;
    }
    case "billing-policy": {
        const r = new pp.BillingPolicy("preview", {
            name: config.require("name"),
            location: config.require("location"),
        });
        pulumi.export("id", r.id);
        break;
    }
    case "managed-environment": {
        const r = new pp.ManagedEnvironment("preview", {
            environmentId: config.require("environmentId"),
        });
        pulumi.export("id", r.id);
        break;
    }
    case "environment-backup": {
        const r = new pp.EnvironmentBackup("preview", {
            environmentId: config.require("environmentId"),
            label: config.require("label"),
        });
        pulumi.export("id", r.id);
        break;
    }
    case "role-assignment": {
        const r = new pp.RoleAssignment("preview", {
            principalObjectId: config.require("principalObjectId"),
            principalType: config.require("principalType"),
            roleDefinitionId: config.require("roleDefinitionId"),
        });
        pulumi.export("id", r.id);
        break;
    }
    case "isv-contract": {
        const r = new pp.IsvContract("preview", {
            name: config.require("name"),
            geo: config.require("geo"),
        });
        pulumi.export("id", r.id);
        break;
    }
    case "environment-settings": {
        const r = new pp.EnvironmentSettings("preview", {
            environmentId: config.require("environmentId"),
        });
        pulumi.export("id", r.id);
        break;
    }
    case "tenant-settings": {
        const r = new pp.TenantSettings("preview", {});
        pulumi.export("id", r.id);
        break;
    }
    case "enterprise-policy-link": {
        const r = new pp.EnterprisePolicyLink("preview", {
            environmentId: DUMMY_ENV_ID,
            policyType: "Encryption",
            systemId: "/regions/unitedstates/providers/Microsoft.PowerPlatform/enterprisePolicies/" + DUMMY_UUID,
        });
        pulumi.export("id", r.id);
        break;
    }
    case "admin-management-application": {
        const r = new pp.AdminManagementApplication("preview", {
            applicationId: DUMMY_UUID,
        });
        pulumi.export("id", r.id);
        break;
    }
    case "data-record": {
        const r = new pp.DataRecord("preview", {
            environmentId: DUMMY_ENV_ID,
            tableLogicalName: "accounts",
        });
        pulumi.export("id", r.id);
        break;
    }
    case "environment-application-admin": {
        const r = new pp.EnvironmentApplicationAdmin("preview", {
            environmentId: DUMMY_ENV_ID,
            applicationId: DUMMY_UUID,
        });
        pulumi.export("id", r.id);
        break;
    }
    case "get-environments": {
        const result = pp.getEnvironmentsOutput({});
        pulumi.export("environments", result.environments);
        break;
    }
    case "get-connectors": {
        const result = pp.getConnectorsOutput({ environmentId: DUMMY_ENV_ID });
        pulumi.export("connectors", result.connectors);
        break;
    }
    case "get-apps": {
        const result = pp.getAppsOutput({ environmentId: DUMMY_ENV_ID });
        pulumi.export("apps", result.apps);
        break;
    }
    case "get-flows": {
        const result = pp.getFlowsOutput({ environmentId: DUMMY_ENV_ID });
        pulumi.export("flows", result.flows);
        break;
    }
    case "get-data-records": {
        const result = pp.getDataRecordsOutput({ environmentId: DUMMY_ENV_ID, entityCollection: "accounts" });
        pulumi.export("records", result.records);
        break;
    }
    default:
        throw new Error(`Unknown resource: ${resource}`);
}
