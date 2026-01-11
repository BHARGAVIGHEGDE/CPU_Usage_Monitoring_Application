# AgenticMonitor

##  Overview
**AgenticMonitor** is a real-time monitoring tool built with **Agentic AI** principles.  
It automatically detects CPU spikes and critical .NET runtime errors on Windows systems.  
The tool uses a **multi-agent AI pipeline** to analyze incidents, determine root causes, and generate **human-readable explanations**, which are then sent as alerts via email.  

This project demonstrates **production-ready AI automation**, secure configuration management, and Windows service deployment.

---

##  Problem Statement
Enterprise .NET applications often experience CPU spikes and runtime errors, causing downtime or degraded performance.  
Traditional monitoring tools:
- Detect spikes but **don’t provide insights**  
- Require manual Event Log analysis  
- Generate alerts that are technical and hard to interpret  

**AgenticMonitor solves this by:**
- Detecting CPU spikes in real-time  
- Collecting relevant .NET runtime logs  
- Using **Agentic AI** to identify probable root causes  
- Providing actionable explanations to support engineers via email  

---

##  Use Case
- Large-scale Windows servers running .NET applications  
- DevOps teams needing **automatic incident diagnosis**  
- IT support teams receiving **human-readable AI-generated insights**  
- Enterprises where quick remediation is critical  

---

##  Architecture

        +----------------+
        | CPU Monitor    |
        +----------------+
                |
        +----------------+
        | Log Monitor    |
        +----------------+
                |
        +----------------+
        | Context Agent  |
        +----------------+
                |
        +----------------+
        | RootCauseAgent |
        +----------------+
                |
        +----------------+
        | ExplanationAgent|
        +----------------+
                |
        +----------------+
        | Email Notifier |
        +----------------+

### Components

| Component | Responsibility |
|-----------|----------------|
| CPU Monitor | Monitors system CPU usage periodically |
| Log Monitor | Reads .NET runtime Event Logs |
| Context Agent | Filters and structures relevant logs and CPU data |
| RootCauseAgent | Uses AI to determine probable root cause and confidence |
| ExplanationAgent | Converts technical root cause into actionable explanation |
| Email Notifier | Sends email alerts to support teams |
| AiService | Connects to OpenAI API for reasoning and explanation |

---

## Features
- Real-time CPU and .NET runtime monitoring  
- Multi-agent AI reasoning pipeline  
- Human-readable incident explanation  
- Confidence scoring for root cause analysis  
- Configurable thresholds and intervals  
- Secure management of API keys and SMTP credentials  
- Deployable as Windows Service via MSI installer  

---

##  Installation & Setup

### Prerequisites
- Windows Server / Windows 10+  
- [.NET 6 LTS or .NET 7 SDK](https://dotnet.microsoft.com/download)  
- SMTP server credentials (for email alerts)  
- OpenAI API key (for AI reasoning)

### Configure Environment Variables
setx OPENAI_API_KEY "your_openai_key_here"
setx SMTP_USER "smtp_user"
setx SMTP_PASS "smtp_password"

###Configure appsettings.json
{
  "Monitoring": {
    "CpuThreshold": 75,
    "LogLookbackMinutes": 5,
    "CheckIntervalSeconds": 60
  },
  "Email": {
    "SmtpServer": "smtp.company.com",
    "From": "abc@company.com",
    "To": "xyz@company.com",
    "Port": 587
  }
}
### Build And Run locally
dotnet build
dotnet run
<img width="1592" height="900" alt="image" src="https://github.com/user-attachments/assets/c7418f4f-5760-4495-86ce-4d33e5688686" />
[use the valid account key]
<img width="1582" height="778" alt="image" src="https://github.com/user-attachments/assets/db618922-161a-4f85-b786-93a15f296ee4" />



