import * as pp from "@rpothin/powerplatform";

const env = new pp.Environment("test-env", {
    displayName: "SDK Test",
    location: "unitedstates",
    environmentType: "Sandbox",
});

export const envId = env.id;
