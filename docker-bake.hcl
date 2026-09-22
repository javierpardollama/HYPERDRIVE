group "default" {
  targets = [
    "hyperdrive-gateway-service",
    "hyperdrive-identity-service",
    "hyperdrive-storage-service"
  ]
}

target "hyperdrive-gateway-service" {
  context    = "."
  dockerfile = "Hyperdrive.Gateway.Service/Hyperdrive.Gateway.Service/Dockerfile"
  tags       = ["hyperdrive-gateway-service:latest"]
}

target "hyperdrive-identity-service" {
  context    = "."
  dockerfile = "Hyperdrive.Identity.Service/Hyperdrive.Identity.Service/Dockerfile"
  tags       = ["hyperdrive-identity-service:latest"]
}

target "hyperdrive-storage-service" {
  context    = "."
  dockerfile = "Hyperdrive.Storage.Service/Hyperdrive.Storage.Service/Dockerfile"
  tags       = ["hyperdrive-storage-service:latest"]
}
