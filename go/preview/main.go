package main

import (
"fmt"

pp "github.com/rpothin/pulumi-powerplatform/sdk/go/powerplatform"
ppcomponents "github.com/rpothin/pulumi-powerplatform/sdk/go/powerplatform/components"
"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
"github.com/pulumi/pulumi/sdk/v3/go/pulumi/config"
)

const dummyUUID = "00000000-0000-0000-0000-000000000000"

func main() {
pulumi.Run(func(ctx *pulumi.Context) error {
cfg := config.New(ctx, "")
resource := cfg.Require("resource")

switch resource {
case "environment":
displayName := cfg.Get("displayName")
if displayName == "" {
displayName = "Preview Test"
}
r, err := pp.NewEnvironment(ctx, "preview", &pp.EnvironmentArgs{
DisplayName:     pulumi.String(displayName),
Location:        pulumi.String(cfg.Require("location")),
EnvironmentType: pulumi.String(cfg.Require("environmentType")),
})
if err != nil {
return err
}
ctx.Export("id", r.ID())

case "environment-group":
r, err := pp.NewEnvironmentGroup(ctx, "preview", &pp.EnvironmentGroupArgs{
DisplayName: pulumi.String(cfg.Require("displayName")),
})
if err != nil {
return err
}
ctx.Export("id", r.ID())

case "dlp-policy":
r, err := pp.NewDlpPolicy(ctx, "preview", &pp.DlpPolicyArgs{
Name: pulumi.String(cfg.Require("name")),
})
if err != nil {
return err
}
ctx.Export("id", r.ID())

case "billing-policy":
r, err := pp.NewBillingPolicy(ctx, "preview", &pp.BillingPolicyArgs{
Name:     pulumi.String(cfg.Require("name")),
Location: pulumi.String(cfg.Require("location")),
})
if err != nil {
return err
}
ctx.Export("id", r.ID())

case "managed-environment":
r, err := pp.NewManagedEnvironment(ctx, "preview", &pp.ManagedEnvironmentArgs{
EnvironmentId: pulumi.String(cfg.Require("environmentId")),
})
if err != nil {
return err
}
ctx.Export("id", r.ID())

case "environment-backup":
r, err := pp.NewEnvironmentBackup(ctx, "preview", &pp.EnvironmentBackupArgs{
EnvironmentId: pulumi.String(cfg.Require("environmentId")),
Label:         pulumi.String(cfg.Require("label")),
})
if err != nil {
return err
}
ctx.Export("id", r.ID())

case "role-assignment":
r, err := pp.NewRoleAssignment(ctx, "preview", &pp.RoleAssignmentArgs{
PrincipalObjectId: pulumi.String(cfg.Require("principalObjectId")),
PrincipalType:     pulumi.String(cfg.Require("principalType")),
RoleDefinitionId:  pulumi.String(cfg.Require("roleDefinitionId")),
})
if err != nil {
return err
}
ctx.Export("id", r.ID())

case "isv-contract":
r, err := pp.NewIsvContract(ctx, "preview", &pp.IsvContractArgs{
Name: pulumi.String(cfg.Require("name")),
Geo:  pulumi.String(cfg.Require("geo")),
})
if err != nil {
return err
}
ctx.Export("id", r.ID())

case "environment-settings":
r, err := pp.NewEnvironmentSettings(ctx, "preview", &pp.EnvironmentSettingsArgs{
EnvironmentId: pulumi.String(cfg.Require("environmentId")),
})
if err != nil {
return err
}
ctx.Export("id", r.ID())

case "tenant-settings":
r, err := pp.NewTenantSettings(ctx, "preview", &pp.TenantSettingsArgs{})
if err != nil {
return err
}
ctx.Export("id", r.ID())

case "enterprise-policy-link":
r, err := pp.NewEnterprisePolicyLink(ctx, "preview", &pp.EnterprisePolicyLinkArgs{
EnvironmentId: pulumi.String(dummyUUID),
PolicyType:    pulumi.String("Encryption"),
SystemId:      pulumi.String("/regions/unitedstates/providers/Microsoft.PowerPlatform/enterprisePolicies/" + dummyUUID),
})
if err != nil {
return err
}
ctx.Export("id", r.ID())

case "admin-management-application":
r, err := pp.NewAdminManagementApplication(ctx, "preview", &pp.AdminManagementApplicationArgs{
ApplicationId: pulumi.String(dummyUUID),
})
if err != nil {
return err
}
ctx.Export("id", r.ID())

case "data-record":
r, err := pp.NewDataRecord(ctx, "preview", &pp.DataRecordArgs{
EnvironmentId:    pulumi.String(dummyUUID),
TableLogicalName: pulumi.String("accounts"),
})
if err != nil {
return err
}
ctx.Export("id", r.ID())

case "environment-application-admin":
r, err := pp.NewEnvironmentApplicationAdmin(ctx, "preview", &pp.EnvironmentApplicationAdminArgs{
EnvironmentId: pulumi.String(dummyUUID),
ApplicationId: pulumi.String(dummyUUID),
})
if err != nil {
return err
}
ctx.Export("id", r.ID())

case "get-environments":
result, err := pp.GetEnvironments(ctx, &pp.GetEnvironmentsArgs{}, nil)
if err != nil {
return err
}
ctx.Export("count", pulumi.Int(len(result.Environments)))

case "get-connectors":
envId := cfg.Get("environmentId")
if envId == "" { envId = dummyUUID }
result, err := pp.GetConnectors(ctx, &pp.GetConnectorsArgs{EnvironmentId: envId}, nil)
if err != nil {
return err
}
ctx.Export("count", pulumi.Int(len(result.Connectors)))

case "get-apps":
envId := cfg.Get("environmentId")
if envId == "" { envId = dummyUUID }
result, err := pp.GetApps(ctx, &pp.GetAppsArgs{EnvironmentId: envId}, nil)
if err != nil {
return err
}
ctx.Export("count", pulumi.Int(len(result.Apps)))

case "get-flows":
envId := cfg.Get("environmentId")
if envId == "" { envId = dummyUUID }
result, err := pp.GetFlows(ctx, &pp.GetFlowsArgs{EnvironmentId: envId}, nil)
if err != nil {
return err
}
ctx.Export("count", pulumi.Int(len(result.Flows)))

case "get-data-records":
envId := cfg.Get("environmentId")
if envId == "" { envId = dummyUUID }
result, err := pp.GetDataRecords(ctx, &pp.GetDataRecordsArgs{
EnvironmentId:    envId,
EntityCollection: "accounts",
}, nil)
if err != nil {
return err
}
ctx.Export("count", pulumi.Int(len(result.Records)))

// --- AVM-aligned component resources (powerplatform:components:*) ---
case "res-environment":
displayName := cfg.Get("displayName")
if displayName == "" {
displayName = "Preview Component Test"
}
environmentType := cfg.Get("environmentType")
if environmentType == "" {
environmentType = "Sandbox"
}
r, err := ppcomponents.NewResEnvironment(ctx, "preview", &ppcomponents.ResEnvironmentArgs{
DisplayName:     displayName,
Location:        cfg.Require("location"),
EnvironmentType: &environmentType,
})
if err != nil {
return err
}
ctx.Export("resourceId", r.ResourceId)
ctx.Export("environmentUrl", r.EnvironmentUrl)
ctx.Export("environmentDisplayName", r.EnvironmentDisplayName)
ctx.Export("dataverseOrganizationId", r.DataverseOrganizationId)
ctx.Export("managedEnvironmentId", r.ManagedEnvironmentId)

case "res-dlp-policy":
dlpDisplayName := cfg.Get("displayName")
if dlpDisplayName == "" {
dlpDisplayName = "Preview Component DLP Policy"
}
ruleSets := []interface{}{
map[string]interface{}{
"classification": "Business",
"connectors": []interface{}{
map[string]interface{}{"id": "/providers/Microsoft.PowerApps/apis/shared_office365"},
},
},
}
rDlp, err := ppcomponents.NewResDlpPolicy(ctx, "preview", &ppcomponents.ResDlpPolicyArgs{
DisplayName: dlpDisplayName,
RuleSets:    ruleSets,
})
if err != nil {
return err
}
ctx.Export("resourceId", rDlp.ResourceId)
ctx.Export("policyName", rDlp.PolicyName)
ctx.Export("ruleSetCount", rDlp.RuleSetCount)
ctx.Export("tenantId", rDlp.TenantId)
ctx.Export("lastModified", rDlp.LastModified)

case "res-tenant-settings":
walkMeOptOut := true
rTenant, err := ppcomponents.NewResTenantSettings(ctx, "preview", &ppcomponents.ResTenantSettingsArgs{
WalkMeOptOut: &walkMeOptOut,
})
if err != nil {
return err
}
ctx.Export("resourceId", rTenant.ResourceId)
ctx.Export("tenantId", rTenant.TenantId)

case "res-deployment-pipeline":
hostEnvironmentId := cfg.Get("hostEnvironmentId")
if hostEnvironmentId == "" {
hostEnvironmentId = dummyUUID
}
devEnvironmentId := cfg.Get("devEnvironmentId")
if devEnvironmentId == "" {
devEnvironmentId = dummyUUID
}
testEnvironmentId := cfg.Get("testEnvironmentId")
if testEnvironmentId == "" {
testEnvironmentId = dummyUUID
}
pipelineName := cfg.Get("pipelineName")
if pipelineName == "" {
pipelineName = "PreviewComponentPipeline"
}
rPipeline, err := ppcomponents.NewResDeploymentPipeline(ctx, "preview", &ppcomponents.ResDeploymentPipelineArgs{
HostEnvironmentId:   hostEnvironmentId,
PipelineName:        pipelineName,
PipelineDescription: pulumi.StringRef("Pipeline created by the SDK preview test harness"),
DevEnvironmentKey:   "dev",
Environments: map[string]ppcomponents.PipelineEnvironmentEntryInput{
"dev":  ppcomponents.PipelineEnvironmentEntryArgs{Id: devEnvironmentId, Name: "Development"},
"test": ppcomponents.PipelineEnvironmentEntryArgs{Id: testEnvironmentId, Name: "Test"},
},
PipelineStages: []ppcomponents.PipelineStageConfigInput{
ppcomponents.PipelineStageConfigArgs{EnvironmentKey: "test", Description: pulumi.StringRef("Promote to test")},
},
})
if err != nil {
return err
}
ctx.Export("resourceId", rPipeline.ResourceId)
ctx.Export("pipelineId", rPipeline.PipelineId)
ctx.Export("pipelineTeamId", rPipeline.PipelineTeamId)
ctx.Export("deploymentEnvironmentIds", rPipeline.DeploymentEnvironmentIds)
ctx.Export("deploymentStageIds", rPipeline.DeploymentStageIds)

// --- New invoke functions ---
case "get-dlp-policies":
result, err := pp.GetDlpPolicies(ctx, &pp.GetDlpPoliciesArgs{}, nil)
if err != nil {
return err
}
ctx.Export("count", pulumi.Int(len(result.Policies)))

case "get-dlp-policy-migration-config":
sourcePolicyId := cfg.Require("sourcePolicyId")
result, err := pp.GetDlpPolicyMigrationConfig(ctx, &pp.GetDlpPolicyMigrationConfigArgs{SourcePolicyId: sourcePolicyId}, nil)
if err != nil {
return err
}
ctx.Export("displayName", pulumi.String(result.DisplayName))
ctx.Export("ruleSetCount", pulumi.Int(len(result.RuleSets)))

case "get-security-roles":
envIdRoles := cfg.Get("environmentId")
if envIdRoles == "" { envIdRoles = dummyUUID }
result, err := pp.GetSecurityRoles(ctx, &pp.GetSecurityRolesArgs{EnvironmentId: envIdRoles}, nil)
if err != nil {
return err
}
ctx.Export("count", pulumi.Int(len(result.SecurityRoles)))

default:
return fmt.Errorf("unknown resource: %s", resource)
}

return nil
})
}
