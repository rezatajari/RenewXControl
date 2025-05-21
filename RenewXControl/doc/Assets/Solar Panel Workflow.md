# Solar Panel Workflow

## Overview
A **solar panel** is responsible for converting sunlight into electrical energy. In this system, the solar panel generates power based on sunlight intensity (irradiance) and follows a defined **set point** to control its operation.

## Why We Use This Approach
- The solar panel needs to **adjust its power generation** dynamically based on environmental conditions.
- It allows for **realistic energy modeling**, even when external conditions change.
- The panel operates **autonomously**, determining when it should generate power based on predefined conditions.
- It ensures that the system remains **flexible** for future expansion or modifications.

## How It Works
### **1. Initialization**
- The solar panel is initialized with specific properties:
  - **Irradiance** (W/m²) – represents sunlight intensity.
  - **Active Power** (kW) – the actual generated power.
  - **Set Point** – defines the operational limit.
  - **Power Status Message** – keeps track of its current state.

### **2. Power Generation Logic**
- The panel starts generating power **only if** there is sufficient irradiance and an operational set point.
- If irradiance or set point is **zero**, the panel does not generate power.
- The panel determines its **active power output** based on the lesser of:
  - The available irradiance.
  - The defined set point.

### **3. Updating Power Output**
- **SetSp(setPoint)**: Allows the system to adjust the set point dynamically.
- **GetAp()**: Returns the current **active power** based on irradiance and set point.
- **GetIrradiance()**: Simulates environmental changes by generating a random irradiance value.

## Real-World Analogy
Imagine a **solar farm** that operates based on weather conditions. When the sun is shining, it generates power up to a defined limit. However, during cloudy conditions, power generation is reduced accordingly. This behavior ensures that the system mimics real-world solar panel performance while maintaining control over energy output.

## Key Considerations
- A **random generator** is used to **simulate real-world irradiance changes**.
- The system dynamically **adjusts power generation** based on conditions instead of relying on fixed values.
- The set point **limits power generation** to prevent exceeding operational thresholds.

This structure ensures that the solar panel operates efficiently within the broader energy system while remaining adaptable to future improvements.

