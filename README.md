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

## Notes

- **No real deployments** — These tests only install, compile, and optionally preview. They never run `pulumi up`.
- **Go SDK** — Requires a `go.mod` to exist in the upstream provider's `sdk/go/powerplatform` directory. The CI job skips if the module isn't available. See below for details.
- **Preview** — Only runs when Power Platform credentials are configured as repository secrets (`POWER_PLATFORM_CLIENT_ID`, `POWER_PLATFORM_CLIENT_SECRET`, `POWER_PLATFORM_TENANT_ID`).