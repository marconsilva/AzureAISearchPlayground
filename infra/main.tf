provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "rg" {
  name     = "devneu-mhsearch-rg"
  location = "North Europe"
}

resource "azurerm_container_app_environment" "app_env" {
  name                = "labneumhsearchplayground-env"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
}

resource "azurerm_container_registry" "acr" {
  name                = "labneumhsearchplaygroundacr"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  sku                 = "Basic"

  admin_enabled = false
}

resource "azurerm_container_app" "app" {
  name                = "azureai-search-playground-app"
  container_app_environment_id = azurerm_container_app_environment.app_env.id
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location

  ingress {
    external_enabled = true
    target_port      = 8080
    transport        = "auto"
    traffic_weight {
      latest_revision = true
      percentage     = 100
    }
  }

  registry {
    server   = azurerm_container_registry.acr.login_server
    username = azurerm_container_registry.acr.admin_username
  }

  revision_mode = "Single"

  template {
    container {
      name   = "playground"
      image  = "${azurerm_container_registry.acr.login_server}/azureai-search-playground:latest"
      cpu    = 0.5
      memory = "1.0Gi"

      env {
        name  = "AzureAISearch__ApiKey"
        value = var.azure_search_api_key
      }
      env {
        name  = "AzureAISearch__ServiceName"
        value = var.azure_search_service_name
      }
      env {
        name  = "AzureAISearch__IndexName"
        value = var.azure_search_index_name
      }
      env {
        name  = "AzureAISearch__Endpoint"
        value = var.azure_search_endpoint
      }
    }
  }
}