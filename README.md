# pulumi-powerplatform-test

Integration tests for the published [pulumi-powerplatform](https://github.com/rpothin/pulumi-powerplatform) SDK packages. These minimal Pulumi programs verify that the SDK can be installed from public registries and compiled successfully.

## What gets tested

| Tier | What | Auth needed? |
|------|------|--------------|
| **1 — Install & compile** | Install SDK from public registry, compile/type-check a minimal program | No |
| **2 — Plugin download** | `pulumi plugin install resource powerplatform <version> --server github://api.github.com/rpothin` | No |
| **3 — Preview** | `pulumi preview` with real credentials (validates provider starts, schema loads, auth works) | Yes |

## Repository structure

```
├── python/        # PyPI: rpothin-powerplatform
├── typescript/    # npm: @rpothin/powerplatform
├── dotnet/        # NuGet: Rpothin.Powerplatform
├── go/            # Go module: github.com/rpothin/pulumi-powerplatform/sdk/go/powerplatform
├── java/          # Maven Central: io.github.rpothin:powerplatform
└── .github/workflows/test-sdks.yaml
```

Each directory contains a Pulumi program that dispatches on a `resource` stack config
value (one `Pulumi.<resource>.yaml` stack config per case) to exercise a single
resource or invoke function per `pulumi preview` run — this is how the same program
covers ~20 different resources/functions across the CI matrix.

This includes the four AVM-aligned component resources under the
`powerplatform:components:*` module (`ResEnvironment`, `ResDlpPolicy`,
`ResTenantSettings`, `ResDeploymentPipeline`) and the `getDlpPolicies`,
`getDlpPolicyMigrationConfig`, and `getSecurityRoles` invoke functions, added in
[pulumi-powerplatform#73](https://github.com/rpothin/pulumi-powerplatform/pull/73)
(v0.4.0/v0.4.1).

## Running tests

### Via GitHub Actions

Trigger the **Test Published SDKs** workflow manually from the Actions tab, specifying a version to test.

### Locally

```bash
# Python
cd python
pip install -r requirements.txt
python -m py_compile __main__.py

# TypeScript
cd typescript
npm install
npx tsc --noEmit

# .NET
cd dotnet
dotnet restore && dotnet build

# Go (requires go.mod to be published upstream)
cd go
go mod tidy && go build

# Java (requires artifact on Maven Central)
cd java
gradle compileJava
```

## E2E Integration Tests

The `python-e2e` job runs a full **up → verify → refresh → destroy** lifecycle against the live Power Platform API.

### What it does

1. Creates a real Sandbox environment via `pulumi up`
2. Validates stack outputs (`envId`, `envState`, `envType`, `envLocation`)
3. Runs `pulumi refresh` to confirm the resource matches live state
4. Destroys the environment via `pulumi destroy` (always runs, even on failure)

> **Note:** This job creates and destroys a real Sandbox environment — expect ~10 minutes of runtime.

### Required Azure permissions

The OIDC app registration (`AZURE_CLIENT_ID` secret) must be assigned the **Power Platform Administrator** Azure AD role. The `pulumi up` step calls the BAP admin API to provision a Sandbox environment, which requires this elevated role.

### How to trigger

`Actions → Test Published SDKs → Run workflow → enter SDK version`

The E2E job runs automatically as part of the workflow whenever credentials are configured.

## Notes

- **No real deployments** — The `python`, `typescript`, `dotnet`, `go`, and `java` jobs only install, compile, and optionally preview. They never run `pulumi up`.
- **Go SDK** — Requires v0.1.17+. A `go.mod` was added to the upstream provider's `sdk/go/powerplatform/` directory in [pulumi-powerplatform#26](https://github.com/rpothin/pulumi-powerplatform/pull/26), making the module resolvable on the Go module proxy from that release onward.
- **Preview** — Only runs when `AZURE_CLIENT_ID` and `AZURE_TENANT_ID` are configured as repository **secrets** (Settings → Secrets and variables → Actions → Repository secrets). Authentication uses OIDC (no client secret required) — the Azure AD app registration must have a federated credential trusting the GitHub Actions OIDC issuer for this repository.
- **Optional environment/policy-scoped secrets** — A few resource cases need a real, pre-existing entity ID to preview meaningfully; when the corresponding secret isn't configured, those cases are skipped (exit 0) rather than failing the job:
  - `POWERPLATFORM_TEST_ENVIRONMENT_ID` — a Dataverse environment ID, used by `get-connectors`, `get-apps`, `get-flows`, `get-data-records`, and `get-security-roles`.
  - `POWERPLATFORM_TEST_DLP_POLICY_ID` — an existing DLP policy ID, used by `get-dlp-policy-migration-config` to read migration configuration from a real policy.
- **`res-deployment-pipeline`** — This component composes several linked `DataRecord` instances (pipeline, stages, team) and a `PipelineSharing` link to a dev environment. Without real host/dev/test environment IDs it previews against dummy GUIDs, following the same precedent as `data-record` and `environment-application-admin`; live preview may not fully succeed without configuring `preview-<lang>:hostEnvironmentId`/`devEnvironmentId`/`testEnvironmentId` in the relevant stack config.