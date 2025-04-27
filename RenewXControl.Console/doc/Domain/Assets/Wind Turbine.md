# Wind Turbine - Actions & Behaviors

## Overview
Wind turbines are key assets in the **RenewXControl** system, responsible for generating electricity from wind energy. Each wind turbine operates based on real-time monitoring, ensuring optimized energy production while considering battery storage capacity.

## Properties
Each user configures their own wind turbine properties, defining values based on their specific assets. The system ensures flexibility while maintaining a minimum operational threshold.

- **Wind Speed Sensor**: Measures the wind speed (minimum value: 0 km/h).
- **Active Power**: Indicates the electricity generation capacity of the turbine (minimum value: 0 kW).
- **SetPoint**: Determines whether the turbine should generate electricity or be turned off.
- **Connection to Battery**: Ensures that generated electricity is stored efficiently.

## Behavior & Workflows
The wind turbine operates dynamically, making decisions based on real-time data received every **second**. The system continuously monitors turbine performance and battery levels to optimize energy management.

### Real-Time Monitoring & Decision-Making
- The system **reads sensor data** every second to assess wind speed and active power generation.
- Based on the collected data, the system determines if the turbine should be **activated or deactivated**.
- If the **battery reaches full capacity**, the system **automatically stops** the turbine to prevent overcharging.
- If energy demand increases or storage capacity allows, the turbine **resumes operation**.
- This real-time control ensures **efficient energy utilization and prevents energy wastage**.

## Benefits of Wind Turbine Monitoring
- **Optimized Energy Generation**: Ensures turbines operate only when energy can be efficiently stored or used.
- **Automated Decision-Making**: Reduces the need for manual intervention by users.
- **Prevention of Energy Waste**: Stops turbines when battery storage is full, avoiding unnecessary wear.
- **Scalability & Flexibility**: Each user configures their own turbine properties, making the system adaptable to different energy infrastructures.

This structured approach ensures that wind turbines are utilized efficiently while maintaining **sustainability and operational flexibility**.

