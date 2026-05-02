[README.md](https://github.com/user-attachments/files/27294075/README.md)
<div align="center">

# 🔐 RanSim

**Ransomware Behaviour Simulator — C# / WinForms**

[![Language](https://img.shields.io/badge/Language-C%23-239120?logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Platform](https://img.shields.io/badge/Platform-Windows-0078D6?logo=windows&logoColor=white)](https://www.microsoft.com/windows)
[![UI](https://img.shields.io/badge/UI-Windows%20Forms-blueviolet?logo=dotnet)](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)
[![Purpose](https://img.shields.io/badge/Purpose-Educational-orange.svg)]()

> ⚠️ **For educational purposes only.**  
> This tool simulates ransomware behaviour using safe, reversible encoding on dummy files.  
> It contains **no real malware**, causes **no permanent damage**, and is intended strictly for learning and security awareness.

</div>

---

## 📖 Overview

**RanSim** is a Windows Forms desktop application that demonstrates how ransomware attacks and file recovery work — in a completely safe and controlled way. It was built as an academic project to teach core Object-Oriented Programming (OOP) concepts (inheritance, polymorphism, encapsulation, enums) in a cybersecurity context.

The simulator operates **exclusively** on a self-created dummy folder (`C:\DummyData`) filled with placeholder files. No real system files are ever touched.

---

## 🎯 Learning Objectives

This project demonstrates the following C# and OOP concepts in a practical setting:

| Concept | Where Applied |
|---|---|
| **Encapsulation** | `SecureFile` hides its status and exposes controlled methods |
| **Inheritance** | `VictimUser` and `AdminUser` extend abstract `SystemUser` |
| **Polymorphism** | `ProcessFile()` behaves differently per user type |
| **Enums** | `FileStatus` models file states (`Safe`, `Encrypted`, `Corrupted`) |
| **Collections** | `List<SecureFile>` tracks all monitored files |
| **Exception Handling** | All file I/O wrapped in `try/catch` blocks |
| **Windows Forms** | GUI with DataGridView, buttons, and dynamic row colouring |

---

## 🏗️ Project Structure

```
RanSim/
├── RanSim.slnx                  # Visual Studio solution file
└── RanSim/
    ├── Form1.cs                  # Main WinForms UI & event handlers
    ├── SecureFile.cs             # File model: encoding, decoding, status
    └── SystemUser.cs             # Abstract user hierarchy (Victim / Admin)
```

### Class Overview

```
SystemUser  (abstract)
├── VictimUser    → ProcessFile() → prints "Access Denied" message
└── AdminUser     → ProcessFile() → validates key, then calls DecryptFile()

SecureFile
├── EncryptFile() → encodes content to Base64, sets Status = Encrypted
└── DecryptFile() → decodes Base64 back to plain text, sets Status = Safe

FileStatus (enum)
└── Safe | Encrypted | Corrupted
```

---

## ✨ Features

| Feature | Description |
|---|---|
| 📁 Auto Sandbox Setup | Creates `C:\DummyData` with placeholder files on first launch |
| 🔒 File "Encryption" | Encodes file content to Base64 — fully reversible, no real cryptography |
| 🔓 Key-Gated Recovery | Decryption only proceeds if the correct recovery key is entered |
| 🎨 Visual Status Feedback | Grid rows turn **red** on attack, **green** on recovery |
| 🧩 Role-Based User Model | `AdminUser` can decrypt; `VictimUser` is denied access |
| 📊 Live DataGridView | File list updates in real time after each operation |
| 🛡️ Error Handling | Graceful `Corrupted` state if any file I/O fails |

---

## 🔧 Requirements

| Requirement | Details |
|---|---|
| OS | Windows 7 / 10 / 11 |
| Framework | .NET Framework 4.8 or .NET 6+ (Windows) |
| IDE | Visual Studio 2022 (recommended) |
| Privileges | Standard user (no admin required) |

---

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/V7-Awad/RanSim.git
cd RanSim
```

### 2. Open in Visual Studio

Open `RanSim.slnx` in **Visual Studio 2022** and let it restore dependencies automatically.

### 3. Build and Run

```
Build → Start (F5)
```

On first launch, the app automatically creates `C:\DummyData\` with two dummy files:
- `salary_reports.txt`
- `passwords.txt`

---

## 🖥️ How to Use

### 🔴 Simulate an Attack

Click the **Attack** button. RanSim will:
1. Encode all files in `C:\DummyData` to Base64
2. Set each file's status to `Encrypted`
3. Highlight all grid rows in **dark red**
4. Display a simulated ransom message

### 🟢 Recover Files

1. Enter the recovery key in the text box
2. Click the **Recovery** button. RanSim will:
   - Validate the key against the `AdminUser` credentials
   - Decode all files back to plain text
   - Set each file's status to `Safe`
   - Highlight all grid rows in **green**

> Entering a wrong key triggers an "Access Denied" message — mirroring a real ransomware recovery flow.

---

## 🔬 How It Works Internally

```
User clicks "Attack"
    └── Form1 iterates vaultFiles (List<SecureFile>)
            └── SecureFile.EncryptFile()
                    ├── Reads file content as string
                    ├── Converts to UTF-8 bytes
                    ├── Encodes to Base64 string
                    ├── Writes back to disk
                    └── Sets Status = FileStatus.Encrypted

User clicks "Recovery" (correct key)
    └── Form1 creates AdminUser("Admin_User", enteredKey)
            └── AdminUser.ProcessFile(file)   ← Polymorphism
                    ├── Validates recovery key
                    └── SecureFile.DecryptFile()
                            ├── Reads Base64 string from disk
                            ├── Decodes to original bytes
                            ├── Writes plain text back to disk
                            └── Sets Status = FileStatus.Safe
```

---

## 📸 UI Layout

```
┌─────────────────────────────────────────────┐
│              RanSim — File Monitor           │
├─────────────────────────────────────────────┤
│  FilePath                     │  Status      │
│  C:\DummyData\salary_rep...   │  Safe        │
│  C:\DummyData\passwords.txt   │  Safe        │
├─────────────────────────────────────────────┤
│  Recovery Key: [________________]            │
│                                              │
│       [🔴 Attack]      [🟢 Recovery]         │
└─────────────────────────────────────────────┘
```

---

## ⚠️ Important Notes

- **No real encryption is used.** Base64 is an encoding scheme, not a cryptographic algorithm. This is intentional — the goal is to simulate behaviour safely, not cause real harm.
- **Only dummy files are affected.** The sandbox path (`C:\DummyData`) is hardcoded and created by the app itself on first run.
- **Never store credentials in plain text** in production code. The hardcoded recovery key here exists purely for the educational demo.

---

## 🤝 Contributing

Contributions and improvements are welcome! Some ideas:

- Replace Base64 with AES encryption (still reversible, but more realistic)
- Add a configurable target directory via the UI
- Add a countdown timer to the ransom message UI
- Log all actions to a `.log` file for post-simulation review

To contribute:
1. Fork the repository
2. Create a branch: `git checkout -b feature/your-feature`
3. Commit: `git commit -m "feat: describe your change"`
4. Push and open a Pull Request

---

## 📄 License

This project is licensed under the **MIT License** — see the [LICENSE](LICENSE) file for details.

---

<div align="center">

Built for learning. Inspired by real-world attack patterns.  
**Understand the threat. Build better defences.**

</div>
