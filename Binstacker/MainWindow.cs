using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Binstacker
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            ProgDataClass.openFirstFilename = openFileDialog1.FileName;
            label1.Text = ProgDataClass.openFirstFilename;
        }

        private void OpenButton2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            ProgDataClass.openSecondFilename = openFileDialog2.FileName;
            label2.Text = ProgDataClass.openSecondFilename;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            ProgDataClass.saveFilename = saveFileDialog1.FileName;
        }
    }
}
