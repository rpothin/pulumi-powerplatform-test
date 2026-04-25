package myproject;

import com.pulumi.Pulumi;
import io.github.rpothin.powerplatform.Environment;
import io.github.rpothin.powerplatform.EnvironmentArgs;
import io.github.rpothin.powerplatform.EnvironmentGroup;
import io.github.rpothin.powerplatform.EnvironmentGroupArgs;
import io.github.rpothin.powerplatform.DlpPolicy;
import io.github.rpothin.powerplatform.DlpPolicyArgs;
import io.github.rpothin.powerplatform.BillingPolicy;
import io.github.rpothin.powerplatform.BillingPolicyArgs;
import io.github.rpothin.powerplatform.ManagedEnvironment;
import io.github.rpothin.powerplatform.ManagedEnvironmentArgs;
import io.github.rpothin.powerplatform.EnvironmentBackup;
import io.github.rpothin.powerplatform.EnvironmentBackupArgs;
import io.github.rpothin.powerplatform.RoleAssignment;
import io.github.rpothin.powerplatform.RoleAssignmentArgs;
import io.github.rpothin.powerplatform.IsvContract;
import io.github.rpothin.powerplatform.IsvContractArgs;
import io.github.rpothin.powerplatform.EnvironmentSettings;
import io.github.rpothin.powerplatform.EnvironmentSettingsArgs;

public class App {
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
                default:
                    throw new RuntimeException("Unknown resource: " + resource);
            }
        });
    }
}
