using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace Potekhina_katalog
{
    /// <summary>
    /// Логика взаимодействия для ADDMemes.xaml
    /// </summary>
    public partial class ADDMemes : Window
    {

        public event EventHandler<Meme> FileCreated;
        string selectedFileName;
        string category;
        string memeTitle;
        string url;
        public ADDMemes()
        {
            InitializeComponent();
        }

        private void ChooseFileClick(object sender, RoutedEventArgs e)                                                  
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.png)|*.jpg;*.png;*.gif|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedFileName = openFileDialog.FileName;
            }
        }




        private void CategorySelected(object sender, SelectionChangedEventArgs e) 
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)categories_filter.SelectedItem;
            var comboBoxItemContent = comboBoxItem.Content as Label;
            category = (string)comboBoxItemContent.Content;
        }

        private void memeTitle_changed(object sender, TextChangedEventArgs e) 
        {
            memeTitle = title.Text;
        }

        private void memeUrl_Changed(object sender, TextChangedEventArgs e) 
        {
            url = meme_url.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)                                                 
        {
            {
                if (memeTitle == null)
                {
                    MessageBox.Show("Введите название файла");
                }
                else if (category == null)
                {
                    MessageBox.Show("Выберите категорию");
                }
                else if (selectedFileName == null && url == null)
                {
                    MessageBox.Show("Нет пути к Вашему мему");
                }
                else
                {
                    if (category == "PROGRAMMER")
                    {
                        var filePath = selectedFileName == null ? url : selectedFileName;
                        Progr prg = new Progr(memeTitle, category, filePath);
                        FileCreated.Invoke(this, prg);
                    }
                    else if (category == "STUDIES")
                    {
                        var filePath = selectedFileName == null ? url : selectedFileName;
                        Stud st = new Stud(memeTitle, category, filePath);
                        FileCreated.Invoke(this, st);
                    }
                    else
                    {
                        var filePath = selectedFileName == null ? url : selectedFileName;
                        Bra br = new Bra(memeTitle, category, filePath);
                        FileCreated.Invoke(this, br);
                    }
                }

                this.Close();
            }
        }

        
    }
}

