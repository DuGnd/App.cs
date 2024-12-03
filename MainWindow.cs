using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace relsta
{
    public partial class MainWindow : Window
    {
        private List<Customer> customers = new List<Customer>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btNhap_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate fields
                if (string.IsNullOrWhiteSpace(tbmakhachhang.Text) ||
                    dpngaymua.SelectedDate == null ||
                    string.IsNullOrWhiteSpace(tbsoluongmua.Text) ||
                    string.IsNullOrWhiteSpace(tbdongia.Text))
                {
                    MessageBox.Show("Tất cả các trường dữ liệu phải được nhập.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Validate customer code uniqueness
                string maKhachHang = tbmakhachhang.Text;
                if (customers.Any(c => c.MaKhachHang == maKhachHang))
                {
                    MessageBox.Show("Mã khách hàng không được trùng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Validate số lượng mua
                if (!decimal.TryParse(tbsoluongmua.Text, out decimal soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Số lượng mua phải là số thực lớn hơn 0.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Validate đơn giá
                if (!decimal.TryParse(tbdongia.Text, out decimal donGia) || donGia <= 0)
                {
                    MessageBox.Show("Đơn giá phải là số thực lớn hơn 0.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Add customer to list
                DateTime ngayMua = dpngaymua.SelectedDate.Value;
                decimal thanhTien = soLuong * donGia;

                customers.Add(new Customer
                {
                    MaKhachHang = maKhachHang,
                    NgayMua = ngayMua,
                    SoLuongMua = soLuong,
                    DonGia = donGia,
                    ThanhTien = thanhTien
                });

                // Update DataGrid
                UpdateDataGrid();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateDataGrid()
        {
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = customers;
        }

        private void ClearInputFields()
        {
            tbmakhachhang.Clear();
            dpngaymua.SelectedDate = null;
            tbsoluongmua.Clear();
            tbdongia.Clear();
        }

        private void btWindow2_Click(object sender, RoutedEventArgs e)
        {
            if (customers.Count == 0)
            {
                MessageBox.Show("Danh sách khách hàng trống.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Open Window2 and pass the first customer
            Window2 window2 = new Window2(customers[0]);
            window2.Show();
        }
    }

    public class Customer
    {
        public string MaKhachHang { get; set; }
        public DateTime NgayMua { get; set; }
        public decimal SoLuongMua { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
