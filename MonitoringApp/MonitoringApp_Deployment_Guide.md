# MonitoringApp – Build, Publish, Installer & Windows Service Setup Guide

## 1. Install Visual Studio Installer Projects Extension
1. Open Visual Studio 2022
2. Go to Extensions → Manage Extensions
3. Search for Microsoft Visual Studio Installer Projects(recent version)
4. Install and restart Visual Studio

## 2. Solution Structure
MonitoringApp (Worker Service)
MonitoringAppInstaller (Setup Project)

## 3. Create Setup Project
Right-click Solution → Add → New Project → Setup Project
Name: MonitoringAppInstaller

## 4. Publish Worker Service (Mandatory)
dotnet publish -c Release -r win-x64 --self-contained true

Publish folder:
MonitoringApp\bin\Release\net8.0\win-x64\publish
Confirm MonitoringApp.exe exists.

## 5. Configure Setup Project
- Application Folder → Add → File → MonitoringApp.exe
- Add appsettings.json
- Set appsettings.json → Always Create = True

## 6. Build Installer
Right-click MonitoringAppInstaller → Build
Output: MonitoringAppInstaller.msi

## 7. Test EXE
Run MonitoringApp.exe from publish folder

## 8. Register Windows Service (Admin)
sc.exe create AgenticMonitor binPath= "FULL_PATH_TO\MonitoringApp.exe"
sc.exe config AgenticMonitor start= auto
sc.exe start AgenticMonitor

## 9. Configuration Rules
No secrets in appsettings.json
Use environment variables

## 10. Notes
Always publish EXE

