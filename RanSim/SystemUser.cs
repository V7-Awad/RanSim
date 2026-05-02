using System;

namespace RansomwareSimulator
{
    // Base class: defines the contract for all system user types
    public abstract class SystemUser
    {
        public string Username { get; set; }

        public SystemUser(string username)
        {
            Username = username;
        }

        // Abstract method: each subclass implements its own file-handling behavior (Polymorphism)
        public abstract void ProcessFile(SecureFile targetFile);
    }

    // Subclass 1: represents an unprivileged victim with no decryption rights
    public class VictimUser : SystemUser
    {
        public VictimUser(string username) : base(username) { }

        public override void ProcessFile(SecureFile targetFile)
        {
            Console.WriteLine($"[!] Access Denied for user: {Username}");
            Console.WriteLine("🚨 SYSTEM MESSAGE: Your files are encrypted! Pay 1 Bitcoin to recover them. 🚨");
        }
    }

    // Subclass 2: represents a privileged admin who can decrypt files using a recovery key
    public class AdminUser : SystemUser
    {
        private const string ValidRecoveryKey = "CyberAdmin2026";
        private string RecoveryKey { get; set; }

        public AdminUser(string username, string key) : base(username)
        {
            RecoveryKey = key;
        }

        // Validates the recovery key before delegating decryption to SecureFile
        public override void ProcessFile(SecureFile targetFile)
        {
            if (RecoveryKey == ValidRecoveryKey)
            {
                Console.WriteLine($"[+] Access granted for admin: {Username}. Initiating recovery...");
                targetFile.DecryptFile();
                Console.WriteLine("[+] File recovered successfully.");
            }
            else
            {
                Console.WriteLine("[X] ERROR: Invalid recovery key. System lockout.");
            }
        }
    }
}