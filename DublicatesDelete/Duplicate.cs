using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DublicatesDelete
{
    public class Duplicate : INotifyPropertyChanged
    {
        public int id { get; set; }
        private string f;
        private string d;
        private string f_p;
        private string d_p;

        public string file_name
        {
            get { return f; }
            set
            {
                f = value;
                OnPropertyChanged("file_name");
            }
        }
        public string dup_name
        {
            get { return d; }
            set
            {
                d = value;
                OnPropertyChanged("dup_name");
            }
        }

        public string f_path
        {
            get { return f_p; }
            set
            {
                f_p = value;
                OnPropertyChanged("f_path");
            }
        }

        public string dup_path
        {
            get { return d_p; }
            set
            {
                d_p = value;
                OnPropertyChanged("dup_path");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
