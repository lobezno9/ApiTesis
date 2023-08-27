using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TokenHandler.Interfaces
{
    public interface IHanlderBO
    {
        public DbContext _context { get; set; }
    }
}