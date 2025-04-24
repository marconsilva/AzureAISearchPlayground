provider "azurerm" {
  features {
    resource_group {
      prevent_deletion_if_contains_resources = true
    }
    key_vault {
      purge_soft_delete_on_destroy = false
      recover_soft_deleted_key_vaults = true
    }
  }
  subscription_id = "13748603-69da-4123-807e-a97792bbf9bc"
  tenant_id       = "9acd2dbd-ad97-4768-9854-5e28ec55fc41"
}

# Configure the backend to store Terraform state in Azure Storage
terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = ">=3.0.0"
    }
  }
  backend "azurerm" {
    resource_group_name  = "devneu-mhtfstate-rg" # Replace with your resource group name
    storage_account_name = "devneumhtfstatestga" # Replace with your storage account name
    container_name       = "searchplaygroundtfstate" # Replace with your container name
    key                  = "terraform.tfstate" # Path to the state file
  }
}


data "azurerm_resource_group" "rg" {
  name = "devneu-mhsearch-rg"
}

resource "azurerm_container_registry" "acr" {
  name                = "labneumhsearchplaygroundacr"
  resource_group_name = data.azurerm_resource_group.rg.name
  location            = data.azurerm_resource_group.rg.location
  sku                 = "Basic"

  admin_enabled = true
}

resource "azurerm_role_assignment" "acr_pull" {
  principal_id                     = azurerm_container_app.app.identity[0].principal_id
  role_definition_name             = "AcrPull"
  scope                            = azurerm_container_registry.acr.id
  skip_service_principal_aad_check = true
}

# Create a User Assigned Managed Identity for secure access
resource "azurerm_user_assigned_identity" "app_identity" {
  name                = "labneumhsearchplayground-identity"
  location            = data.azurerm_resource_group.rg.location
  resource_group_name = data.azurerm_resource_group.rg.name
}

resource "azurerm_container_app_environment" "app_env" {
  name                = "labneumhsearchplayground-env"
  location            = data.azurerm_resource_group.rg.location
  resource_group_name = data.azurerm_resource_group.rg.name
}


resource "azurerm_container_app" "app" {
  name                = "labneumhsearchplayground-app"
  container_app_environment_id = azurerm_container_app_environment.app_env.id
  resource_group_name = data.azurerm_resource_group.rg.name
  revision_mode = "Single"

  identity {
    type = "SystemAssigned"
  }

  ingress {
    allow_insecure_connections = true
    external_enabled = true
    target_port      = 8080
    transport        = "auto"
    traffic_weight {
      latest_revision = true
      percentage     = 100
    }
  }

  # Add the secret definition for the ACR password
  secret {
    name  = "labneumhsearchplaygroundacr-password"
    value = azurerm_container_registry.acr.admin_password
  }

  registry {
    server   = azurerm_container_registry.acr.login_server
    username = azurerm_container_registry.acr.admin_username
    password_secret_name = "labneumhsearchplaygroundacr-password"
  }


  template {
    container {
      name   = "searchplayground"
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