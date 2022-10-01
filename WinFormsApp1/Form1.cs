using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    { 
        
        string connetionString = null;
            SqlConnection cnn;
        static int VALIDATION_DELAY = 1500;
        System.Threading.Timer timer = null;

        public SqlDataReader dataReader;
        public SqlDataReader dataReaders;
            string dateTime = DateTime.Now.ToShortTimeString();
       //public DataTable dt = new DataTable();

        static string id;
        static string name;
        static string group;
        static string groupname;
        static string status;
        static string TableNo;
        static string regdate;
        public Form1()
        {
            InitializeComponent();
              connetionString = "Data Source=DESKTOP-LH3ETHA\\SQLEXPRESS;Initial Catalog=invitation;integrated Security=true";
            cnn = new SqlConnection(connetionString);
            pictureBox1.Hide();
            pictureBox2.Hide();
            //pictureBox3.Hide();

            textBox1.Focus();
            FillListBox();
            


        }

        private void FillListBox()
        {
            try
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("Select * from Guest where Status = 'Present' ", cnn);

                dataReader = cmd.ExecuteReader();

                listBox1.Items.Clear();

                while (dataReader.Read())
                {
                    listBox1.Items.Add(dataReader.GetValue(1) +" - " + dataReader.GetValue(2)+" - " + dataReader.GetValue(3)+" - " + dataReader.GetValue(5));
                }

                cnn.Close();
            }

            catch (Exception ex)
            {
              
            }
        }
      
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox origin = sender as TextBox;
            if (!origin.ContainsFocus)
                return;

            DisposeTimer();
            timer = new System.Threading.Timer(TimerElapsed, null, VALIDATION_DELAY, VALIDATION_DELAY);

        }
        private void TimerElapsed(Object obj)
        {
            CheckSyntaxAndReport();
            DisposeTimer();
        }

        private void DisposeTimer()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }

        private void CheckSyntaxAndReport()
        {
            this.Invoke(new Action(() =>
            {
                FillListBox();
                ClearCHECKBOXLIST();
                DisablePics();
                string input = textBox1.Text;
                if (cnn.State == ConnectionState.Closed)
                    {
                        cnn.Open();

                    }
                //SqlCommand cmd = new SqlCommand("Select * from Guest where Groups = '" + input + "'  ", cnn);


                using (SqlCommand cmds = new SqlCommand("select GuestName,id,status, groups, groupname, concat(GuestName, ' ----- ' ,Status, ' ----- ', RegDate) as StatusName from guest  where Groups = '" + input + "' ", cnn))
                {
                    cmds.CommandType = CommandType.Text;

                    
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmds))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        ((ListBox)checkedListBox1).DataSource = dt;

                        ((ListBox)checkedListBox1).DisplayMember = "StatusName";
                        ((ListBox)checkedListBox1).ValueMember = "id";
                       
                       
                    }
                }
            
                //try
                //{





            //using (dataReader = cmd.ExecuteReader())
            //{
            //    if (dataReader.HasRows)
            //    {

            //        while (dataReader.Read())
            //        {
            //            id = dataReader.GetValue(0).ToString();
            //            name = dataReader.GetValue(1).ToString();
            //            group = dataReader.GetValue(2).ToString();
            //            groupname = dataReader.GetValue(3).ToString();
            //            status = dataReader.GetValue(4).ToString();
            //            regdate = dataReader.GetValue(5).ToString();
            //            TableNo = dataReader.GetValue(6).ToString();

            //        }
            //    }
            //    else
            //    {
            //        //pictureBox3.Hide();
            //        pictureBox1.Hide();
            //        pictureBox2.Show();
            //        ClearCHECKBOXLIST();
            //    }
            //}

            //if (status == "Present")
            //        {
            //        //pictureBox3.Show();
            //        label7.Text = "Table Occupied";
            //            pictureBox1.Hide();
            //            pictureBox2.Hide();
                    

            //        }

                    //else if (group != "") {

                   
                    //using (SqlCommand cmds = new SqlCommand("Select distinct[GuestName],id,status from Guest where Groups = '" + group + "' and Status is NULL ", cnn))
                    //{
                    //    cmds.CommandType = CommandType.Text;
                    //    using (SqlDataAdapter sda = new SqlDataAdapter(cmds))
                    //    {
                    //        DataTable dt = new DataTable();
                    //        sda.Fill(dt);
                           
                    //        ((ListBox)checkedListBox1).DataSource = dt;

                    //        ((ListBox)checkedListBox1).DisplayMember = "GuestName";
                    //        ((ListBox)checkedListBox1).ValueMember = "id";
                    //        label6.Text = "- " + groupname;
                    //        label7.Text = TableNo;
                    //    }
                    //}
                

                //SqlCommand cmdss = new SqlCommand("Select * from Guest where Groups = '" + group + "' ", cnn);

                //       using (dataReader = cmds.ExecuteReader())
                //        {

                        
                //        while (dataReader.Read())
                //        {//DataTable dtEmp = new DataTable();
                //         // add column to datatable  

                //            //dtEmp.Columns.Add("EmpID", typeof(int));
                //            //dtEmp.Columns.Add("EmpName", typeof(string));
                //            //dtEmp.Columns.Add("EmpCity", typeof(string));
                         
                //            checkedListBox1.Items.Add(dataReader.GetValue(1).ToString() + " "+ dataReader.GetValue(2).ToString());
                    
                //        }
                //                }
                    
                //    else if(status == "")
                //    {
                //        SqlCommand cmds = new SqlCommand("Update Guest set RegDate = '" + dateTime + "', status = 'Present' where id = '" + input + "' ", cnn);
                //        int result = (int)cmds.ExecuteNonQuery();
                //        pictureBox2.Hide();
                //        //pictureBox3.Hide();
                //        pictureBox1.Show();
                //        cnn.Close();

                //        cmds.Dispose();

                //    label7.Text = TableNo;
                //    //MessageBox.Show(dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(2));

                //}

                //else
                //{
                //    //pictureBox3.Hide();
                //    pictureBox1.Hide(); 
                //    pictureBox2.Show();
                    

                //}


                    ClearALL();





                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);

                //}



            }

            

                //string s = textBox1.Text.ToUpper(); //Do everything on the UI thread itself
                //label1.Text = s;

                ));
        }

        public void DisablePics()
        {
            pictureBox1.Hide();
            pictureBox2.Hide();
            //pictureBox3.Hide();
        }

       public void ClearALL()
        {
            textBox1.Clear();
            FillListBox();
            
            //id = "";
            //name = "";
            //status = "";
            //group = "";
            //groupname = "";
            //regdate = "";
        } 
        
        public void ClearCHECKBOXLIST()
        {
            ((ListBox)checkedListBox1).DataSource = null;

            for(int U = 0; U < checkedListBox1.Items.Count; U++)
            {
                checkedListBox1.Items.RemoveAt(U);
            }

            label6.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
              
                    
                   if(checkedListBox1.GetItemChecked(i))
                {
                    string Selected = checkedListBox1.SelectedValue.ToString();

                    
                    SqlCommand cmd = new SqlCommand("Select * from guest where id = '" + Selected + "' ", cnn);
                    using (dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {

                            while (dataReader.Read())
                            {
                                id = dataReader.GetValue(0).ToString();
                                name = dataReader.GetValue(1).ToString();
                                group = dataReader.GetValue(2).ToString();
                                groupname = dataReader.GetValue(3).ToString();
                                status = dataReader.GetValue(4).ToString();
                                regdate = dataReader.GetValue(5).ToString();
                                TableNo = dataReader.GetValue(6).ToString();

                            }
                        }
                    }
                    cmd.Dispose();
                    label6.Text = "- " + groupname;
                    label7.Text = TableNo;
                    if (status != "")
                    {
                        pictureBox1.Hide();
                        pictureBox2.Show();
                    }
                    else
                    {
                        SqlCommand cmds = new SqlCommand("Update Guest set RegDate = '" + dateTime + "', status = 'Present' where id = '" + Selected + "' ", cnn);
                        int result = (int)cmds.ExecuteNonQuery();
                        pictureBox2.Hide();
                        //pictureBox3.Hide();
                        pictureBox1.Show();
                        cnn.Close();
                        ClearALL();
                        ClearCHECKBOXLIST();
                        cmds.Dispose();

                    }

                }

                //strcheck += "" + checkedListBox1.Items[i].ToString() + ",";

            }

          

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
    
   

