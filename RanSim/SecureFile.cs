using System;
using System.IO;
using System.Text;

namespace RansomwareSimulator
{
    public enum FileStatus
    {
        Safe,
        Encrypted,
        Corrupted
    }

    public class SecureFile
    {
        public string FilePath { get; set; }
        public FileStatus Status { get; private set; }

        public SecureFile(string path)
        {
            FilePath = path;
            Status = FileStatus.Safe;
        }

        // Encodes file content to Base64 and marks it as Encrypted
        public void EncryptFile()
        {
            try
            {
                string originalContent = File.ReadAllText(FilePath);
                byte[] textBytes = Encoding.UTF8.GetBytes(originalContent);
                string encryptedContent = Convert.ToBase64String(textBytes);
                File.WriteAllText(FilePath, encryptedContent);

                Status = FileStatus.Encrypted;
            }
            catch (Exception ex)
            {
                Status = FileStatus.Corrupted;
                Console.WriteLine("Encryption error: " + ex.Message);
            }
        }

        // Decodes Base64 content back to plain text and marks it as Safe
        public void DecryptFile()
        {
            try
            {
                string encryptedContent = File.ReadAllText(FilePath);
                byte[] base64Bytes = Convert.FromBase64String(encryptedContent);
                string originalContent = Encoding.UTF8.GetString(base64Bytes);
                File.WriteAllText(FilePath, originalContent);

                Status = FileStatus.Safe;
            }
            catch (Exception ex)
            {
                Status = FileStatus.Corrupted;
                Console.WriteLine("Decryption error: invalid content or file corrupted. " + ex.Message);
            }
        }
    }
}