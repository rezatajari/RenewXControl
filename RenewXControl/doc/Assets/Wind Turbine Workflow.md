# Wind Turbine Workflow

## Overview
A **wind turbine** is responsible for converting wind energy into electrical power. In this system, the wind turbine generates power based on **wind speed** and follows a defined **set point** to regulate its operation.

## Why We Use This Approach
- The wind turbine needs to **adjust power generation** dynamically based on wind conditions.
- It ensures **realistic energy modeling**, reflecting variations in wind speed.
- The turbine operates **autonomously**, determining when to generate power.
- This setup **allows future modifications** to integrate additional features.

## How It Works
### **1. Initialization**
- The wind turbine is initialized with key properties:
  - **Wind Speed** (km/h) – represents the wind intensity.
  - **Active Power** (kW) – the actual generated power.
  - **Set Point** – defines the operational limit.
  - **Power Status Message** – keeps track of its current state.

### **2. Power Generation Logic**
- The turbine starts generating power **only if** wind speed is sufficient and an operational set point is defined.
- If wind speed or set point is **zero**, the turbine does not generate power.
- The turbine determines its **active power output** based on the lesser of:
  - The available wind speed.
  - The defined set point.

### **3. Updating Power Output**
- **SetSp(setPoint)**: Adjusts the set point dynamically.
- **GetAp()**: Returns the current **active power** based on wind speed and set point.
- **GetWindSpeed()**: Simulates wind variations by generating a random wind speed value.

## Real-World Analogy
Think of a **wind farm** where turbines spin at different speeds depending on wind strength. On a windy day, the turbine can reach its full power output, but on a calm day, it produces little or no electricity. This ensures that the system mirrors real-world wind turbine behavior while maintaining controlled energy generation.

## Key Considerations
- A **random generator** is used to **simulate real-world wind speed changes**.
- The system dynamically **adjusts power generation** based on wind conditions.
- The set point **limits power generation** to prevent exceeding operational thresholds.

This structure ensures that the wind turbine operates efficiently within the broader energy system while remaining adaptable to future enhancements.