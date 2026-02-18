using System;

namespace AppMovilTrotaMundos.Models
{

        public class MenuItem
        {
            public string Title { get; set; }
            public string IconUnicode { get; set; } // ← Aquí irá el código Font Awesome
            public Type TargetType { get; set; }
        }
    
}
