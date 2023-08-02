using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.UI.MobileControls;
using static MongoDB.Driver.WriteConcern;

namespace Crud9.Models
{
    public class StudentBookConnection
    {
        private SqlConnection con;


        private void Connection()
        {
            string str = ConfigurationManager.ConnectionStrings["StudentBook"].ToString();
            con = new SqlConnection(str);
        }
        public List<StudentViewModel> GetStudents()
        {
            {
                Connection();
                SqlCommand cmt = new SqlCommand("GetAllData", con);
                List<StudentViewModel> list = new List<StudentViewModel>();
                cmt.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter st = new SqlDataAdapter(cmt);
                DataSet dS = new DataSet();
                con.Open();
                st.Fill(dS);
                con.Close();
                if (dS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow sdr in dS.Tables[0].Rows)
                    {

                        list.Add(
                            new StudentViewModel
                            {

                                StudentID = Convert.ToInt32(sdr["StudentID"]),
                                StudentName = Convert.ToString(sdr["StudentName"]),
                                Email = Convert.ToString(sdr["Email"]),
                                Password = Convert.ToString(sdr["Password"]),
                                Phone = Convert.ToString(sdr["Phone"]),
                                IsAdmin = Convert.ToBoolean(sdr["IsAdmin"])
                            });
                    }
                }

                if (dS.Tables[1].Rows.Count > 0)
                {
                    list.ForEach(dr =>
                    {

                        var rows = dS.Tables[1].AsEnumerable().Where(obj => obj.Field<int>("StudentID")
                        == dr.StudentID).ToList();
                        dr.BookList = new List<BookViewModel>();
                        rows.ForEach(sdr =>
                        {
                            dr.BookList.Add(
                               new BookViewModel
                               {
                                   BookID = Convert.ToInt32(sdr["BookID"]),
                                   BookName = Convert.ToString(sdr["BookName"])
                               });
                        });

                    });
                }

                return list;
            }
        }
        public bool AddStudent(StudentViewModel st)
        {
            var selectedList = st.BookList.Where(obj => obj.IsSelected == true).Select(o => o.BookID).ToList();
            var BookList = string.Join(",", selectedList).ToString();
            Connection();
            SqlCommand cmt = new SqlCommand("AddDetails", con);
            cmt.CommandType = CommandType.StoredProcedure;
            cmt.Parameters.AddWithValue("StudentID", st.StudentID);
            cmt.Parameters.AddWithValue("StudentName", st.StudentName);
            cmt.Parameters.AddWithValue("Email", st.Email);
            cmt.Parameters.AddWithValue("Password", st.Password);
            cmt.Parameters.AddWithValue("Phone", st.Phone);
            cmt.Parameters.AddWithValue("bookIDs", BookList);
            con.Open();
            int w = cmt.ExecuteNonQuery();
            con.Close();
            if (w >= 0) return true;
            else return false;
        }
        public bool DeleteStudents(int id)
        {
            Connection();
            SqlCommand cmt = new SqlCommand("DeleteStudent", con);
            con.Open();
            cmt.CommandType = CommandType.StoredProcedure;
            cmt.Parameters.AddWithValue("StudentID", id);
            int w = cmt.ExecuteNonQuery();
            con.Close();
            if (w >= 1) return true;
            else return false;

        }
        public List<BookViewModel> GetBooks()
        {
            List<BookViewModel> books = new List<BookViewModel>();
            Connection();
            SqlCommand cmt = new SqlCommand("GetAllBooks", con);
            con.Open();

            using (SqlDataReader red = cmt.ExecuteReader())
            {
                while (red.Read())
                {
                    books.Add(new BookViewModel
                    {
                        BookID = (int)red["BookID"],
                        BookName = red["BookName"].ToString(),

                    });
                }
            }
            return books;
        }
    }
}    
 


/* public string GetUserPassword(string userEmail)
         {
             Connection();
             var user = from obj in GetStudents() where obj.Email == userEmail select obj;
             if (user.Any())
             {
                 return user.First().Password;
             }
             else { return string.Empty; }
         }*/
//using (SqlCommand cmt1 = new SqlCommand("AddNewBooks", con))
//{
//    cmt1.CommandType = CommandType.StoredProcedure;

