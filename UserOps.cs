using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OIS.Forms
{
    public partial class UserOp : Form
    {

        DbConnect db = new DbConnect();
        Personeller personel = new Personeller();


        public UserOp()
        {
            InitializeComponent();

        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            TBsicilNo.MaxLength = 5;

            DataGridPersonel.ColumnCount = 4;
            DataGridPersonel.Columns[0].HeaderText = "Sicil No";
            DataGridPersonel.Columns[1].HeaderText = "Adı";
            DataGridPersonel.Columns[2].HeaderText = "Soyadı";
            DataGridPersonel.Columns[3].HeaderText = "Görevi";
            DataGridPersonel.ReadOnly = true;
            for (int i = 0; i < DataGridPersonel.ColumnCount; i++)
            {
                DataGridPersonel.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            LBstateText.Visible = false;
            
        }

        private void BTadd_Click(object sender, EventArgs e)
        {
            personel.sicilNo = TBsicilNo.Text;
            personel.adi = TBname.Text;
            personel.soyadi = TBsurname.Text;
            personel.gorevi = CBduty.Text;
            personel.kullaniciAdi = TBusername.Text;
            personel.sifre = TBpassword.Text;
            personel.yetkinlik = CBaccessLevel.Text;
            String[] row0 = { "denemestring", "2" };

            if (personel.sicilNo == null || personel.adi == null || personel.soyadi == null || personel.gorevi == "" ||
                    personel.kullaniciAdi == null || personel.sifre == null || personel.yetkinlik == "")
            {
                LBstateText.Visible = true;
                LBstateText.ForeColor = danger;
                LBstateText.Text = "Eksik bilgi girilmiştir!";
                
            }
            else
            {
                db.personeller.Add(personel);
                db.SaveChanges();
                
                LBstateText.Visible = true;
                LBstateText.ForeColor = succes;
                LBstateText.Text = "Kayıtlar başarılı bir şekilde eklenmiştir";
            }
        }

        private void BTdelete_Click(object sender, EventArgs e)
        {
            
            bool flag = false;


            if (TBsicilNo.Text == "")
            {
                LBstateText.Text = "Veri silme işleminde sicil No boş olamaz!";
                LBstateText.ForeColor = danger;
                LBstateText.Visible = true;
                return;
            }

            foreach (Personeller data in db.personeller)
            {
                if (data.sicilNo == TBsicilNo.Text)
                {
                    db.personeller.Remove(data);
                    LBstateText.Visible = true;
                    LBstateText.ForeColor = succes;
                    LBstateText.Text = "Kayıt başarılı bir şekilde kaldırıldı";
                    flag = true;
                    break;
                }
            }

            if (flag == false)
            {
                LBstateText.Visible = true;
                LBstateText.ForeColor = danger;
                LBstateText.Text = "Böyle bir kayıt bulunamadı!";
            }
            db.SaveChanges();

        }


        // Currently shows all data from specific table
        private void BTsearch_Click(object sender, EventArgs e)
        {
            DataGridPersonel.Refresh();
            DataGridPersonel.Rows.Clear();

            personel = new Personeller();
            personel.sicilNo = TBsicilNo.Text;
            personel.adi = TBname.Text;
            personel.soyadi = TBsurname.Text;
            personel.gorevi = CBduty.Text;

            if (TBsicilNo.Text != "")
            {
                foreach (Personeller data in db.personeller)
                {
                    if (TBsicilNo.Text == data.sicilNo)
                    {
                        string[] row = { data.sicilNo, data.adi, data.soyadi, data.gorevi };
                        DataGridPersonel.Rows.Add(row);
                    }
                }
                if (DataGridPersonel.Rows.Count == 1)
                {
                    DisplayAlert("Böyle bir kayıt bulunamadı!", danger);
                }
            }
            else if(TBname.Text != "")
            {
                LBstateText.Visible = false;
                foreach (Personeller data in db.personeller)
                {
                    if (string.Equals(TBname.Text, data.adi, StringComparison.OrdinalIgnoreCase))
                    {
                        string[] row = { data.sicilNo, data.adi, data.soyadi, data.gorevi };
                        DataGridPersonel.Rows.Add(row);
                    }
                }
                if (DataGridPersonel.Rows.Count == 1)
                {
                    DisplayAlert("Böyle bir kayıt bulunamadı!", danger);
                }
            }

            else if (TBname.Text != "")
            {
                LBstateText.Visible = false;
                foreach (Personeller data in db.personeller)
                {
                    if (string.Equals(TBname.Text, data.adi, StringComparison.OrdinalIgnoreCase))
                    {
                        string[] row = { data.sicilNo, data.adi, data.soyadi, data.gorevi };
                        DataGridPersonel.Rows.Add(row);
                    }
                }
                if (DataGridPersonel.Rows.Count == 1)
                {
                    DisplayAlert("Böyle bir kayıt bulunamadı!", danger);
                }
            }

            else if (TBsurname.Text != "")
            {
                LBstateText.Visible = false;
                foreach (Personeller data in db.personeller)
                {
                    if (string.Equals(TBsurname.Text, data.soyadi, StringComparison.OrdinalIgnoreCase))
                    {
                        string[] row = { data.sicilNo, data.adi, data.soyadi, data.gorevi };
                        DataGridPersonel.Rows.Add(row);
                    }
                }
                if (DataGridPersonel.Rows.Count == 1)
                {
                    DisplayAlert("Böyle bir kayıt bulunamadı!", danger);
                }
            }

            else
            {
                foreach (Personeller data in db.personeller)
                {
                    string[] row = { data.sicilNo, data.adi, data.soyadi, data.gorevi };
                    DataGridPersonel.Rows.Add(row);
                
                }
            }
        }


        // Private Fields and Properities
        private Color danger = Color.FromArgb(193, 0, 0);
        private Color succes = Color.FromArgb(34, 190, 0);

        // Functions for basic operations
        void DisplayAlert(string message, Color color)
        {
            if (color == null)
            {
                color = Color.Pink;
            }
            LBstateText.Text = message;
            LBstateText.ForeColor = color;
            LBstateText.Visible = true;
        }
        
        void FindDataPersonel(Personeller personel)
        {
            LBstateText.Visible = false;
            
            IQueryable<Personeller> personellist = db.personeller;

            if (personel.sicilNo != null)
            {
                personellist = personellist.Where(w => w.sicilNo== personel.sicilNo);
            }
            if (personel.adi != null)
            {
                personellist = personellist.Where(w => w.adi == personel.adi);
            }
            if (personel.soyadi != null)
            {
                personellist = personellist.Where(w => w.soyadi == personel.soyadi);
            }
            if (personel.gorevi != null)
            {
                personellist = personellist.Where(w => w.gorevi == personel.gorevi);
            }
            

            foreach (Personeller data in personellist)
            {
                if (personel.adi != null && string.Equals(personel.adi, data.adi, StringComparison.OrdinalIgnoreCase))
                {
                    string[] row = { data.sicilNo, data.adi, data.soyadi, data.gorevi };
                    DataGridPersonel.Rows.Add(row);
                }
                if (personel.soyadi != null && string.Equals(personel.soyadi, data.soyadi, StringComparison.OrdinalIgnoreCase))
                {
                    string[] row = { data.sicilNo, data.adi, data.soyadi, data.gorevi };
                    DataGridPersonel.Rows.Add(row);
                }
                if (personel.adi != null && string.Equals(personel.adi, data.adi, StringComparison.OrdinalIgnoreCase))
                {
                    string[] row = { data.sicilNo, data.adi, data.soyadi, data.gorevi };
                    DataGridPersonel.Rows.Add(row);
                }

            }
            if (DataGridPersonel.Rows.Count == 1)
            {
                DisplayAlert("Böyle bir kayıt bulunamadı!", danger);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
