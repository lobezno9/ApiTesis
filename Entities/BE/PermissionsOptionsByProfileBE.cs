using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.BE
{
    public class PermissionsOptionsByProfileBE
    {
        public int Id { get; set; }
        public int IdProfile { get; set; }
        public int IdOption { get; set; }
        public int IdPermission { get; set; }
        public bool? IsActive { get; set; }
        public string Description { get; set; }//Nombre del permiso
    }
}

