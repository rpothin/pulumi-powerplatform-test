package main

import (
"fmt"

pp "github.com/rpothin/pulumi-powerplatform/sdk/go/powerplatform"
"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
"github.com/pulumi/pulumi/sdk/v3/go/pulumi/config"
)

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

default:
return fmt.Errorf("unknown resource: %s", resource)
}

return nil
})
}
