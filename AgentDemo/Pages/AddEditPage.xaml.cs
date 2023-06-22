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
using AgentDemo.Pages;
using AgentDemo.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.Win32;
using System.Diagnostics;

namespace AgentDemo.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        Agent context;
        public AddEditPage(Agent agent)
        {
            InitializeComponent();
           
            context = agent;
            DataContext = context;
            TypeCb.ItemsSource = App.db.AgentType.ToList();
            TypeCb.DisplayMemberPath = "Title";
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

            string count = context.Priority.ToString();
            try
            {

                if (count.Length == 0)
                {
                    MessageBox.Show("Пусто. Пожалуйста, заполните количество товара!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(context.Title))
                {
                    MessageBox.Show("Пожалуйста заполните поле наименования", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (context.AgentType == null)
                {
                    MessageBox.Show("Пожалуйста выберите тип агента", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (string.IsNullOrEmpty(context.Address))
                {
                    MessageBox.Show("Пожалуйста заполните поле адреса", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (string.IsNullOrEmpty(context.INN))
                {
                    MessageBox.Show("Пожалуйста заполните поле ИНН", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (string.IsNullOrEmpty(context.KPP))
                {
                    MessageBox.Show("Пожалуйста заполните поле КПП", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (string.IsNullOrEmpty(context.DirectorName))
                {
                    MessageBox.Show("Пожалуйста заполните поле наименование директора", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (string.IsNullOrEmpty(context.Phone))
                {
                    MessageBox.Show("Пожалуйста заполните поле номера телефона", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (string.IsNullOrEmpty(context.Email))
                {
                    MessageBox.Show("Пожалуйста заполните поле электронной почты", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                
                if (context.ID == 0)
                {
                    App.db.Agent.Add(context);
                }
                App.db.SaveChanges();
                MessageBox.Show("Успешно сохранено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Произошла ошибка при сохранении: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
            if(openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                context.Logo = imagePath;
                BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                AgentImage.Source = bitmap;
            }
        }

        private void NameTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void AddressTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void INNTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void KPPTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void DirectorTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void PhoneTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void EmailTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void PriorityTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
