using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection.Emit;

namespace Echo
{
    public partial class Form1 : Form
    {
        const int MAX_TEXT_LENGTH = 36;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int index;
            do
            {
                index = random.Next()%(Data.length-1)+1;
            } while (Data.echoes[index].Length > MAX_TEXT_LENGTH && index != 0 && index < Data.length&&Data.echoes[index].Contains('\n'));
            label1.Text=Data.echoes[index];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Data.dirPath))
            {
                Directory.CreateDirectory(Data.dirPath);
            }
            if (!File.Exists(Data.path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(Data.path))
                {
                    sw.Write("欢迎投稿回声洞");
                }
            }
            SystemImp.LoadEchoes();
            Random random = new Random();
            int index;
            do
            {
                index = random.Next()%(Data.length-1)+1;
            } while (Data.echoes[index].Length > MAX_TEXT_LENGTH && index != 0 && index < Data.length&&Data.echoes[index].Contains('\n'));
            label1.Text=Data.echoes[index];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SystemImp.executeInCmd(@"start C:\Echo\Echoes.txt");
            SystemImp.LoadEchoes();
            Random random = new Random();
            int index;
            do
            {
                index = random.Next()%(Data.length-1)+1;
            } while (Data.echoes[index].Length > MAX_TEXT_LENGTH && index != 0 && index < Data.length&&Data.echoes[index].Contains('\n'));
            label1.Text=Data.echoes[index];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SystemImp.LoadEchoes();
            Random random = new Random();
            int index;
            do
            {
                index = random.Next() % (Data.length - 1) + 1;
            } while (Data.echoes[index].Length > MAX_TEXT_LENGTH && index != 0 && index < Data.length&&Data.echoes[index].Contains('\n'));
            label1.Text = Data.echoes[index];
        }
    }
    class Data
    {
        public static string[] echoes = new string[114];
        public static int length = 1;
        public static string dirPath = @"C:\Echo";
        public static string path = @"C:\Echo\Echoes.txt";
    }
    class SystemImp
    {
        public static string executeInCmd(string cmdline)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.StandardInput.AutoFlush = true;
                process.StandardInput.WriteLine(cmdline + "&exit");

                string output = process.StandardOutput.ReadToEnd();

                process.WaitForExit();
                process.Close();
                return output;
            }
        }
        public static void LoadEchoes()
        {
            StreamReader streamReader = new StreamReader(Data.path);
            Data.length = 1;
            while (!streamReader.EndOfStream)
            {
                Data.echoes[Data.length++] = streamReader.ReadLine();
            }
            streamReader.Close();
        }
    }
}
