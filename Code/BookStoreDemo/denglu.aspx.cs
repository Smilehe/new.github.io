﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class denglu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lab_usps.Visible = false;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["aspxmConnectionString"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = @"SELECT * FROM [users]";
        try
        {
            conn.Open();  //打开链接                
            SqlDataReader reader = cmd.ExecuteReader();  //执行查询操作，返回SqlDataReader 对象                
            while (reader.Read() == true)
            {
                string strName = reader["userName"].ToString();
                string strpsw = reader["passWord"].ToString();
                if (strName==txt_user.Text)
                {
                    if(strpsw==txt_pasw.Text)
                    {
                        if (txt_yz.Text == Session["vcode"].ToString())
                        {
                            Session["user"] = this.txt_user.Text;
                            Session["url"] = "denglu";
                            Response.Redirect("index.aspx");
                        }
                        else
                        {
                            Response.Write("<script>alert('验证码不正确')</script>");
                        }
                    }
                    else
                    {
                        lab_usps.Visible = true;
                    }
                }
                else
                {
                    lab_usps.Visible = true;
                }
            }            
            conn.Close();
        }
        catch (Exception ex)
        {
        }
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {       
       Response.Redirect("zhuce.aspx");
    }
    protected void img_yz_DataBinding(object sender, EventArgs e)
    {
        
    }
    protected void img_yz_Click(object sender, ImageClickEventArgs e)
    {
        img_yz.ImageUrl = "Code.aspx?KeyTime=" + DateTime.Now.ToString();
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Session["url"] = "";
        Response.Redirect("index.aspx");
    }
}