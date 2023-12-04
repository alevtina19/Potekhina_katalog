using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;


namespace Potekhina_katalog
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        List<Meme> memes = new List<Meme> { };

        private void updateImagess(Meme meme)
        {

            imagess.Source = new BitmapImage(new Uri(meme.filePath));
        }
        //private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        //{
        //    if (catalog.SelectedItem.GetType() == typeof(Progr))
        //    {

        //        Progr selectedItem = (Progr)catalog.SelectedItem;
        //        updateImagess(selectedItem);
        //    }
        //    else if ((catalog.SelectedItem.GetType() == typeof(Stud)))
        //    {


        //        Stud selectedItem = (Stud)catalog.SelectedItem;
        //        updateImagess(selectedItem);

        //    }
        //    else
        //    {
        //        Bra selectedItem = (Bra)catalog.SelectedItem;
        //        updateImagess(selectedItem);
        //    }

        //}
        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                if (catalog.SelectedItem.GetType() == typeof(Progr))
                {
                    Progr selectedItemGif = (Progr)catalog.SelectedItem;
                    updateImagess(selectedItemGif);
                }
                else if (catalog.SelectedItem.GetType() == typeof(Stud))
                {
                    Stud selectedItem = (Stud)catalog.SelectedItem;
                    updateImagess(selectedItem);
                }
                else
                {
                    Bra selectedItem = (Bra)catalog.SelectedItem;
                    updateImagess(selectedItem);


                }
            }
            catch { }
        }




        private void AddNewMemeClick(object sender, RoutedEventArgs e)
        {
            ADDMemes newfileWindow = new ADDMemes();
            newfileWindow.FileCreated += File_Created;
            newfileWindow.ShowDialog();
        }

        private void File_Created(object sender, Meme meme)
        {
            switch (meme.category)
            {
                case "PROGRAMMER":
                    prg_catal.Items.Add(meme);
                    memes.Add(meme);
                    break;
                case "STUDIES":
                    std_catal.Items.Add(meme);
                    memes.Add(meme);
                    break;
                case "BRAIN":
                    brn_catal.Items.Add(meme);
                    memes.Add(meme);
                    break;
            }
        }

   

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (catalog.SelectedItem != null)
            {
                Meme selectedItem = catalog.SelectedItem as Meme;
                memes.Remove(selectedItem);

                switch (selectedItem.category)
                {
                    case "PROGRAMMER":
                        prg_catal.Items.Remove(selectedItem);
                        break;
                    case "STUDIES":
                        std_catal.Items.Remove(selectedItem);
                        break;
                    case "BRAIN":
                        brn_catal.Items.Remove(selectedItem);
                        break;
                }

            }



        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(memes);
            File.WriteAllText("C:\\Users\\79030\\Desktop\\университет\\2 курс 3 семестр\\ПИоГИ\\КУРСОВАЯ\\data.json", json);
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            {
                try
                {
                    string json = File.ReadAllText("C:\\Users\\79030\\Desktop\\университет\\2 курс 3 семестр\\ПИоГИ\\КУРСОВАЯ\\data.json");
                    List<Meme> jsMemes = JsonConvert.DeserializeObject<List<Meme>>(json);

                    foreach (Meme meme in jsMemes)
                    {
                        if (meme.category == "PROGRAMMER")
                        {
                            Progr prg = new Progr(meme.title, meme.category, meme.filePath);
                            memes.Add(prg);
                            prg_catal.Items.Add(prg);
                        }
                        else if (meme.category == "STUDIES")
                        {
                            Stud st = new Stud(meme.title, meme.category, meme.filePath);
                            memes.Add(st);
                            std_catal.Items.Add(st);
                        }
                        else if (meme.category == "BRAIN")
                        {
                            Bra br = new Bra(meme.title, meme.category, meme.filePath);
                            memes.Add(br);
                            brn_catal.Items.Add(br);
                        }

                    }

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void Cat_Sel(object sender, RoutedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)categories_filter.SelectedItem;

            if (comboBoxItem.Name == "seleceted_progr")
            {
                prg_catal.Visibility = Visibility.Visible;
                brn_catal.Visibility = Visibility.Hidden;
                std_catal.Visibility = Visibility.Hidden;
            }
            else if (comboBoxItem.Name == "seleceted_std")
            {
                std_catal.Visibility = Visibility.Visible;
                brn_catal.Visibility = Visibility.Hidden;
                prg_catal.Visibility = Visibility.Hidden;
            }
            else if (comboBoxItem.Name == "seleceted_brn")
            {
                brn_catal.Visibility = Visibility.Visible;
                prg_catal.Visibility = Visibility.Hidden;
                std_catal.Visibility = Visibility.Hidden;
            }
        }
    }        
}
