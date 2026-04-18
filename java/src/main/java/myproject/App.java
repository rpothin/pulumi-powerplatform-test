package myproject;

import com.pulumi.Pulumi;
import io.github.rpothin.powerplatform.Environment;
import io.github.rpothin.powerplatform.EnvironmentArgs;

public class App {
    public static void main(String[] args) {
        Pulumi.run(ctx -> {
            var env = new Environment("test-env", EnvironmentArgs.builder()
                .displayName("SDK Test")
                .location("unitedstates")
                .environmentType("Sandbox")
                .build());
            ctx.export("envId", env.id());
        });
    }
}