//    foreach (var item in st.BookList)
//    {
//        if (item.IsSelected)
//        {
//            cmt1.Parameters.Clear();
//            cmt1.Parameters.AddWithValue("StudentID", studentID);
//            cmt1.Parameters.AddWithValue("BookID", item.BookID);
//            int newbookid = Convert.ToInt32(cmt1.ExecuteScalar());
//        }
//    }
//}
/*        public bool UpdateStudent(StudentViewModel st)
        {
            Connection();
            SqlCommand cmt = new SqlCommand("UpdateStudents", con);
            cmt.CommandType = CommandType.StoredProcedure;
            cmt.Parameters.AddWithValue("StudentID", st.StudentID);
            cmt.Parameters.AddWithValue("StudentName", st.StudentName);
            cmt.Parameters.AddWithValue("Email", st.Email);
            cmt.Parameters.AddWithValue("Password", st.Password);
            cmt.Parameters.AddWithValue("Phone", st.Phone);
            con.Open();
            int StudentID = Convert.ToInt32(cmt.ExecuteScalar());
                
          
            SqlCommand cmt2 = new SqlCommand("AddNewBooks", con);
            cmt2.CommandType = CommandType.StoredProcedure;
            
                foreach (var items in st.BookList)
                {

                if (items.IsSelected)
                {
                    cmt2.Parameters.Clear();
                    cmt2.Parameters.AddWithValue("StudentID", st.StudentID);
                    cmt2.Parameters.AddWithValue("BookID", items.BookID);
                    int newbookid = cmt2.ExecuteNonQuery();
                }
            }
           
            con.Close();
            if (StudentID >= 1) return true;
            else return false;
        }*/
/*     foreach (Books t in books)
      {
              Console.Write($"[{t.BookID}] {t.BookName}: ");
              bool isChecked = bool.Parse(Console.ReadLine());
              t.IsSelected = isChecked;
      }
  }
  con.Close() ;
  return books;
}
public void Save(string Books, int BookID )
{

          Connection();
          SqlCommand cmt = new SqlCommand("AddNewBooks", con);
          cmt.Parameters.AddWithValue("@Books", Books);
          cmt.Parameters.AddWithValue("@BookID", BookID);
          con.Open();
          cmt.ExecuteNonQuery();
          con.Close();
      }
public List<Books> Edit()
{
  List<Books> books = new List<Books>();
  Connection();
  SqlCommand cmt = new SqlCommand("GetAllBooks", con);
  con.Open();

  using (SqlDataReader red = cmt.ExecuteReader())
  {
      while (red.Read())
      {
          Books book = new Books();
          book.BookName = red["BookName"].ToString();
          book.BookID = (int)red["BookID"];
          books.Add(book);
      }
      con.Close();
  }
  return books;
}
}
}*/
/*   public List<Books> GetBooks()
   {
       List<Books> Books = new List<Books>();
       Connection();
       SqlCommand cmt = new SqlCommand("GetAllBooks", con);
       con.Open();

       using (SqlDataReader red = cmt.ExecuteReader())
       {
           while (red.Read())
           {
               Books book = new Books();
               book.BookName = red["BookName"].ToString();
               book.BookID = (int)red["BookID"];
               Books.Add(book);
           }
           con.Close();
       }
            foreach (Books book in Books)
           {
               Console.Write($"[{book.BookID}] {book.BookName}: ");
               bool isChecked = bool.Parse(Console.ReadLine());
               book.IsSelected = isChecked;
           }
           return Books;     
   }
   public void SaveSelectedBooks(List<Books> selectedBooks, int studentID)
   {
       Connection();
       con.Open();

       foreach (Books book in selectedBooks)
       {
           if (book.IsSelected)
           {
               SqlCommand Cmd = new SqlCommand("INSERT INTO studentBook (StudentID, BookID, IsSelected) VALUES (@StudentID, @BookID, @IsSelected)", con);
               Cmd.Parameters.AddWithValue("@StudentID", studentID);
               Cmd.Parameters.AddWithValue("@BookID", book.BookID);
               Cmd.Parameters.AddWithValue("@IsSelected", book.IsSelected);
               Cmd.ExecuteNonQuery();
           }
       }

       con.Close();
   }*/


/*      public List<Student> GetBooks()
      {
          {
              Connection();
              SqlCommand cmt = new SqlCommand("GetAllBooks", con);
              List<Student> list = new List<Student>();
              cmt.CommandType = CommandType.StoredProcedure;
              SqlDataAdapter st = new SqlDataAdapter(cmt);
              DataTable dt = new DataTable();
              con.Open();
              st.Fill(dt);
              con.Close();

              foreach (DataRow sdr in dt.Rows)
              {

                  list.Add(
                      new Student
                      {
                          BookName = Convert.ToString(sdr["BookName"]),
                           BookID = Convert.ToInt32(sdr["BookID"])
                      });
              }
              return list;

          }

      }                  */