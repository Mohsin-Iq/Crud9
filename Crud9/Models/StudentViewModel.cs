using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crud9.Models
{
    public class StudentViewModel:Student
    {
        public List<BookViewModel> BookList { get; set; }
        public bool IsAdmin { get; internal set; }
    }
}