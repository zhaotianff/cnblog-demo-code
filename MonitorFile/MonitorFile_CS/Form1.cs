using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorFile_CS
{
    public partial class Form1 : Form
    {
        FileSystemWatcher systemWatcher = new FileSystemWatcher();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = folderBrowserDialog.SelectedPath;
            }
        }



        private void ShowMsg(string msg)
        {
            this.listBox1.Invoke(new Action(() => {
                this.listBox1.Items.Add(msg);
            }));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            systemWatcher.Path = this.textBox1.Text;

            //设置监听的行为
            //这里设置为文件名
            systemWatcher.NotifyFilter = NotifyFilters.FileName;

            //设置文件类型过滤
            systemWatcher.Filter = "*.txt";

            systemWatcher.Changed += (obj, args) => { ShowMsg($"文件更改{args.Name}"); };
            //systemWatcher.Changed += SystemWatcher_Changed;  //上面是演示代码，为了方便。正常使用时应该创建函数，而不是直接使用匿名函数
            systemWatcher.Created += (obj, args) => { ShowMsg($"文件创建{args.Name}"); };
            systemWatcher.Deleted += (obj, args) => { ShowMsg($"文件删除{args.Name}"); };
            systemWatcher.Renamed += (obj, args) => { ShowMsg($"文件重命名{args.Name}"); };

            //开始监听
            systemWatcher.EnableRaisingEvents = true;
        }

        private void SystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            //处理逻辑
        }
    }
}
