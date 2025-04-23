output "container_app_url" {
  description = "The URL of the deployed Container App."
  value       = azurerm_container_app.app.configuration[0].ingress[0].fqdn
}