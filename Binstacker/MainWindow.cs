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
            if (openFileDialog2.ShowDialog() == DialogResult.Cancel) return;
            ProgDataClass.openSecondFilename = openFileDialog2.FileName;
            label2.Text = ProgDataClass.openSecondFilename;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            // ProgDataClass.saveFilename = saveFileDialog1.FileName;
            Gluing();
        }

        private void Gluing()
        {
            string path = @"D:\BinImagTest\Test1.bin";

            using (BinaryWriter exportFile = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                using (BinaryReader reader1 = new BinaryReader(File.Open(@"D:\BinImagTest\ic1.bin", FileMode.Open), Encoding.ASCII))
                {
                    // пока не достигнут конец файла
                    // считываем каждое значение из файла
                    while (reader1.PeekChar() > -1)
                    {
                        exportFile.Write(reader1.ReadByte());
                    }
                }

                using (BinaryReader reader2 = new BinaryReader(File.Open(@"D:\BinImagTest\ic2.bin", FileMode.Open), Encoding.ASCII))
                {
                    // пока не достигнут конец файла
                    // считываем каждое значение из файла
                    while (reader2.PeekChar() > -1)
                    {
                        exportFile.Write(reader2.ReadByte());
                    }
                }
                

                //// записываем в файл значение каждого поля структуры
                //exportFile.Write(s.name);
            }
            MessageBox.Show("Complite!");
        }

        #region Zametki
        //4 байта это шапка
        #endregion

    }
}
