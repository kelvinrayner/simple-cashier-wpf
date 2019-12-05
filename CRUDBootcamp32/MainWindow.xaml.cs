using CRUDBootcamp32.Context;
using CRUDBootcamp32.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using Microsoft.Office.Interop.Outlook;

namespace CRUDBootcamp32
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyContext myContext = new MyContext();
        List<TransactionItem> traItem = new List<TransactionItem>();
        User userLogin = new User();
        
        public MainWindow(User user)
        {
            InitializeComponent();
            SupplierGrid.ItemsSource = myContext.Suppliers.ToList();
            SupplierList.ItemsSource = myContext.Suppliers.ToList();
            ItemGrid.ItemsSource = myContext.Items.ToList();
            ItemShopGrid.ItemsSource = myContext.Items.ToList();
            CartGrid.ItemsSource = traItem.ToList();
            TransactionsGrid.ItemsSource = myContext.TransactionItems.Include("Transaction").ToList();
            RoleGrid.ItemsSource = myContext.Roles.ToList();
            RoleList.ItemsSource = myContext.Roles.ToList();
            UserGrid.ItemsSource = myContext.Users.ToList();
            this.userLogin = user;

            if (user.Role.Id != 1)
            {
                tabRegister.Visibility = Visibility.Hidden;
                tabRole.Visibility = Visibility.Hidden;
                RegisterDataGrid.Visibility = Visibility.Hidden;
                RoleDataGrid.Visibility = Visibility.Hidden;
                TxtIdUserChangePassword.Text = user.Id.ToString();
                TxtNameUserChangePassword.Text = user.Name;
                TxtEmailUserChangePassword.Text = user.Email;
            }
            if (user.Role.Id != 1 && user.Role.Id != 2)
            {
                tabSupplier.Visibility = Visibility.Hidden;
                tabItem.Visibility = Visibility.Hidden;
                SupplierDataGrid.Visibility = Visibility.Hidden;
                ItemDataGrid.Visibility = Visibility.Hidden;
                TxtIdUserChangePassword.Text = user.Id.ToString();
                TxtNameUserChangePassword.Text = user.Name;
                TxtEmailUserChangePassword.Text = user.Email;
            }
        }

        #region Supplier

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string strName = TxtName.Text;
            string strEmail = TxtEmail.Text;

            if (!String.IsNullOrEmpty(TxtName.Text) && !String.IsNullOrEmpty(TxtEmail.Text))
            {
                if (myContext.Suppliers.Any(o => o.Email == TxtEmail.Text))
                {
                    MessageBox.Show(TxtEmail.Text + " has been registered");
                }
                else
                {
                    if(TxtId.Text == "")
                    {
                        var push = new Supplier(strName, strEmail);
                        myContext.Suppliers.Add(push);
                        var result = myContext.SaveChanges();

                        if (result > 0)
                        {
                            MessageBox.Show(result + " row has been inserted " + strName + ' ' + strEmail);
                            TxtId.Text = "";
                            TxtName.Text = "";
                            TxtEmail.Text = "";
                            SupplierGrid.ItemsSource = myContext.Suppliers.ToList();
                            SupplierList.ItemsSource = myContext.Suppliers.ToList();
                        }
                    }
                    else
                    {
                        var conId = Int32.Parse(TxtId.Text);
                        var update = myContext.Suppliers.Where(o => o.Id == conId).First();

                        if (update != null)
                        {
                            update.Name = TxtName.Text;
                            update.Email = TxtEmail.Text;
                            var modify = myContext.SaveChanges();

                            if (modify > 0)
                            {
                                MessageBox.Show(modify + " row has been update to " + strName + ' ' + strEmail);
                                TxtId.Text = "";
                                TxtName.Text = "";
                                TxtEmail.Text = "";
                                SupplierGrid.ItemsSource = myContext.Suppliers.ToList();
                                SupplierList.ItemsSource = myContext.Suppliers.ToList();
                            }
                        }
                    }
                }           
            }
            else if (String.IsNullOrEmpty(TxtName.Text))
            {
                MessageBox.Show("Name must be filled");
            }
            else if (String.IsNullOrEmpty(TxtEmail.Text))
            {
                MessageBox.Show("Email must be filled");
            }
                
        }
    

        private void TxtId_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void TxtName_TextChanged(object sender, TextChangedEventArgs e)
        {


        }
        

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = SupplierGrid.SelectedItem;
            string id = (SupplierGrid.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
            TxtId.Text = id;
            string name = (SupplierGrid.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
            TxtName.Text = name;
            string email = (SupplierGrid.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
            TxtEmail.Text = email;
        }

        private void TxtEmail_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            
        }

        private void TxtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var convId = Int32.Parse(TxtId.Text);
            var dataDel = myContext.Suppliers.Where(o => o.Id == convId).First();

            if (dataDel != null)
            {
                myContext.Suppliers.Remove(dataDel);
                var delete = myContext.SaveChanges();

                if (delete > 0)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
                    if(messageBoxResult == MessageBoxResult.Yes)
                    {
                        MessageBox.Show(delete + " row has been deleted");
                        try
                        {
                            SupplierGrid.ItemsSource = myContext.Suppliers.ToList();
                            TxtId.Text = "";
                            TxtName.Text = "";
                            TxtEmail.Text = "";
                        }
                        catch
                        {
                            TxtId.Text = "";
                            TxtName.Text = "";
                            TxtEmail.Text = "";
                        }
                    }
                    else
                    {
                        TxtId.Text = "";
                        TxtName.Text = "";
                        TxtEmail.Text = "";
                    }
                    
                }
            }
        }

        #endregion

        #region Item

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        
        private void BtnSubmitItem_Click(object sender, RoutedEventArgs e)
        {
            string strNameItem = TxtNameItem.Text;
            string strStockItem = TxtStockItem.Text;
            int stock = int.Parse(strStockItem);
            string strPriceItem = TxtPriceItem.Text;
            int price = int.Parse(strPriceItem);
            var selectedItem = SupplierList.SelectedValue;
            int idSupplier = Convert.ToInt32(selectedItem);
            var getData = myContext.Suppliers.Where(o => o.Id == idSupplier).First();
            

            if (!String.IsNullOrEmpty(TxtNameItem.Text) && !String.IsNullOrEmpty(TxtStockItem.Text) && !String.IsNullOrEmpty(TxtPriceItem.Text))
            {
                if (TxtIdItem.Text == "")
                {
                    if (myContext.Items.Any(o => o.Name == TxtNameItem.Text))
                    {
                        MessageBox.Show(TxtNameItem.Text + " has been registered");
                    }
                    else
                    {
                        var pushItem = new Item(strNameItem, stock, price, getData);
                        myContext.Items.Add(pushItem);
                        var resultItem = myContext.SaveChanges();

                        if (resultItem > 0)
                        {
                            MessageBox.Show(strNameItem + " has been inserted");
                            TxtIdItem.Text = "";
                            TxtNameItem.Text = "";
                            TxtStockItem.Text = "";
                            TxtPriceItem.Text = "";
                            ItemGrid.ItemsSource = myContext.Items.ToList();
                            ItemShopGrid.ItemsSource = myContext.Items.ToList();
                        }
                    }
                }
               else
                {
                    var convId = Int32.Parse(TxtIdItem.Text);
                    var updateItem = myContext.Items.Where(o => o.Id == convId).First();

                    if (updateItem != null)
                    {
                        int newStock = Convert.ToInt32(TxtStockItem.Text);
                        int newPrice = Convert.ToInt32(TxtPriceItem.Text);
                        updateItem.Name = TxtNameItem.Text;
                        updateItem.Stock = newStock;
                        updateItem.Price = newPrice;
                        updateItem.Supplier = getData;

                        var modifyItem = myContext.SaveChanges();

                        if (modifyItem > 0)
                        {
                            MessageBox.Show(modifyItem + " row has been update to " + TxtIdItem.Text + ' ' + TxtNameItem.Text + ' ' + TxtStockItem.Text + ' ' + TxtPriceItem.Text);
                            TxtIdItem.Text = "";
                            TxtNameItem.Text = "";
                            TxtStockItem.Text = "";
                            TxtPriceItem.Text = "";
                            ItemGrid.ItemsSource = myContext.Items.ToList();
                            ItemShopGrid.ItemsSource = myContext.Items.ToList();
                        }
                    }
                }
            }
            else if (String.IsNullOrEmpty(TxtNameItem.Text))
            {
                MessageBox.Show("Name must be filled");
            }
            else if (String.IsNullOrEmpty(TxtStockItem.Text))
            {
                MessageBox.Show("Stock must be filled");
            }
            else if (String.IsNullOrEmpty(TxtPriceItem.Text))
            {
                MessageBox.Show("Price must be filled");
            }
            
        }

        private void ItemGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = ItemGrid.SelectedItem;
            string id = (ItemGrid.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
            TxtIdItem.Text = id;
            string name = (ItemGrid.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
            TxtNameItem.Text = name;
            string stock = (ItemGrid.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
            TxtStockItem.Text = stock;
            string price = (ItemGrid.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
            TxtPriceItem.Text = price;
            
        }

        private void TxtNumberItem_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtNameItem_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[a-zA-Z!]"))
            {
                e.Handled = true;
            }
        }

        private void BtnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            var convId = Int32.Parse(TxtIdItem.Text);
            var dataDel = myContext.Items.Where(o => o.Id == convId).First();

            if (dataDel != null)
            {

                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    myContext.Items.Remove(dataDel);
                    var delete = myContext.SaveChanges();

                    if (delete > 0)
                    {
                        MessageBox.Show(delete + " row has been deleted");
                        try
                        {
                            ItemGrid.ItemsSource = myContext.Items.ToList();
                            TxtIdItem.Text = "";
                            TxtNameItem.Text = "";
                            TxtStockItem.Text = "";
                            TxtPriceItem.Text = "";
                        }
                        catch
                        {
                            TxtIdItem.Text = "";
                            TxtNameItem.Text = "";
                            TxtStockItem.Text = "";
                            TxtPriceItem.Text = "";
                        }
                        
                    }
                }
                else
                {
                    ItemGrid.ItemsSource = myContext.Items.ToList();
                    TxtIdItem.Text = "";
                    TxtNameItem.Text = "";
                    TxtStockItem.Text = "";
                    TxtPriceItem.Text = "";
                }
                
            }
        }
        #endregion

        #region Shopping

        private void ItemShopGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = ItemShopGrid.SelectedItem;
            string id = (ItemShopGrid.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
            string name = (ItemShopGrid.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
            string stock = (ItemShopGrid.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
            string price = (ItemShopGrid.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
        }
        #endregion

        #region Cart
        private void BtnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            var data = ItemShopGrid.SelectedItem;
            int sum = 0;
            string id = (ItemShopGrid.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
            int idItem = Convert.ToInt32(id);
            string name = (ItemShopGrid.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
            string stockString = (ItemShopGrid.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
            int stock = Convert.ToInt32(stockString);
            string priceString = (ItemShopGrid.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
            int price = Convert.ToInt32(priceString);
            var getItem = myContext.Items.Where(o => o.Id == idItem).First();
            string qtyString = TxtJumlahShop.Text;
            int qty = Convert.ToInt32(qtyString);

            if (qty > stock)
            {
                MessageBox.Show("Stock item " + name + " kurang dari " + qty);
            }
            else
            {
                MessageBox.Show("Item " + name + " telah dimasukkan ke dalam cart");

                traItem.Add(new TransactionItem() { Item = getItem, Qty = qty, TotalPrice = qty * price });
                foreach (TransactionItem t in traItem)
                {
                    sum += t.TotalPrice;
                }

                TxtJumlahDibayar.Text = sum.ToString();
                CartGrid.ItemsSource = traItem.ToList();
            }
        }
        
        private void CartGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = CartGrid.SelectedItem;
            string name = (CartGrid.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
            string qty = (CartGrid.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
            string price = (CartGrid.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
            string total = (CartGrid.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
        }
        

        private void BtnCheckout_Click(object sender, RoutedEventArgs e)
        {
            string jumlahDibayar = TxtJumlahDibayar.Text;
            int totalPrice = Convert.ToInt32(jumlahDibayar);
            string bayar = TxtJumlahPembayaran.Text;
            int jumlahPembayaran = Convert.ToInt32(bayar);
            int kembalian = jumlahPembayaran - totalPrice;
            
            if (jumlahPembayaran < totalPrice)
            {
                MessageBox.Show("Uang yg dibayar kurang!");

            }
            else
            {
                var pushTransaction = new Transaction(jumlahPembayaran, DateTime.Now);
                myContext.Transactions.Add(pushTransaction);
                var resultTransaction = myContext.SaveChanges();
                string transacId = pushTransaction.Id.ToString();
                int transactionId = Convert.ToInt32(transacId);

                foreach (var result in traItem)
                {
                    string transId = pushTransaction.Id.ToString();
                    int transIdPush = Convert.ToInt32(transId);
                    var getDataTransaction = myContext.Transactions.Where(o => o.Id == transIdPush).First();
                    string itemId = result.Item.Id.ToString();
                    int itemIdPush = Convert.ToInt32(itemId);
                    var getDataItem = myContext.Items.Where(o => o.Id == itemIdPush).First();
                    string qtyPush = result.Qty.ToString();
                    int qtyToPush = Convert.ToInt32(qtyPush);
                    string totalPricePush = result.TotalPrice.ToString();
                    int totalPriceToPush = Convert.ToInt32(totalPrice);

                    var pushTransactionItem = new TransactionItem(qtyToPush, totalPriceToPush, getDataTransaction, getDataItem);
                    myContext.TransactionItems.Add(pushTransactionItem);
                    var resultTransactionItem = myContext.SaveChanges();

                    var stockItem = myContext.Items.Where(o => o.Id == itemIdPush).Select(o => o.Stock).First();
                    int afterStock = stockItem - qtyToPush;
                    var updateItem = myContext.Items.Where(o => o.Id == itemIdPush).First();
                    updateItem.Stock = afterStock;
                    var modifyItem = myContext.SaveChanges();

                    if (resultTransactionItem > 0 && modifyItem > 0)
                    {
                        MessageBox.Show(resultTransactionItem + " has been inserted and " + modifyItem + " item stock has been updated");
                    }
                    else
                    {
                        MessageBox.Show("Fail to insert");
                    }

                    //MessageBox.Show(transId + '|' + itemId + '|' + qtyPush + '|' + totalPricePush);
                }
                MessageBox.Show("Kembalian : Rp " + kembalian + ",-");
                MessageBoxResult messageBoxResult = MessageBox.Show("Want to print payment invoice?", "Print Confirmation", MessageBoxButton.YesNo);
                TransactionsGrid.ItemsSource = myContext.TransactionItems.ToList();
                ItemShopGrid.ItemsSource = myContext.Items.ToList();
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    int dockey = Convert.ToInt32(transactionId);
                    FormReportcs formReportcs = new FormReportcs(dockey);
                    formReportcs.Show();
                }
                else
                {
                    MessageBox.Show("Fail to load payment invoice!");
                }
            }
        }
        #endregion

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            int dockey = Convert.ToInt32(TxtPrint.Text);
            FormReportcs formReportcs = new FormReportcs(dockey);
            formReportcs.Show();
        }

        #region Role
        private void RoleGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = RoleGrid.SelectedItem;
            string id = (RoleGrid.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
            TxtIdRole.Text = id;
            string name = (RoleGrid.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
            TxtNameRole.Text = name;
        }
        
        private void BtnSubmitRole_Click(object sender, RoutedEventArgs e)
        {
            string strName = TxtNameRole.Text;

            if (!String.IsNullOrEmpty(TxtNameRole.Text))
            {
                if (myContext.Roles.Any(o => o.Name == TxtNameRole.Text))
                {
                    MessageBox.Show(TxtNameRole.Text + " has been registered");
                }
                else
                {
                    if (TxtIdRole.Text == "")
                    {
                        var push = new Role(strName);
                        myContext.Roles.Add(push);
                        var result = myContext.SaveChanges();

                        if (result > 0)
                        {
                            MessageBox.Show(result + " row has been inserted " + strName);
                            TxtNameRole.Text = "";
                            RoleGrid.ItemsSource = myContext.Roles.ToList();
                            RoleList.ItemsSource = myContext.Roles.ToList();
                        }
                    }
                    else
                    {
                        var conId = Int32.Parse(TxtIdRole.Text);
                        var update = myContext.Roles.Where(o => o.Id == conId).First();

                        if (update != null)
                        {
                            update.Name = TxtNameRole.Text;
                            var modify = myContext.SaveChanges();

                            if (modify > 0)
                            {
                                MessageBox.Show(modify + " row has been update to " + TxtNameRole.Text);
                                
                                TxtNameRole.Text = "";
                                RoleGrid.ItemsSource = myContext.Roles.ToList();
                                RoleList.ItemsSource = myContext.Roles.ToList();
                            }
                        }
                    }
                }

            }
            else if (String.IsNullOrEmpty(TxtNameRole.Text))
            {
                MessageBox.Show("Role Name must be filled!");
            }

        }
        #endregion

        #region Register
        private void UserGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = UserGrid.SelectedItem;
            string id = (UserGrid.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
            TxtIdUser.Text = id;
            string name = (UserGrid.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
            TxtNameUser.Text = name;
            string email = (UserGrid.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
            TxtEmailUser.Text = email;
            string password = (UserGrid.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
            TxtPasswordUser.Text = password;
        }

        private void BtnSubmitUser_Click(object sender, RoutedEventArgs e)
        {
            string nameUser = TxtNameUser.Text;
            string emailUser = TxtEmailUser.Text;
            var selectedItem = RoleList.SelectedValue;
            int idRole = Convert.ToInt32(selectedItem);
            var getData = myContext.Roles.Where(o => o.Id == idRole).First();
            Guid guid = Guid.NewGuid();
            string password = guid.ToString();

            if (!String.IsNullOrEmpty(TxtNameUser.Text) && !String.IsNullOrEmpty(TxtEmailUser.Text))
            {
                if (TxtIdUser.Text == "")
                {
                    if (myContext.Users.Any(o => o.Email == TxtEmailUser.Text))
                    {
                        MessageBox.Show(TxtEmailUser.Text + " has been registered");
                    }
                    else
                    {
                        var push = new User(nameUser, emailUser, password, DateTime.Now, getData);
                        myContext.Users.Add(push);
                        var result = myContext.SaveChanges();

                        if (result > 0)
                        {
                            MessageBox.Show(result + " row has been inserted " + nameUser + ' ' + emailUser);
                            TxtIdUser.Text = "";
                            TxtNameUser.Text = "";
                            TxtEmailUser.Text = "";
                            UserGrid.ItemsSource = myContext.Users.ToList();
                            CreateEmailSentGP(nameUser, emailUser, password);
                            MessageBox.Show("Your password has been sent to " + emailUser);
                        }
                    }
                }
                else
                {
                    var conId = Int32.Parse(TxtIdUser.Text);
                    var update = myContext.Users.Where(o => o.Id == conId).First();
                    
                    if (update != null)
                    {
                        update.Name = TxtNameUser.Text;
                        update.Email = TxtEmailUser.Text;
                        update.Password = TxtPasswordUser.Text;
                        update.Role = getData;
                        var modify = myContext.SaveChanges();

                        if (modify > 0)
                        {
                            MessageBox.Show(modify + " row has been update to " + TxtNameUser.Text + ' ' + TxtEmailUser.Text);
                            TxtIdUser.Text = "";
                            TxtNameUser.Text = "";
                            TxtEmailUser.Text = "";
                            UserGrid.ItemsSource = myContext.Users.ToList();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Gagal");
                    }
                }
            }
            else if(String.IsNullOrEmpty(TxtNameUser.Text))
            {
                MessageBox.Show("Name must be fill!");
            }
            else if(String.IsNullOrEmpty(TxtEmailUser.Text))
            {
                MessageBox.Show("Email must be fill!");
            }

        }

        private void BtnChangePasswordUser_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(TxtNameUser.Text))
            {
                Guid guid = Guid.NewGuid();
                string password = guid.ToString();
                var conId = Int32.Parse(TxtIdUser.Text);
                var update = myContext.Users.Where(o => o.Id == conId).First();
                var email = myContext.Users.Where(o => o.Id == conId).Select(o => o.Email).First();
                var name = myContext.Users.Where(o => o.Id == conId).Select(o => o.Name).First();

                if (update != null)
                {
                    update.Password = password;
                    var modify = myContext.SaveChanges();

                    if (modify > 0)
                    {
                        MessageBox.Show("Your new pasword has been changed to " + password);
                        CreateEmailSentNewPassword(name, email, password);
                        MessageBox.Show("Your new pasword has been sent to " + email);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih salah satu user");
            }
        }
        #endregion

        private void CreateEmailSentGP(String Name, String email, String password)
        {
            Microsoft.Office.Interop.Outlook.Application application = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.MailItem mailItem = application.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
            mailItem.Subject = "Generate Password sent to email exercise";
            mailItem.To = email;
            mailItem.Body = "Succesfully generate your password! Your generate password : " + password + ". Please login with username " + email + " and new password " + password;
            mailItem.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceHigh;
            mailItem.Display(false);
        }

        private void CreateEmailSentNewPassword(String Name, String email, String password)
        {
            Microsoft.Office.Interop.Outlook.Application application = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.MailItem mailItem = application.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
            mailItem.Subject = "Generate New Change Password sent to email exercise";
            mailItem.To = email;
            mailItem.Body = "Succesfully change your password! Your new password : " + password + ". Please login with username " + email + " and new password " + password;
            mailItem.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceHigh;
            mailItem.Display(false);
        }

        #region Change Password
        private void BtnUserChangePassword_Click(object sender, RoutedEventArgs e)
        {
            TxtIdUserChangePassword.Text = userLogin.Id.ToString();
            TxtNameUserChangePassword.Text = userLogin.Name;
            TxtEmailUserChangePassword.Text = userLogin.Email;
            string lastPassword = PbxLastPasswordChangePassword.Password;
            string newPassword = Guid.NewGuid().ToString();
            var match = myContext.Users.FirstOrDefault(u => u.Password == lastPassword);
            var conId = Int32.Parse(TxtIdUserChangePassword.Text);
            var email = myContext.Users.Where(o => o.Id == conId).Select(o => o.Email).First();
            var name = myContext.Users.Where(o => o.Id == conId).Select(o => o.Name).First();

            if (match != null)
            {
                var update = myContext.Users.Where(o => o.Id == conId).First();

                if (update != null)
                {
                    update.Password = newPassword;
                    var modify = myContext.SaveChanges();

                    if (modify > 0)
                    {
                        MessageBox.Show("Your new pasword has been changed to " + newPassword);
                        CreateEmailSentNewPassword(name, email, newPassword);
                        MessageBox.Show("Your new pasword has been sent to " + email);
                        TxtIdUserChangePassword.Text = userLogin.Id.ToString();
                        TxtNameUserChangePassword.Text = userLogin.Name;
                        TxtEmailUserChangePassword.Text = userLogin.Email;
                        PbxLastPasswordChangePassword.Password = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Last password not match to current password | match : " + match);
            }
        }
        #endregion
    }
}
