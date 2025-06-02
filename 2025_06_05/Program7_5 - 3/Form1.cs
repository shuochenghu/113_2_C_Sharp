using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Program7_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // �x�s�Ҧ��y���W�٪��r��M��
        List<string> team;
        // �x�s�Ҧ��@�ɤj�ɫa�x����W�٪��r��M��
        List<string> winner;

        // �����J�ɰ���AŪ���y���P�a�x���
        private void Form1_Load(object sender, EventArgs e)
        {
            readTeams();
            readWinner();
        }

        // Ū�� Teams.txt �ɮסA�N�y���W�٥[�J listBox1 �æs�J team �M��
        private void readTeams()
        {
            try
            {
                //OpenFileDialog openFileDialog = new OpenFileDialog();
                //openFileDialog.Title = "�п�ܲy������ɮ�";
                //openFileDialog.Filter = "��r�ɮ� (*.txt)|*.txt|�Ҧ��ɮ� (*.*)|*.*";
                //openFileDialog.FileName = "Teams.txt";

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    team = new List<string>();
                    StreamReader inputFile = File.OpenText(openFileDialog.FileName);

                    // �v��Ū���ɮסA�ñN�C�@��]�y���W�١^�[�J listBox1 �� team �M��
                    string line;
                    listBox1.Items.Clear();
                    while ((line = inputFile.ReadLine()) != null)
                    {
                        listBox1.Items.Add(line);
                        team.Add(line);
                    }

                    inputFile.Close();
                }
                else
                {
                    MessageBox.Show("����ܲy������ɮסA�L�k���J�y����ơC", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // �Y�o�ͨҥ~�A��ܿ��~�T���]�c�餤��^
                MessageBox.Show("Ū���y����Ʈɵo�Ϳ��~�G" + ex.Message, "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Ū�� WorldSeries.txt �ɮסA�N�C�~�a�x����W�٦s�J winner �M��
        private void readWinner()
        {
            try
            {
                //OpenFileDialog openFileDialog = new OpenFileDialog();
                //openFileDialog.Title = "�п�ܥ@�ɤj�ɫa�x����ɮ�";
                //openFileDialog.Filter = "��r�ɮ� (*.txt)|*.txt|�Ҧ��ɮ� (*.*)|*.*";
                //openFileDialog.FileName = "WorldSeries.txt";

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    winner = new List<string>();
                    StreamReader inputFile = File.OpenText(openFileDialog.FileName);

                    // �v��Ū���ɮסA�N�C�~�a�x����W�٦s�J winner �M��
                    string line;
                    while ((line = inputFile.ReadLine()) != null)
                    {
                        winner.Add(line);
                    }
                    inputFile.Close();
                }
                else
                {
                    MessageBox.Show("����ܥ@�ɤj�ɫa�x����ɮסA�L�k���J�a�x��ơC", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // �Y�o�ͨҥ~�A��ܿ��~�T���]�c�餤��^
                MessageBox.Show("Ū���a�x��Ʈɵo�Ϳ��~�G" + ex.Message, "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ��ϥΪ̦b listBox1 ������P�y���ɡA�p��Ӳy���ܫa���ƨ���ܩ� label1
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = listBox1.SelectedItem.ToString();
            int numWin = 0;
            // �M�� winner �M��A�έp����y�����ܫa����
            foreach (string w in winner)
            {
                if (str == w)
                {
                    numWin++;
                }
            }
            // ��ܿ���y���� 1903 �~�� 2009 �~���ܫa���ơ]�c�餤��^
            label1.Text = str + " �� 1903 �~�� 2009 �~�@��o�@�ɤj�ɫa�x " + numWin + " ���C";
        }
    }
}
