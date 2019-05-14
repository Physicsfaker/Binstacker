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

        #region Buttons
        private void AddButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnDlg = new OpenFileDialog
            {
                Multiselect = true,
                Title = "Выберите файлы",
                //InitialDirectory = @"D:\BinImagTest\", //закомент
                Filter = "Text files(*.bin)| *.bin"
            };
            if (opnDlg.ShowDialog() == DialogResult.Cancel) return;

            listBox1.Items.AddRange(opnDlg.FileNames);
        }

        private void buttonUp_Click(object sender, EventArgs e) //кнопка вверх
        {
            if (listBox1.Items.Count == 0) return;
            if (listBox1.SelectedIndex == -1) { listBox1.SelectedIndex = listBox1.Items.Count - 1; return; }
            if (listBox1.SelectedIndex != 0)
            {
                string bufer = listBox1.SelectedItem.ToString();
                listBox1.Items[listBox1.SelectedIndex] = listBox1.Items[listBox1.SelectedIndex - 1];
                listBox1.Items[listBox1.SelectedIndex - 1] = bufer;
                listBox1.SelectedIndex--;
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)   //кнопка вниз
        {
            if (listBox1.Items.Count == 0) return;
            if (listBox1.SelectedIndex == -1) { listBox1.SelectedIndex = 0; return; }
            if (listBox1.SelectedIndex != listBox1.Items.Count - 1)
            {
                string bufer = listBox1.SelectedItem.ToString();
                listBox1.Items[listBox1.SelectedIndex] = listBox1.Items[listBox1.SelectedIndex + 1];
                listBox1.Items[listBox1.SelectedIndex + 1] = bufer;
                listBox1.SelectedIndex++;
            }
        }

        private void removeButton_Click(object sender, EventArgs e) //кнопка удалить
        {
            if (listBox1.Items.Count == 0) return;
            if (listBox1.SelectedIndex == -1) { listBox1.SelectedIndex = 0; return; }
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Gluing();
        }
        #endregion

        private void Gluing()
        {
            if (listBox1.Items.Count <= 1) { MessageBox.Show("At least 2 files for gluing"); return; }

            SaveFileDialog savDlg = new SaveFileDialog
            {
                Title = "new_file",
                //InitialDirectory = @"D:\BinImagTest\",  //нужен комент
                Filter = "Text files(*.bin)| *.bin"
            };
            if (savDlg.ShowDialog() == DialogResult.Cancel) return;

            SaveButton.Enabled = false;
            progressBar1.Step = 100 / listBox1.Items.Count;

            using (BinaryWriter exportFile = new BinaryWriter(File.Open(savDlg.FileName, FileMode.OpenOrCreate)))
            {
                int count = 0;
                foreach (var listBoxItem in listBox1.Items)
                {                  
                    using (BinaryReader reader = new BinaryReader(File.Open(listBoxItem.ToString(), FileMode.Open), Encoding.ASCII))
                    {
                        if (count > 0 && checkBox1.Checked)
                            reader.ReadBytes(8);
                        // пока не достигнут конец файла
                        // считываем каждое значение из файла
                        while (reader.PeekChar() > -1)
                        {
                            exportFile.Write(reader.ReadByte());
                        }
                        progressBar1.PerformStep();
                        count++;
                    }
                }
            }
            progressBar1.Value = 100;
            MessageBox.Show("Complite!");
            SaveButton.Enabled = true;
            progressBar1.Value = 0;
        }

        #region Zametki
        //8 байта это шапка бинарника
        #endregion

    }
}
