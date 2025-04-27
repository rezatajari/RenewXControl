# Battery Workflow

## Overview
The battery component plays a crucial role in managing energy storage within the system. It ensures that power is stored when available and discharged when needed. The battery's behavior is determined by its charge level, set points, and interaction with external power sources.

## Initialization
The battery is initialized using configuration data, ensuring it starts with predefined values. These include:
- **Capacity**: The total energy storage limit of the battery (in kW).
- **State of Charge (SoC)**: The current stored energy level (in kW).
- **Set Point**: A predefined threshold that dictates when the battery should stop discharging.
- **Frequently Of Discharge**: Controls the discharge rate.
- **ChargeStateMessage**: A status message indicating whether the battery is charging, discharging, or idle.
- **IsNeedToCharge**: A flag indicating if the battery requires charging.
- **IsStartingCharge**: A flag determining whether charging is actively occurring.

If the battery's charge level is below its full capacity at initialization, **IsNeedToCharge** is set to `true`, meaning it will be considered for charging.

## Charging Process
Charging begins when power is available from external sources (e.g., solar and wind energy). During this process:
1. **IsStartingCharge** is set to `true`, indicating active charging.
2. **ChargeStateMessage** updates to reflect the charging state.
3. The total available power (sum of solar and wind power) is added to the **StateOfCharge** every second.
4. Once **StateOfCharge** reaches the **Capacity**, charging stops, and:
   - **IsNeedToCharge** is set to `false`.
   - **IsStartingCharge** is set to `false`.
   - **ChargeStateMessage** updates to "Charge complete."

This ensures that the battery does not overcharge and maintains its optimal level.

## Discharging Process
The battery discharges when it needs to supply power to the system. The discharge process follows these steps:
1. **IsNeedToCharge** is set to `false`, indicating that the battery is providing power.
2. **IsStartingCharge** is set to `true` to signify an active discharge.
3. **ChargeStateMessage** updates to "Battery is discharging."
4. The energy is reduced over time at a controlled rate, ensuring a gradual discharge until the **SetPoint** is reached.
5. When **StateOfCharge** reaches **SetPoint**:
   - Discharging stops.
   - **IsStartingCharge** is set to `false`.
   - **IsNeedToCharge** is set to `true`, signaling that the battery now requires charging.
   - **ChargeStateMessage** updates to "Discharge complete."

This structured approach ensures that the battery maintains a balanced cycle of charging and discharging, optimizing its efficiency in real-world usage.

## SetPoint Adjustment
The **SetSp** method allows for modifying the discharge threshold dynamically. When updated:
1. **SetPoint** is adjusted to the new value.
2. The battery initiates the discharge process if required.
3. A system message logs the update to provide real-time feedback.