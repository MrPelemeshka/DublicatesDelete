using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DublicatesDelete
{
    public class Folder : INotifyPropertyChanged
    {

        public int id { get; set; }
        private string route;
        public string folder
        {
            get { return route; }
            set
            {
                route = value;
                OnPropertyChanged("folder");
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