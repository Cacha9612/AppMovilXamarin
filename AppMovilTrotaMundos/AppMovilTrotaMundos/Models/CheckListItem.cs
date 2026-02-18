using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AppMovilTrotaMundos.Models
{
    public class CheckListItem : INotifyPropertyChanged
    {
        public string NombreCampo { get; set; }

        private int _estado;
        public int Estado
        {
            get => _estado;
            set
            {
                if (_estado != value)
                {
                    _estado = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Estado)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
