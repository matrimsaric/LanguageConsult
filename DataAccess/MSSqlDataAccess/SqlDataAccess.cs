﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.DataAccess.MSSqlDataAccess
{
    internal class SqlDataAccess
    {
        private string connectionString = "Data Source=DESKTOP-PNSFQ01;Initial Catalog=LANGUAGE;Integrated Security=true;";// TODO Need to pass these in somehow

        internal SqlDataAccess()
        {

        }

        internal void ExecuteNonQuery(string strSqlStatement)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(strSqlStatement, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }

        }

        internal void ExecuteCommand(SqlCommand incoming)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                incoming.Connection = conn;
                conn.Open();

                incoming.ExecuteNonQuery();
            }
        }

        internal DataSet ExecuteGetCommand(SqlCommand incoming)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                incoming.Connection = conn;
                conn.Open();

                SqlDataAdapter adp = new SqlDataAdapter(incoming);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                return ds;
            }
        }

        internal DataTable GetData(string sql)
        {
            DataTable response = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                SqlCommand oCmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(sql, conn))
                {
                    sda.Fill(response);
                }
                conn.Close();

            }
            return response;
        }

        internal DataTable GetDataCom(SqlCommand com)
        {
            DataTable response = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                com.Connection = conn;
                conn.Open();
                using (SqlDataReader dr = com.ExecuteReader())
                {
                    response.Load(dr);
                }
                conn.Close();

            }
            return response;
        }
    }


}