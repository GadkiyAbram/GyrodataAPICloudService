# GyrodataAPICloudService

This is the key part of the Gyrodata Sakhalin Inventory System.
This service is responsible for all communications between client applications and DB (MSSQL). Client applications of the System:
  - Android Inventory Application (https://github.com/GadkiyAbram/GyrodataSakhalinKtln)
  - Windows Inventory Application (https://github.com/GadkiyAbram/GyrodataSakhalin)
  - Web-based Inventory Application (https://github.com/GadkiyAbram/gyrodata-sakhalin)
  
The Service consists of the following sub-services:
  - AuthServices. Responsible for generating individual Tokens, authenticating the Users in client applications. Further actions with Inventory Data are only possible if the User is stored in the DB (can be inserted by admin or sending the request via web-based application).
  - ToolServices. Responsible for providing Company's Items Inventory Data to the client applications and further managing one.
  - BatteryServices. Responsible for providing Company's Battery Inventory Data to the client applications and further managing one.
  - JobServices. Responsible for Company Job Log Data. The same actions as for Tools/Items, e.g. CRUD.
