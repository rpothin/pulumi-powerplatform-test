package myproject;

import com.pulumi.Pulumi;
import io.github.rpothin.powerplatform.AdminManagementApplication;
import io.github.rpothin.powerplatform.AdminManagementApplicationArgs;
import io.github.rpothin.powerplatform.BillingPolicy;
import io.github.rpothin.powerplatform.BillingPolicyArgs;
import io.github.rpothin.powerplatform.DataRecord;
import io.github.rpothin.powerplatform.DataRecordArgs;
import io.github.rpothin.powerplatform.DlpPolicy;
import io.github.rpothin.powerplatform.DlpPolicyArgs;
import io.github.rpothin.powerplatform.EnterprisePolicyLink;
import io.github.rpothin.powerplatform.EnterprisePolicyLinkArgs;
import io.github.rpothin.powerplatform.Environment;
import io.github.rpothin.powerplatform.EnvironmentArgs;
import io.github.rpothin.powerplatform.EnvironmentApplicationAdmin;
import io.github.rpothin.powerplatform.EnvironmentApplicationAdminArgs;
import io.github.rpothin.powerplatform.EnvironmentBackup;
import io.github.rpothin.powerplatform.EnvironmentBackupArgs;
import io.github.rpothin.powerplatform.EnvironmentGroup;
import io.github.rpothin.powerplatform.EnvironmentGroupArgs;
import io.github.rpothin.powerplatform.EnvironmentSettings;
import io.github.rpothin.powerplatform.EnvironmentSettingsArgs;
import io.github.rpothin.powerplatform.IsvContract;
import io.github.rpothin.powerplatform.IsvContractArgs;
import io.github.rpothin.powerplatform.ManagedEnvironment;
import io.github.rpothin.powerplatform.ManagedEnvironmentArgs;
import io.github.rpothin.powerplatform.PowerplatformFunctions;
import io.github.rpothin.powerplatform.RoleAssignment;
import io.github.rpothin.powerplatform.RoleAssignmentArgs;
import io.github.rpothin.powerplatform.TenantSettings;
import io.github.rpothin.powerplatform.TenantSettingsArgs;
import io.github.rpothin.powerplatform.inputs.GetAppsArgs;
import io.github.rpothin.powerplatform.inputs.GetConnectorsArgs;
import io.github.rpothin.powerplatform.inputs.GetDataRecordsArgs;
import io.github.rpothin.powerplatform.inputs.GetEnvironmentsArgs;
import io.github.rpothin.powerplatform.inputs.GetFlowsArgs;

public class App {
    private static final String DUMMY_UUID = "00000000-0000-0000-0000-000000000000";

