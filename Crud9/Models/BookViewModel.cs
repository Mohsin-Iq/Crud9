using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crud9.Models
{
    public class BookViewModel :Book
    {
        public bool IsSelected { get; set; }
     }
}