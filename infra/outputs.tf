output "container_app_url" {
  description = "The URL of the deployed Container App."
  value       = azurerm_container_app.app.ingress[0].fqdn
}

output "container_registry_login_server" {
  description = "The login server URL for the container registry"
  value       = azurerm_container_registry.acr.login_server
}

output "resource_group_name" {
  description = "The name of the resource group"
  value       = data.azurerm_resource_group.rg.name
}