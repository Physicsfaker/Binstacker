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

        private void AddButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnDlg = new OpenFileDialog
            {
                Multiselect = true,
                Title = "Выберите файлы",
                InitialDirectory = @"D:\BinImagTest\", //закомент
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

        private void Gluing()
        {
            if (listBox1.Items.Count <= 1) { MessageBox.Show("At least 2 files for gluing"); return; }

            SaveFileDialog savDlg = new SaveFileDialog
            {
                Title = "new_file",
                InitialDirectory = @"D:\BinImagTest\",  //нужен комент
                Filter = "Text files(*.bin)| *.bin"
            };

            if (savDlg.ShowDialog() == DialogResult.Cancel) return;

            using (BinaryWriter exportFile = new BinaryWriter(File.Open(savDlg.FileName, FileMode.OpenOrCreate)))
            {

                foreach (var listBoxItem in listBox1.Items)
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(listBoxItem.ToString(), FileMode.Open), Encoding.ASCII))
                    {
                        // пока не достигнут конец файла
                        // считываем каждое значение из файла
                        while (reader.PeekChar() > -1)
                        {
                            exportFile.Write(reader.ReadByte());
                        }
                    }
                }
            }
            MessageBox.Show("Complite!");
        }

        #region Zametki
        //4 байта это шапка
        #endregion

    }
}
