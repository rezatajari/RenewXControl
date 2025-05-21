# Monitoring Application Process

## Overview
This document explains how the asset monitoring application operates, focusing on key concepts such as asynchronous execution, continuous updates using a `while` loop, and battery charge/discharge handling. The system simulates the real-time monitoring of renewable energy sources, including wind turbines, solar panels, and batteries.

## Application Flow
The monitoring application follows a structured sequence:

1. **Load Configuration:**
   - Reads asset configuration from a JSON file (`Asset.json`).
   - Initializes `WindTurbine`, `SolarPanel`, and `Battery` objects with respective configurations.

2. **Print Initial Values:**
   - Displays the initial status of wind turbines, solar panels, and batteries.
   - Waits for user input before proceeding to live monitoring.

3. **Start Assets:**
   - Initiates energy generation from solar panels and wind turbines.

4. **Monitoring Loop (Asynchronous):**
   - Continuously updates asset status.
   - Adjusts power generation dynamically based on battery charge levels.

---

## Why Use Async and While Loop?

### Asynchronous Execution (`async Task`)
The application uses `Task.Run(MonitoringShowInformation)` to run the monitoring process asynchronously. This allows the application to:
- **Avoid blocking the main thread** so that the system remains responsive.
- **Execute background tasks concurrently**, such as monitoring and updating statuses without halting execution.

### Continuous Monitoring (`while (true) Loop`)
The infinite `while` loop ensures that:
- The system constantly updates the asset status every second (`Task.Delay(1000)`).
- Real-time conditions, such as wind speed and irradiance, influence the power output.
- Battery state changes dynamically based on energy availability.

---

## Charge and Discharge Handling
A crucial part of the monitoring system is determining when to charge or discharge the battery.

### **Charging Condition**
```csharp
if (battery is { IsNeedToCharge: true, IsStartingCharge: false })
{
    solarPanel.SetSp(2.1);
    solarPanel.PowerStatusMessage = "Solar is run..";
    windTurbine.SetSp(3.1);
    windTurbine.PowerStatusMessage = "Turbine is run..";
    _ = Task.Run(() => battery.Charge(solarPanel.GetAp(), windTurbine.GetAp()));
}
```
#### Explanation:
- If the battery **needs charging** (`IsNeedToCharge == true`) but **has not started charging yet** (`IsStartingCharge == false`):
  - **Solar panel and wind turbine start generating power** to supply the battery.
  - The battery begins charging asynchronously (`Task.Run(battery.Charge())`).

### **Discharging Condition**
```csharp
else if (battery is { IsNeedToCharge: false, IsStartingCharge: false })
{
    solarPanel.SetSp(0);
    solarPanel.PowerStatusMessage = "Solar is off..";
    windTurbine.SetSp(0);
    windTurbine.PowerStatusMessage = "Turbine is off..";
    _ = Task.Run(() => battery.Discharge());
}
```
#### Explanation:
- If the battery **does not need charging** (`IsNeedToCharge == false`) and is **not currently charging**:
  - **Solar panel and wind turbine shut down** to conserve energy.
  - The battery starts supplying power asynchronously (`Task.Run(battery.Discharge())`).

### **Why This Happens at the End of Each Iteration?**
- The system evaluates the battery’s status **after** displaying updated values.
- It ensures the UI reflects the latest power states **before** making new changes.
- Prevents unnecessary fluctuations by waiting for each cycle to complete.

---

## Summary of Key Concepts
| Feature | Purpose |
|---------|---------|
| **Async Execution** | Keeps monitoring responsive while handling charge/discharge tasks in parallel. |
| **While Loop** | Ensures continuous status updates at regular intervals. |
| **Battery Management** | Adjusts energy generation dynamically to optimize charging and discharging. |
| **Status Display First** | Shows the latest status before deciding on power adjustments. |

This structured approach ensures that the monitoring system accurately tracks renewable energy assets in real-time, optimizing efficiency and performance.