    public static void main(String[] args) {
        Pulumi.run(ctx -> {
            var config = ctx.config();
            var resource = config.require("resource");

            switch (resource) {
                case "environment": {
                    var r = new Environment("preview", EnvironmentArgs.builder()
                        .displayName(config.get("displayName").orElse("Preview Test"))
                        .location(config.require("location"))
                        .environmentType(config.require("environmentType"))
                        .build());
                    ctx.export("id", r.id());
                    break;
                }
                case "environment-group": {
                    var r = new EnvironmentGroup("preview", EnvironmentGroupArgs.builder()
                        .displayName(config.require("displayName"))
                        .build());
                    ctx.export("id", r.id());
                    break;
                }
                case "dlp-policy": {
                    var r = new DlpPolicy("preview", DlpPolicyArgs.builder()
                        .name(config.require("name"))
                        .build());
                    ctx.export("id", r.id());
                    break;
                }
                case "billing-policy": {
                    var r = new BillingPolicy("preview", BillingPolicyArgs.builder()
                        .name(config.require("name"))
                        .location(config.require("location"))
                        .build());
                    ctx.export("id", r.id());
                    break;
                }
                case "managed-environment": {
                    var r = new ManagedEnvironment("preview", ManagedEnvironmentArgs.builder()
                        .environmentId(config.require("environmentId"))
                        .build());
                    ctx.export("id", r.id());
                    break;
                }
                case "environment-backup": {
                    var r = new EnvironmentBackup("preview", EnvironmentBackupArgs.builder()
                        .environmentId(config.require("environmentId"))
                        .label(config.require("label"))
                        .build());
                    ctx.export("id", r.id());
                    break;
                }
                case "role-assignment": {
                    var r = new RoleAssignment("preview", RoleAssignmentArgs.builder()
                        .principalObjectId(config.require("principalObjectId"))
                        .principalType(config.require("principalType"))
                        .roleDefinitionId(config.require("roleDefinitionId"))
                        .build());
                    ctx.export("id", r.id());
                    break;
                }
                case "isv-contract": {
                    var r = new IsvContract("preview", IsvContractArgs.builder()
                        .name(config.require("name"))
                        .geo(config.require("geo"))
                        .build());
                    ctx.export("id", r.id());
                    break;
                }
                case "environment-settings": {
                    var r = new EnvironmentSettings("preview", EnvironmentSettingsArgs.builder()
                        .environmentId(config.require("environmentId"))
                        .build());
                    ctx.export("id", r.id());
                    break;
                }
                case "tenant-settings": {
                    var r = new TenantSettings("preview", TenantSettingsArgs.builder().build());
                    ctx.export("id", r.id());
                    break;
                }
                case "enterprise-policy-link": {
                    var r = new EnterprisePolicyLink("preview", EnterprisePolicyLinkArgs.builder()
                        .environmentId(DUMMY_UUID)
                        .policyType("Encryption")
                        .systemId("/regions/unitedstates/providers/Microsoft.PowerPlatform/enterprisePolicies/" + DUMMY_UUID)
                        .build());
                    ctx.export("id", r.id());
                    break;
                }
                case "admin-management-application": {
                    var r = new AdminManagementApplication("preview", AdminManagementApplicationArgs.builder()
                        .applicationId(DUMMY_UUID)
                        .build());
                    ctx.export("id", r.id());
                    break;
                }
                case "data-record": {
                    var r = new DataRecord("preview", DataRecordArgs.builder()
                        .environmentId(DUMMY_UUID)
                        .tableLogicalName("accounts")
                        .build());
                    ctx.export("id", r.id());
                    break;
                }
                case "environment-application-admin": {
                    var r = new EnvironmentApplicationAdmin("preview", EnvironmentApplicationAdminArgs.builder()
                        .environmentId(DUMMY_UUID)
                        .applicationId(DUMMY_UUID)
                        .build());
                    ctx.export("id", r.id());
                    break;
                }
                case "get-environments": {
                    var result = PowerplatformFunctions.getEnvironments(GetEnvironmentsArgs.builder().build());
                    ctx.export("count", result.applyValue(r -> r.environments().size()));
                    break;
                }
                case "get-connectors": {
                    var result = PowerplatformFunctions.getConnectors(GetConnectorsArgs.builder()
                        .environmentId(config.get("environmentId").orElse(DUMMY_UUID))
                        .build());
                    ctx.export("count", result.applyValue(r -> r.connectors().size()));
                    break;
                }
                case "get-apps": {
                    var result = PowerplatformFunctions.getApps(GetAppsArgs.builder()
                        .environmentId(config.get("environmentId").orElse(DUMMY_UUID))
                        .build());
                    ctx.export("count", result.applyValue(r -> r.apps().size()));
                    break;
                }
                case "get-flows": {
                    var result = PowerplatformFunctions.getFlows(GetFlowsArgs.builder()
                        .environmentId(config.get("environmentId").orElse(DUMMY_UUID))
                        .build());
                    ctx.export("count", result.applyValue(r -> r.flows().size()));
                    break;
                }
                case "get-data-records": {
                    var result = PowerplatformFunctions.getDataRecords(GetDataRecordsArgs.builder()
                        .environmentId(config.get("environmentId").orElse(DUMMY_UUID))
                        .entityCollection("accounts")
                        .build());
                    ctx.export("count", result.applyValue(r -> r.records().size()));
                    break;
                }
                default:
                    throw new RuntimeException("Unknown resource: " + resource);
            }
        });
    }
}
