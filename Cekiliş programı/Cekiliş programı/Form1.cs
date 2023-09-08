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

namespace Cekiliş_programı
{
    public partial class Form1 : Form
    {
        private int currentIndex;
        private List<string> katilimcilar = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }
        int rgl = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            timer2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rgl = 0;
            currentIndex = 0;
            Random random = new Random();
            int sayı = random.Next(10, 210);
            rgl = sayı;
            label6.Text = Convert.ToString(rgl);

            if (timer1.Interval >= rgl)
            { timer1.Interval = 10; }
            else
                timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Metin Dosyaları (*.txt)|*.txt|Tüm Dosyalar (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Dosya içeriğini okuyup katilimcilar listesine ekliyoruz
                    katilimcilar.Clear();
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                    {
                        string satir;
                        while ((satir = sr.ReadLine()) != null)
                        {
                            katilimcilar.Add(satir);

                        }
                    }

                    // ListBox'a aktarıyoruz
                    lstKatilimcilar.Items.Clear();
                    listBox1.Items.Clear();
                    foreach (string katilimci in katilimcilar)
                    {
                        lstKatilimcilar.Items.Add(katilimci);
                        listBox1.Items.Add(katilimci);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lstKatilimcilar.Items.Clear();
            listBox1.Items.Clear();
            button1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Interval += 1;
                currentIndex = (currentIndex + 1) % lstKatilimcilar.Items.Count;
                lstKatilimcilar.SelectedIndex = currentIndex;
                label3.Text = timer1.Interval.ToString();
            }
            catch { }
            if (timer1.Interval >= rgl)
            {
                timer1.Enabled = false;
                if (katilimcilar.Count > 0)
                {
                    if (timer1.Enabled == false)
                    {

                        int kazananIndex = lstKatilimcilar.SelectedIndex;
                        string kazananKisi = katilimcilar[kazananIndex];



                        // Çekiliş sonucunu ekrana yazdırma
                        MessageBox.Show($"Kazanılan : {kazananKisi}");



                        // Kazanan kişiyi listeden çıkarma
                        lstcekilen.Items.Add(kazananKisi);


                        katilimcilar.RemoveAt(kazananIndex);


                        // Listeyi güncelleyerek tekrar ListBox'a yazdırma
                        lstKatilimcilar.Items.Clear();
                        foreach (string katilimci in katilimcilar)
                        {
                            lstKatilimcilar.Items.Add(katilimci);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tüm katılımcılar çekiliş yapıldı!");
                }
                timer1.Interval = 10;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (lstKatilimcilar.SelectedIndex != -1)
            {
                lstKatilimcilar.Items.RemoveAt(lstKatilimcilar.SelectedIndex);

            }
            else
            {
                MessageBox.Show("Lütfen bir öğe seçin.");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lstKatilimcilar.Items.Add(textBox1.Text);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (lstKatilimcilar.Items.Count < 0)
            {
                button1.Enabled = false;
                button5.Enabled = false;

            }
            else if (lstKatilimcilar.Items.Count > 0)
            {
                button1.Enabled = true;
                button5.Enabled = true;
            }
        }
    }
    }

