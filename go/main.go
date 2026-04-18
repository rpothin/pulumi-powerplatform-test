package main

import (
	pp "github.com/rpothin/pulumi-powerplatform/sdk/go/powerplatform"
	"github.com/pulumi/pulumi/sdk/v3/go/pulumi"
)

func main() {
	pulumi.Run(func(ctx *pulumi.Context) error {
		env, err := pp.NewEnvironment(ctx, "test-env", &pp.EnvironmentArgs{
			DisplayName:     pulumi.String("SDK Test"),
			Location:        pulumi.String("unitedstates"),
			EnvironmentType: pulumi.String("Sandbox"),
		})
		if err != nil {
			return err
		}
		ctx.Export("envId", env.ID())
		return nil
	})
}
