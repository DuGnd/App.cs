using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Chua_De_07.Models;

namespace Chua_De_07
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        QlduocPhamContext db = new QlduocPhamContext();
        private void HienThiBD()
        {
            dgThuoc.ItemsSource = db.Thuocs.Where(x => x.SoLuong <= 200).Select(x => new
            {
                x.MaThuoc,
                x.TenThuoc,
                x.MaDm,
                x.GiaBan,
                x.SoLuong,
                ThanhTien = x.GiaBan*x.SoLuong
            }).ToList();
            //    public ds Where(ArrayList< .. > dsThuoc)
            //{ dgthuoc.Iteamsource = db.thuóc
         
            //    ArrayList < .. > dsms = new Array().Length..;

            //    for (int i = 0;i < dsThuoc.Size(); i++)
            //    {
            //        if (dsThuoc[i].SoLuong <= 200) dsms.Add(..);''
            //    }
            //    return dsms;
            //}
        }

        private void HienThiCB()
        {
            cbbDanhMucThuoc.ItemsSource = db.DanhMucThuocs.Select(x => x).ToList();
            cbbDanhMucThuoc.DisplayMemberPath = "TenDm";
            cbbDanhMucThuoc.SelectedValuePath = "MaDm";
            cbbDanhMucThuoc.SelectedIndex = 0;
        }
        public MainWindow()
        {

        }

        private bool kt()
        {
            if(txtMaThuoc.Text == "")
            {
                MessageBox.Show("Ma thuoc k dc trong a!!!", "Thong bao");
                return false;
            }
            if(txtTenThuoc.Text == "")
            {
                MessageBox.Show("Ten thuoc k dc trong a!!!", "Thong bao");
                return false;
            }
            if (txtGiaBan.Text == "")
            {
                MessageBox.Show("Gia Ban k dc trong a!!!", "Thong bao");
                return false;
            }
            if (!Regex.IsMatch(txtGiaBan.Text, @"\d+"))
            {
                MessageBox.Show("Gia ban phai la so a!!!", "Thong bao");
                return false;
            }
            if(int.Parse(txtGiaBan.Text) <= 0)
            {
                MessageBox.Show("Gia thuoc k dc nho hon 0 a!!!", "Thong bao");
                return false;
            }
            if (txtSoLuong.Text == "")
            {
                MessageBox.Show("SoLuong k dc trong a!!!", "Thong bao");
                return false;
            }
            if (!Regex.IsMatch(txtSoLuong.Text, @"\d+"))
            {
                MessageBox.Show("So luong phai la so a!!!", "Thong bao");
                return false;
            }
            if (int.Parse(txtSoLuong.Text) <= 0)
            {
                MessageBox.Show("SoLuong k dc nho hon 0 a!!!", "Thong bao");
                return false;
            }
            return true;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HienThiBD();
            HienThiCB();
        }

        private void HienThiAll()
        {
            dgThuoc.ItemsSource = db.Thuocs.Select(x => new
            {
                x.MaThuoc,
                x.TenThuoc,
                x.MaDm,
                x.GiaBan,
                x.SoLuong,
                ThanhTien = x.GiaBan * x.SoLuong
            }).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var h = db.Thuocs.SingleOrDefault(x => x.MaThuoc == txtMaThuoc.Text);
            if(h != null)
            {
                MessageBox.Show("Thuoc da co roi a!!!", "Thong bao");
            }
            else
            {
                try
                {
                    if (kt())
                    {
                        Thuoc x = new Thuoc();
                        x.MaThuoc = txtMaThuoc.Text;
                        x.TenThuoc = txtTenThuoc.Text;
                        x.GiaBan = float.Parse(txtGiaBan.Text);
                        x.SoLuong = int.Parse(txtSoLuong.Text);
                        DanhMucThuoc y = (DanhMucThuoc)cbbDanhMucThuoc.SelectedItem;
                        x.MaDm = y.MaDm;
                        db.Thuocs.Add(x);
                        db.SaveChanges();
                        MessageBox.Show("Them thuoc thanh cong a!!!", " Thong bao");
                        HienThiAll();
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var x = db.Thuocs.SingleOrDefault(x => x.MaThuoc == txtMaThuoc.Text);
            if (x == null)
            {
                MessageBox.Show("Thuoc khong co roi a!!!", "Thong bao");
            }
            else
            {
                try
                {
                    if (kt())
                    {
                        x.TenThuoc = txtTenThuoc.Text;
                        x.GiaBan = float.Parse(txtGiaBan.Text);
                        x.SoLuong = int.Parse(txtSoLuong.Text);
                        DanhMucThuoc y = (DanhMucThuoc)cbbDanhMucThuoc.SelectedItem;
                        x.MaDm = y.MaDm;
                        db.SaveChanges();
                        MessageBox.Show("Sua thuoc thanh cong a!!!", " Thong bao");
                        HienThiAll();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var x = db.Thuocs.SingleOrDefault(x => x.MaThuoc == txtMaThuoc.Text);
            if (x == null)
                MessageBox.Show("Thuoc khong co de xoa a!!!", "Thong bao");
            else
            {
                MessageBoxResult rs = MessageBox.Show("Xác nhận xóa không ạ", "Thông báo", MessageBoxButton.YesNo);
                if(rs == MessageBoxResult.Yes)
                {
                    db.Thuocs.Remove(x);
                    db.SaveChanges();
                    MessageBox.Show("Da xoa thanh cong");
                    HienThiAll();
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Manhinh2 x = new Manhinh2();
            x.Show();
        }

        private void dgThuoc_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Type t = dgThuoc.SelectedItem.GetType();
            PropertyInfo[] p = t.GetProperties();
            txtMaThuoc.Text = p[0].GetValue(dgThuoc.SelectedValue).ToString();
            txtTenThuoc.Text = p[1].GetValue(dgThuoc.SelectedValue).ToString();
            cbbDanhMucThuoc.SelectedValue = p[2].GetValue(dgThuoc.SelectedValue).ToString();
            txtGiaBan.Text = p[3].GetValue(dgThuoc.SelectedValue).ToString();
            txtSoLuong.Text = p[4].GetValue(dgThuoc.SelectedValue).ToString();
        }
    }
}
