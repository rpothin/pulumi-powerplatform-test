import pulumi
import rpothin_powerplatform as pp

env = pp.Environment(
    "test-env",
    display_name="SDK Test",
    location="unitedstates",
    environment_type="Sandbox",
)

pulumi.export("envId", env.id)
pulumi.export("envState", env.state)
pulumi.export("envType", env.environment_type)
pulumi.export("envLocation", env.location)
