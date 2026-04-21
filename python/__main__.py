import pulumi
import rpothin_powerplatform as pp

config = pulumi.Config()

env = pp.Environment(
    "test-env",
    display_name=config.get("displayName") or "SDK Test",
    location=config.require("location"),
    environment_type=config.require("environmentType"),
)

pulumi.export("envId", env.id)
pulumi.export("envState", env.state)
pulumi.export("envType", env.environment_type)
pulumi.export("envLocation", env.location)
