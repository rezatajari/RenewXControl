# **Asset Management System**

## **Core**
### 1. Base Asset Structure
- **`Asset` Class (Parent)**
  - Foundation for all energy assets
  - Key Properties:
    - `Id`: Unique identifier (auto-incremented)
    - `SiteId`: Links asset to physical location
    - `Name`: Human-readable identifier (e.g., "Battery1")

### 2. Specialized Asset Types
| Asset Type    | Key Properties               | Control Methods          | Status Tracking          |
|---------------|------------------------------|--------------------------|--------------------------|
| **Battery**   | `StateOfCharge`, `Capacity`  | `Charge()`, `Discharge()`| `ChargeStateMessage`     |
| **SolarPanel**| `Irradiance`, `ActivePower`  | `Start()`, `Off()`       | `PowerStatusMessage`     |
| **WindTurbine**| `WindSpeed`, `ActivePower`  | `Start()`, `Off()`       | `PowerStatusMessage`     |

---

## **Key Functionalities**
### 1. Common Patterns
- **SetPoint Control**
  - All assets use `SetPoint` to determine operational status
  - Modified via `SetSp()` (randomized for simulation)
  
- **Auto-Naming**
  - Assets self-generate names (e.g., "WT3" for WindTurbine #3)

### 2. Type-Specific Behaviors
#### **Battery**
- **Charge Management**:
  - `Charge()`: Uses solar/wind power to fill capacity
  - `Discharge()`: Releases energy at configured frequency
- **Smart Checks**:
  - `CheckEmpty()` auto-sets `IsNeedToCharge` flag

#### **SolarPanel/WindTurbine**
- **Power Generation**:
  - `Start()`/`Off()` toggle production based on `SetPoint`
  - `GetAp()` calculates real-time output
- **Environmental Simulation**:
  - `GetIrradiance()`/`GetWindSpeed()` generate random values

---

## **Workflow Overview**
1. **Initialization**
   - Create assets with configs and `SiteId`
   ```csharp
   var battery = new Battery(config, siteId: 101);
   ```

2. **Operation Control**
   - Toggle states:
     ```csharp
     solarPanel.Start();  // Begins power generation
     battery.Discharge(); // Releases stored energy
     ```

3. **Status Monitoring**
   - Check messages:
     ```plaintext
     > battery.ChargeStateMessage
     "Battery is charging"
     ```

---

## **Design Benefits**
| Feature               | Advantage                                                                 |
|-----------------------|--------------------------------------------------------------------------|
| **Inheritance**       | Shared properties in `Asset` reduce code duplication                    |
| **Config-Driven**     | Initial settings (capacity, setpoints) injected via config classes      |
| **Real-Time Simulation** | Random values simulate live environmental conditions                   |
| **State Awareness**   | Messages (`PowerStatusMessage`) provide human-readable operational status |
