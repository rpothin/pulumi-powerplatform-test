import pulumi
import rpothin_powerplatform as pp

env = pp.Environment(
    "test-env",
    display_name="SDK Test",
    location="unitedstates",
    environment_type="Sandbox",
)

pulumi.export("envId", env.id)
