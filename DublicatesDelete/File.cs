using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DublicatesDelete
{
    public class File : INotifyPropertyChanged
    {

        public int id { get; set; }
        private string n;
        private string e;
        private string f;
        private string c;

        public string name
        {
            get { return n; }
            set
            {
                n = value;
                OnPropertyChanged("name");
            }
        }
        public string extension
        {
            get { return e; }
            set
            {
                e = value;
                OnPropertyChanged("extension");
            }
        }

        public string folder_name
        {
            get { return f; }
            set
            {
                f = value;
                OnPropertyChanged("folder_name");
            }
        }

        public string code
        {
            get { return c; }
            set
            {
                c = value;
                OnPropertyChanged("code");
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