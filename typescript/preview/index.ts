import * as pulumi from "@pulumi/pulumi";
import * as pp from "@rpothin/powerplatform";

const config = new pulumi.Config();
const resource = config.require("resource");

const DUMMY_ENV_ID = "00000000-0000-0000-0000-000000000000";
const DUMMY_UUID = "00000000-0000-0000-0000-000000000000";

// eslint-disable-next-line @typescript-eslint/no-explicit-any
const outputs: Record<string, pulumi.Output<any>> = {};

switch (resource) {
    case "environment": {
        const r = new pp.Environment("preview", {
            displayName: config.get("displayName") ?? "Preview Test",
            location: config.require("location"),
            environmentType: config.require("environmentType"),
        });
        outputs["id"] = r.id;
        break;
    }
    case "environment-group": {
        const r = new pp.EnvironmentGroup("preview", {
            displayName: config.require("displayName"),
        });
        outputs["id"] = r.id;
        break;
    }
    case "dlp-policy": {
        const r = new pp.DlpPolicy("preview", {
            name: config.require("name"),
        });
        outputs["id"] = r.id;
        break;
    }
    case "billing-policy": {
        const r = new pp.BillingPolicy("preview", {
            name: config.require("name"),
            location: config.require("location"),
        });
        outputs["id"] = r.id;
        break;
    }
    case "managed-environment": {
        const r = new pp.ManagedEnvironment("preview", {
            environmentId: config.require("environmentId"),
        });
        outputs["id"] = r.id;
        break;
    }
    case "environment-backup": {
        const r = new pp.EnvironmentBackup("preview", {
            environmentId: config.require("environmentId"),
            label: config.require("label"),
        });
        outputs["id"] = r.id;
        break;
    }
    case "role-assignment": {
        const r = new pp.RoleAssignment("preview", {
            principalObjectId: config.require("principalObjectId"),
            principalType: config.require("principalType"),
            roleDefinitionId: config.require("roleDefinitionId"),
        });
        outputs["id"] = r.id;
        break;
    }
    case "isv-contract": {
        const r = new pp.IsvContract("preview", {
            name: config.require("name"),
            geo: config.require("geo"),
        });
        outputs["id"] = r.id;
        break;
    }
    case "environment-settings": {
        const r = new pp.EnvironmentSettings("preview", {
            environmentId: config.require("environmentId"),
        });
        outputs["id"] = r.id;
        break;
    }
    case "tenant-settings": {
        const r = new pp.TenantSettings("preview", {});
        outputs["id"] = r.id;
        break;
    }
    case "enterprise-policy-link": {
        const r = new pp.EnterprisePolicyLink("preview", {
            environmentId: DUMMY_ENV_ID,
            policyType: "Encryption",
            systemId: "/regions/unitedstates/providers/Microsoft.PowerPlatform/enterprisePolicies/" + DUMMY_UUID,
        });
        outputs["id"] = r.id;
        break;
    }
    case "admin-management-application": {
        const r = new pp.AdminManagementApplication("preview", {
            applicationId: DUMMY_UUID,
        });
        outputs["id"] = r.id;
        break;
    }
    case "data-record": {
        const r = new pp.DataRecord("preview", {
            environmentId: DUMMY_ENV_ID,
            tableLogicalName: "accounts",
        });
        outputs["id"] = r.id;
        break;
    }
    case "environment-application-admin": {
        const r = new pp.EnvironmentApplicationAdmin("preview", {
            environmentId: DUMMY_ENV_ID,
            applicationId: DUMMY_UUID,
        });
        outputs["id"] = r.id;
        break;
    }
    case "get-environments": {
        const result = pp.getEnvironmentsOutput({});
        outputs["environments"] = result.environments;
        break;
    }
    case "get-connectors": {
        const result = pp.getConnectorsOutput({ environmentId: config.get("environmentId") ?? DUMMY_ENV_ID });
        outputs["connectors"] = result.connectors;
        break;
    }
    case "get-apps": {
        const result = pp.getAppsOutput({ environmentId: config.get("environmentId") ?? DUMMY_ENV_ID });
        outputs["apps"] = result.apps;
        break;
    }
    case "get-flows": {
        const result = pp.getFlowsOutput({ environmentId: config.get("environmentId") ?? DUMMY_ENV_ID });
        outputs["flows"] = result.flows;
        break;
    }
    case "get-data-records": {
        const result = pp.getDataRecordsOutput({ environmentId: config.get("environmentId") ?? DUMMY_ENV_ID, entityCollection: "accounts" });
        outputs["records"] = result.records;
        break;
    }
    // --- AVM-aligned component resources (powerplatform:components:*) ---
    case "res-environment": {
        const r = new pp.components.ResEnvironment("preview", {
            displayName: config.get("displayName") ?? "Preview Component Test",
            location: config.require("location"),
            environmentType: config.get("environmentType") ?? "Sandbox",
        });
        outputs["resourceId"] = r.resourceId;
        outputs["environmentUrl"] = r.environmentUrl;
        outputs["environmentDisplayName"] = r.environmentDisplayName;
        outputs["dataverseOrganizationId"] = r.dataverseOrganizationId;
        outputs["managedEnvironmentId"] = r.managedEnvironmentId;
        break;
    }
    case "res-dlp-policy": {
        const r = new pp.components.ResDlpPolicy("preview", {
            displayName: config.get("displayName") ?? "Preview Component DLP Policy",
            ruleSets: [
                {
                    classification: "Business",
                    connectors: [
                        { id: "/providers/Microsoft.PowerApps/apis/shared_office365" },
                    ],
                },
            ],
        });
        outputs["resourceId"] = r.resourceId;
        outputs["policyName"] = r.policyName;
        outputs["ruleSetCount"] = r.ruleSetCount;
        outputs["tenantId"] = r.tenantId;
        outputs["lastModified"] = r.lastModified;
        break;
    }
    case "res-tenant-settings": {
        const r = new pp.components.ResTenantSettings("preview", {
            walkMeOptOut: true,
        });
        outputs["resourceId"] = r.resourceId;
        outputs["tenantId"] = r.tenantId;
        break;
    }
    case "res-deployment-pipeline": {
        const hostEnvironmentId = config.get("hostEnvironmentId") ?? DUMMY_ENV_ID;
        const devEnvironmentId = config.get("devEnvironmentId") ?? DUMMY_ENV_ID;
        const testEnvironmentId = config.get("testEnvironmentId") ?? DUMMY_ENV_ID;
        const r = new pp.components.ResDeploymentPipeline("preview", {
            hostEnvironmentId: hostEnvironmentId,
            pipelineName: config.get("pipelineName") ?? "PreviewComponentPipeline",
            pipelineDescription: "Pipeline created by the SDK preview test harness",
            devEnvironmentKey: "dev",
            environments: {
                dev: { id: devEnvironmentId, name: "Development" },
                test: { id: testEnvironmentId, name: "Test" },
            },
            pipelineStages: [
                { environmentKey: "test", description: "Promote to test" },
            ],
        });
        outputs["resourceId"] = r.resourceId;
        outputs["pipelineId"] = r.pipelineId;
        outputs["pipelineTeamId"] = r.pipelineTeamId;
        outputs["deploymentEnvironmentIds"] = r.deploymentEnvironmentIds;
        outputs["deploymentStageIds"] = r.deploymentStageIds;
        break;
    }
    // --- New invoke functions ---
    case "get-dlp-policies": {
        const result = pp.getDlpPoliciesOutput({});
        outputs["policies"] = result.policies;
        break;
    }
    case "get-dlp-policy-migration-config": {
        const result = pp.getDlpPolicyMigrationConfigOutput({ sourcePolicyId: config.require("sourcePolicyId") });
        outputs["displayName"] = result.displayName;
        outputs["ruleSetCount"] = result.ruleSets.apply(rs => (rs ?? []).length);
        break;
    }
    case "get-security-roles": {
        const result = pp.getSecurityRolesOutput({ environmentId: config.get("environmentId") ?? DUMMY_ENV_ID });
        outputs["securityRoles"] = result.securityRoles;
        break;
    }
    default:
        throw new Error(`Unknown resource: ${resource}`);
}

module.exports = outputs;
