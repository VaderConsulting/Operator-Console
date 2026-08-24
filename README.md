# Operator Console

VS 2008 VB.NET WinForms (.NET 3.5) working copy of the Stratatel CADS Operator Console: OperatorConsole is an MDI WinExe that calls gFunctions.DoLogon then opens frmUserSearch against LDAP roots from external.config and a CADS config WCF endpoint. Password Master (Backdoor) XMLEncrypts a daily Stratatel password with gFunctions (hardcoded key gitignored; see frmMain.vb.example); WCFHostTest self-hosts IService EchoWithGet/EchoWithPost/GetImage on http://localhost:8000/. Open `Operator Console.sln` in Visual Studio. This is a historical working copy from Dave Robinson / VaderConsulting.

**Source last updated:** 2008-12-02  
**Language:** VB.NET  
**Target:** v3.5  
**Output:** Library, WinExe

## What it is

VS 2008 VB.NET WinForms (.NET 3.5) working copy of the Stratatel CADS Operator Console: OperatorConsole is an MDI WinExe that calls gFunctions.DoLogon then opens frmUserSearch against LDAP roots from external.config and a CADS config WCF endpoint. Password Master (Backdoor) XMLEncrypts a daily Stratatel password with gFunctions (hardcoded key gitignored; see frmMain.vb.example); WCFHostTest self-hosts IService EchoWithGet/EchoWithPost/GetImage on http://localhost:8000/. Open `Operator Console.sln` in Visual Studio. This is a historical working copy from Dave Robinson / VaderConsulting.

## Solution structure

| Project | Language | Path |
|---------|----------|------|
| `Outlook Addin 07` | VB.NET | `Outlook Addin 07/Outlook Addin 07.vbproj` |
| `Password Master` | VB.NET | `Backdoor/Password Master.vbproj` |
| `WCFHostTest` | VB.NET | `WCFHostTest/WCFHostTest.vbproj` |
| `EndpointTest` | VB.NET | `EndpointTest/EndpointTest.vbproj` |
| `Operator Console` | VB.NET | `Operator Console/Operator Console.vbproj` |
| `Security Config` | VB.NET | `Security Config/Security Config.vbproj` |

## How to open

Open `Operator Console.sln` in Visual Studio.

## Attribution and provenance

- **Assembly company:** Microsoft, Stratatel Ltd
- **Assembly copyright:** Copyright ©  2008, Copyright © Microsoft 2008, Copyright © Stratatel Ltd 2008

## License

MIT. See `LICENSE`.
