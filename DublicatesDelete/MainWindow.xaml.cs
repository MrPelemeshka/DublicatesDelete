using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace DublicatesDelete
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;

        public MainWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();
            var rows = from o in db.Folders
                       select o;
            foreach (var row in rows)
            {
                db.Folders.Remove(row);
            }
            db.SaveChanges();
            
            db.Folders.Load();
            this.DataContext = db.Folders.Local.ToBindingList();
        }

        private void button_folder_1_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Folder folders = new Folder();
                    folders.folder = dialog.SelectedPath;
                    db.Folders.Add(folders);
                    db.SaveChanges();
                }
            }
        }

        private void button_del_Click(object sender, RoutedEventArgs e)
        {
            if (foldersList.SelectedItem == null) return;
            Folder folder = foldersList.SelectedItem as Folder;
            db.Folders.Remove(folder);
            db.SaveChanges();
        }

        private void eth_btn_Click(object sender, RoutedEventArgs e)
        {
            if (foldersList.SelectedItem == null) return;
            Folder folder = foldersList.SelectedItem as Folder;
            eth_path.Text = folder.folder;
        }

        private void button_search_Click(object sender, RoutedEventArgs e)
        {
            var DuplicateForm = new Duplicates(eth_path.Text);
            DuplicateForm.Show();
        }
    }
}

