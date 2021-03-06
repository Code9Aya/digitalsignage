﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    string connStr = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\maindb.mdf;Integrated Security = True";
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        dt = Load();
        HtmlGenericControl div, h, p, img;

        foreach (DataRow row in dt.Rows)
        {
            Response.Write(row["name"]);
            h = new HtmlGenericControl("h3");
            h.ID = "dynDivCode";
            h.InnerHtml = row["name"].ToString();
            accordion.Controls.Add(h);
           
            div = new HtmlGenericControl("div");

            img = new HtmlGenericControl("img");
            img.Attributes["src"] = row["photo"].ToString();
            img.Attributes["class"] = "photos";
            div.Controls.Add(img);

            p = new HtmlGenericControl("p");
            p.InnerHtml = row["profile"].ToString();
            div.Controls.Add(p);

            accordion.Controls.Add(div);
        }
    }

    protected DataTable Load()
    {
        DataTable table = new DataTable();

        SqlConnection conn = new SqlConnection(connStr);
        string command = "SELECT * FROM donors";
        SqlCommand cmd = new SqlCommand(command, conn);
        SqlDataReader dr;
        conn.Open();
        dr = cmd.ExecuteReader();
        table.Load(dr);
        conn.Close();
        return table;
    }
}