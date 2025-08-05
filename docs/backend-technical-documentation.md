# Backend Technical Documentation

## Main Entities

### Sale & SaleItem
- **Sale** records each transaction with date, total, payment method and links to the user and branch involved. Each sale contains a collection of `SaleItem` records for the products sold.
- **SaleItem** references either a tamale or a beverage, stores quantity and subtotal per line.

### Inventory
- **InventoryItem** represents stockable items with type, current stock, unit cost and a flag indicating if the item is critical.
- **InventoryEntry** logs every stock movement as an entry, exit or waste operation and records who registered it, the quantity and date.

### Products and Ingredients
- **Tamale** and **Beverage** are sellable products. Their ingredient requirements are defined by `TamaleIngredient` and `BeverageIngredient`, which link to `InventoryItem` and specify the quantity of each ingredient needed to produce one unit.

### Combos
- **Combo** groups predefined tamales and beverages, has a fixed price and can be marked active or editable. Seasonal availability is managed through the `Season` enum. Items inside a combo are stored through `ComboItemTamale` and `ComboItemBeverage` join entities with quantities.

## Main Services

### SaleService
Handles sale creation. Validates user and branch, builds sale items, verifies ingredient stock and registers inventory exits. Saves the sale and returns a DTO.

### InventoryService
Provides CRUD for inventory items and registers stock movements. Supports entries, exits and waste operations. Enforces critical stock rules before subtracting quantities.

### ComboService
Manages combo lifecycle: create, update, clone, activate/deactivate and fetch combos with their item details.

### NotificationService
Sends push notifications via Firebase Cloud Messaging to registered device tokens.

### BranchService
Creates branches, assigns users and generates simple sales reports per branch.

## Key Process Logic

### Creating a Sale
1. Resolve user and branch.
2. For each requested item:
   - Load the product (tamale or beverage).
   - Calculate subtotal and add to sale.
   - For each ingredient, ensure sufficient stock, subtract the required quantity and record an inventory exit with reason "Sale".
3. Sum subtotals to compute the sale total, persist the sale and commit the transaction.

### Calculating Totals
`SaleService` computes the total by summing all item subtotals after processing the sale items.

### Inventory Movements
`InventoryService` centralizes stock changes:
- **Entry**: increases stock.
- **Exit**: decreases stock; rejects if quantity exceeds stock or if a critical item would go below zero.
- **Waste**: similar to exit but marked as waste for analytics.

### Using Combos
Combos bundle multiple tamales and beverages with predefined quantities. They can be cloned for seasonal variations, activated/deactivated and fully edited when `IsEditable` is true.

## Integrations and Analytics
- **Notifications**: after a sale is created, a push notification is broadcast with the sale ID and total.
- **Reports**: `BranchService` exposes a summary of sale count and totals per branch.
- **Dashboard**: `DashboardController` aggregates analytics such as daily/monthly sales, top tamales, beverage popularity by time of day, profit estimations and waste metrics.
- **FAQ**: `OpenRouterService` connects to the OpenRouter API to answer customer questions using an LLM.

## Special Configuration and Business Rules
- Inventory items marked as critical cannot be consumed when stock is zero.
- Combos can be restricted by season and toggled active/inactive.
- Role and permission checks are enforced via the `PermissionAuthorize` attribute on controllers.
