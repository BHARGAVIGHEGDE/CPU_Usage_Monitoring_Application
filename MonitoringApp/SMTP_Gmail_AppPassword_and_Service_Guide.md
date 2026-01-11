# SMTP (Gmail App Password) Setup & Windows Service Restart Guide

## 1. Prerequisites
- Google account with 2‑Step Verification enabled
- Windows Admin access
- MonitoringApp already installed as a Windows Service

---

## 2. Enable 2‑Step Verification
1. Go to https://myaccount.google.com/security
2. Enable **2‑Step Verification**

---

## 3. Create Gmail App Password
1. Go to https://myaccount.google.com/apppasswords
2. App: Mail
3. Device: Windows Computer
4. Generate password

Copy the 16‑character password.

---

## 4. Gmail SMTP Settings
SMTP Server: smtp.gmail.com  
Port: 587  
SSL: Enabled  
Username: yourgmail@gmail.com  
Password: App Password  

---

## 5. Set Environment Variables (System‑wide)

Open PowerShell as Administrator:

setx SMTP_USER "yourgmail@gmail.com" /M  
setx SMTP_PASS "your_app_password_here" /M  
setx OPEN_API "your_api_key_here" /M

Restart required for services to pick up changes.

---

## 6. Verify Environment Variables
Open a new PowerShell window:

echo $env:SMTP_USER  
echo $env:SMTP_PASS  

---

## 7. Stop the Windows Service

sc.exe stop AgenticMonitor  

---

## 8. Clean & Rebuild (Visual Studio)
Build → Clean Solution  
Build → Rebuild Solution  

---

## 9. Publish the Application

dotnet publish -c Release -r win-x64 --self-contained true  

Ensure MonitoringApp.exe exists.

---

## 10. Start the Windows Service

sc.exe start AgenticMonitor  

Verify using services.msc or:

sc.exe query AgenticMonitor  

---

## 11. Security Rules
- Do not store secrets in appsettings.json
- Always restart service after env changes

---

## 12. Summary
Gmail App Password configured  
Environment variables set  
Service rebuilt, published, and restarted  
