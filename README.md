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

Each directory contains a minimal Pulumi program that creates a single `Environment` resource.

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