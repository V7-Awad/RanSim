using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RansomwareSimulator
{
    public partial class Form1 : Form
    {
        // Holds the list of monitored files (Array of Objects pattern)
        private List<SecureFile> vaultFiles = new List<SecureFile>();

        private const string FolderPath = @"C:\DummyData";
        private const string RecoveryKey = "CyberAdmin2026";

        public Form1()
        {
            InitializeComponent();
        }

        // Fires on form load: initializes the dummy folder, reads files, and binds them to the grid
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                    File.WriteAllText(FolderPath + @"\salary_reports.txt", "Employee: Ahmed, Salary: 5000");
                    File.WriteAllText(FolderPath + @"\passwords.txt", "AdminPass: CyberAdmin2026");
                }

                string[] filesInDirectory = Directory.GetFiles(FolderPath);
                vaultFiles.Clear();

                foreach (string file in filesInDirectory)
                    vaultFiles.Add(new SecureFile(file));

                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading files: " + ex.Message, "System Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Attack button: encrypts all files and highlights rows red
        private void btnAttack_Click(object sender, EventArgs e)
        {
            if (vaultFiles.Count == 0) return;

            foreach (SecureFile file in vaultFiles)
                file.EncryptFile();

            RefreshGrid();
            ApplyRowColors("Encrypted", Color.DarkRed, Color.White);

            MessageBox.Show(
                "🚨 ATTENTION: ALL FILES HAVE BEEN ENCRYPTED! 🚨\n\nPay 1 Bitcoin to receive the decryption key.",
                "Ransomware Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Recovery button: validates the key, decrypts all files, and highlights rows green
        private void btnRecovery_Click(object sender, EventArgs e)
        {
            string enteredKey = txtRecoveryKey.Text;

            if (enteredKey != RecoveryKey)
            {
                MessageBox.Show("ACCESS DENIED: Incorrect decryption key!", "Security Alert",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AdminUser systemAdmin = new AdminUser("Admin_User", enteredKey);

            foreach (SecureFile file in vaultFiles)
                systemAdmin.ProcessFile(file);

            RefreshGrid();
            ApplyRowColors("Safe", Color.LightGreen, Color.Black);

            MessageBox.Show("✅ SYSTEM RECOVERED: All files have been decrypted successfully.",
                "Recovery Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtRecoveryKey.Text = "";
        }

        // Rebinds the data source to reflect the latest file statuses
        private void RefreshGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = vaultFiles;
        }

        // Colors grid rows that match a given status value
        private void ApplyRowColors(string statusValue, Color backColor, Color foreColor)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Status"].Value?.ToString() == statusValue)
                {
                    row.DefaultCellStyle.BackColor = backColor;
                    row.DefaultCellStyle.ForeColor = foreColor;
                }
            }
        }
    }
}