# What is RenewXControl?

## Project Overview

RenewXControl is a smart monitoring and management system designed to optimize renewable energy usage for users who generate electricity from solar panels and wind turbines. The system enables users to track their energy assets, monitor performance, and manage energy storage for selling electricity to the government.

---

## 🌐 Live Demo

🔗 [Visit Live Website](http://alirezanuri70-001-site1.mtempurl.com/)

---

## 📷 API Documentation

![Swagger API UI](https://github.com/rezatajari/RenewXControl/blob/master/doc/Images%26Gifs/SwaggerEndpoints.jpeg)  
_Interactive Swagger documentation showing all available endpoints._

---

## Application Demo  
### 1. Initial Configuration (Console Application) 
![Initial Setup](https://github.com/rezatajari/RenewXControl/blob/master/doc/Images%26Gifs/Init.jpg)  
*System startup showing loaded configurations for all assets*

### 2. Live Monitoring (Console Application) 
![Live Monitoring Demo](https://github.com/rezatajari/RenewXControl/blob/master/doc/Images%26Gifs/Live%20Monitoring%20Application%20Process.gif)  
*Real-time operation featuring:*  
- Color-coded status updates (Green=Active, Red=Inactive)  
- Dynamic power level adjustments  
- Automatic battery state transitions

### 3. Live Monitoring (In Website) 
![Live Monitoring Demo](https://github.com/rezatajari/RenewXControl/blob/master/doc/Images%26Gifs/LiveMonitoringsInHost.gif)  
---

## Project Scope

The project focuses on providing a comprehensive application for users to oversee their renewable energy infrastructure. This includes tracking asset conditions, monitoring energy generation, and ensuring efficient energy storage and distribution.

## Project Documentation

- **[Domain](https://github.com/rezatajari/RenewXControl/tree/master/doc/Domain)**
  - This documentation dives into the domain model, covering the logic and interaction between various entities.

- **[Configuration](https://github.com/rezatajari/RenewXControl/blob/master/doc/Configuration.md)**
  - Details the configuration settings for the system, helping you understand how to set up different components.

- **[Monitoring Application Process](https://github.com/rezatajari/RenewXControl/blob/master/doc/Monitoring%20Application%20Process.md)**
  - This document explains the monitoring process, including how data is captured, updated, and displayed in real-time for asset monitoring.

---
## Key Components
### **Users & Energy Generation**

- Users generate electricity from renewable energy sources such as solar panels and wind turbines.
- The generated energy is stored in batteries before being sold to the government.

### **Assets & Properties**

- **Wind Turbine**:
  - Equipped with a sensor to measure **wind speed** (0 to 10 km/h).
  - Measures **active power** (generation capability of electricity), which can range from 0 to 30 kW.
  - Has a **setPoint** value, which determines whether the turbine should generate electricity. This allows for asset control, enabling users to turn the turbine on or off based on operational needs.

- **Solar Panel**:
  - Equipped with a **light sensor** that measures **irradiance** (solar light intensity), ranging from 0 (night) to 10 (maximum sunlight) in units of W/m².
  - Also measures **wind speed** (0 to 10 km/h) as an additional environmental factor.
  - Can generate up to **20 kW** of **active power**.
  - Has a **SetPoint** for allowing control over when it should generate electricity.

- **Battery**:
  - Stores energy with a **capacity of 0 to 50 kW**.
  - Tracks **State of Charge (SoC)**, which indicates how much energy is stored. For example, an SoC of 10 means the battery has 10 kW of charge, with a maximum capacity of 50 kW.
  - Has a **SetPoint**, which controls battery operations:
    - **SetPoint = 0**: Battery should discharge.
    - **SetPoint = 50**: Battery can charge up to this limit.
  - Has a **discharge frequency**, which determines how frequently the battery discharges energy. For example, if the frequency is set to 1, the battery discharges 5 kW per frequency unit.

### **Asset Connections**

- **Solar Panel** → Connected to a solar light sensor and wind speed sensor.
- **Wind Turbine** → Connected to a wind speed sensor.
- **Battery** → Stores energy from both solar panels and wind turbines.
- **Energy Sales** → Users sell stored energy when the battery reaches discharge capacity.

## IoT Integration

RenewXControl operates in two key sections:

- **Asset Devices**: These include all physical devices such as solar panels, wind turbines, batteries, and their associated sensors.
- **Application**: This is the software responsible for collecting, processing, and managing data from the assets.

## Read & Write Flow of Application

The application reads data from assets **every second** to ensure real-time monitoring. Since we need continuous data collection, our system will perform two primary operations:

- **Read Operations**: Collecting sensor data, such as wind speed, solar light intensity, and battery levels.
- **Write Operations**: Making decisions based on the collected data, such as adjusting battery charging and discharging operations.

This read/write operation is crucial for the system's logic, ensuring efficient energy management and decision-making.

---

## Tech Stack

- C# .NET 9 Web API  
- Blazor WebAssembly (SignalR real-time updates)  
- SQL Server (SmartASP.NET hosting)  
- Clean Architecture & Domain-Driven Design  
- Swagger for API documentation  

---

## Modular & Scalable Design

RenewXControl is designed to be highly flexible and scalable. Each user has unique assets with different configurations, so the system supports **modular asset management**. Key features include:

- **User-Specific Asset Configuration**: Users define their asset capacities, such as battery storage, wind turbine power, and solar panel limits.
- **Scalable Application Sales**: The application is designed to be sold to multiple users, each with their own asset setup.
- **Flexible Control System**: Minimum asset values are fixed, but users can customize their energy production and storage capacities based on their needs.

## Objective

The goal of RenewXControl is to provide an intelligent platform for users to efficiently manage their renewable energy infrastructure, ensuring optimal performance, real-time monitoring, and seamless energy transactions with the government.



Here's how to seamlessly integrate your GIF demonstration into the README.md with proper section flow:

---
