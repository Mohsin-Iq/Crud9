/*using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


namespace Crud9.Models
{
    public class StudendsDBHandler
    {
        private SqlConnection con;
        private void Connection()
        {
            string hd = ConfigurationManager.ConnectionStrings["StudentsDB"].ToString();
          
            con = new SqlConnection(hd);
    
        }
        public bool AddStudents(Students sd)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("AddNewStudents", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", sd.Name);
            cmd.Parameters.AddWithValue("@Email", sd.Email);
            cmd.Parameters.AddWithValue("@Address", sd.Address);
            cmd.Parameters.AddWithValue("@Number", sd.Number);
            cmd.Parameters.AddWithValue("@Gender", sd.Gender);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public List<Students> GetStudent()
        {
            Connection();
            List<Students> studentlist = new List<Students>();

            SqlCommand cmt = new SqlCommand("GetStudentDetails", con);
            cmt.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter st = new SqlDataAdapter(cmt);
            DataTable dt = new DataTable();
            con.Open();
            st.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                studentlist.Add(
                    new Students
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Name = Convert.ToString(dr["name"]),
                        Email = Convert.ToString(dr["email"]),
                        Address = Convert.ToString(dr["address"]),
                        Number = Convert.ToInt32(dr["Number"]),
                        Gender = Convert.ToString(dr["Gender"]),  
                    });
            }
            return studentlist;
        }
        public bool UpdateDetails(Students st)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("UpdateStudentsDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", st.Id);
            cmd.Parameters.AddWithValue("@Name", st.Name);
            cmd.Parameters.AddWithValue("@Email", st.Email);
            cmd.Parameters.AddWithValue("@Address", st.Address);
            cmd.Parameters.AddWithValue("@Number", st.Number);
            cmd.Parameters.AddWithValue("@Gender", st.Gender);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool DeleteStudents(int id)
        {
            Connection();
            SqlCommand cme = new SqlCommand("DeleteStudent", con);
            cme.CommandType = CommandType.StoredProcedure;
            cme.Parameters.AddWithValue("id", id);
            con.Open();
            int w = cme.ExecuteNonQuery();
            con.Close();
            if (w >= 1)
                return true;
            else
                return false;
        }
        
    }

}*/