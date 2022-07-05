using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;

namespace DublicatesDelete
{
    /// <summary>
    /// Логика взаимодействия для Duplicates.xaml
    /// </summary>
    public partial class Duplicates : Window
    {
        ApplicationContext db;

        public Duplicates(string e_path)
        {
            InitializeComponent();
            db = new ApplicationContext();

            var rowss = from o in db.Files
                    select o;
            foreach (var row in rowss)
            {
                db.Files.Remove(row);
            }
            db.SaveChanges();
            var q = from o in db.Duplicates
                    select o;
            foreach (var row in q)
            {
                db.Duplicates.Remove(row);
            }
            db.SaveChanges();

            var rows = from o in db.Folders
                       select o;
            foreach (var row in rows)
            {
                DirConsole(row.folder); 
            }
            var dup = db.Files.Where(f => f.folder_name==e_path);
            foreach (var row in dup)
            {
                Console.WriteLine(row.code);
                var dupl = db.Files.Where(f=>f.code == row.code && f.folder_name!=e_path);
                foreach(var r in dupl)
                {
                    Duplicate d = new Duplicate();
                    d.file_name = row.name + row.extension;
                    d.dup_name = r.name + r.extension;
                    d.f_path = row.folder_name;
                    d.dup_path = r.folder_name;
                    db.Duplicates.Add(d);
                    db.SaveChanges();
                }

            }
            this.DataContext = db.Duplicates.Local.ToBindingList();
        }

        public void DirConsole(string pathToFolder)
        {
            Dir(pathToFolder, 0);

            void Dir(string nameFolder, int level)
            {
                
                string[] allfiles = Directory.GetFiles(nameFolder);
                foreach (string filename in allfiles)
                {
                    
                    File f = new File();
                    var path = Path.GetDirectoryName(filename);
                    f.name = Path.GetFileNameWithoutExtension(filename);
                    f.extension = Path.GetExtension(filename);
                    f.folder_name = path;
                    using (var md5 = MD5.Create())
                    {
                        using (var stream = System.IO.File.OpenRead(filename))
                        {
                            var hash = md5.ComputeHash(stream);
                            f.code = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                        }
                    }
                    if (db.Files.Select(fol => new { name = f.name, code = f, extension = f.extension, folder_name = db.Folders.Select(g => g.folder) }) != null)
                    {
                        db.Files.Add(f);
                        db.SaveChanges();
                    }
                    
                }
                string[] allfolders = Directory.GetDirectories(nameFolder);

                foreach (string f in allfolders)
                {
                    Dir(f, level + 1);
                }
            }
        }

        private void button_restore_Click(object sender, RoutedEventArgs e)
        {
            if (duplicatesDataGrid.SelectedItem == null) return;
            Duplicate dup = duplicatesDataGrid.SelectedItem as Duplicate;
            db.Duplicates.Remove(dup);
            db.SaveChanges();
        }

        private void button_del_Click(object sender, RoutedEventArgs e)
        {
            var dup = db.Duplicates;
            foreach (var d in dup)
            {
                string s = @"";
                s = s + d.dup_path + "\\" + d.dup_name;
                System.IO.File.Delete(s);
                db.Duplicates.Remove(d);
                db.SaveChanges();
                this.Close();
            }
        }
    }
}
