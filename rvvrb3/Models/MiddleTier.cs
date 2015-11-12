using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Provider;

namespace rvvrb3.Models
{
        public class MiddleTier
        {
            //Add a data adapter
            //private SqlDataAdapter rvvrbAdapter = new SqlDataAdapter();

            //Instantiate connection object
            private SqlConnection sconn = new SqlConnection();

            //instantiate the sqlcommand object
            private SqlCommand scmd = new SqlCommand();

            //constructor
            public MiddleTier()
            {
                //get the connection string from the web config
                sconn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            }
   
           // public SqlConnection OpenDatabase()
           // {
           //     if (sconn.State == ConnectionState.Closed)
           //     {
           //         try
           //         {
           //             //open the connection
           //             sconn.Open();
           //
           //             //return the object
           //             return sconn;
           //         }
           //         catch (SqlException sqx)
           //         {
           //             throw sqx;
           //         }
           //         catch (Exception ex)
           //         {
           //             throw ex;
           //         }
           //     }
           //     return null;
           // }

            //public SqlConnection CloseDatabase()
            //{
            //    try
            //    {
            //        //close the database connection
            //        sconn.Close();
            //
            //        //return the object
            //        return sconn;
            //    }
            //    catch (SqlException sqx)
            //    {
            //        throw sqx;
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //
            //}

            public string ConnectionStatus()
            {
                return sconn.State.ToString();
            }

            public RvvrbDataTable ReturnHomeSongs()
            {

                if (sconn.State == ConnectionState.Closed)
                {
                    try
                    {
                        //open the connection
                        sconn.Open();

                        DataTable dt = new DataTable();
                        dt.TableName = "Songs";

                        scmd = new SqlCommand("getAllSongsNotInPlaylists", sconn);
                        scmd.Connection = sconn;
                        scmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataReader dr = scmd.ExecuteReader();
                        dt.Load(dr);
                        dr.Close();
                        RvvrbDataTable rdt = new RvvrbDataTable(dt);
                        return rdt;

                    }

                    catch (SqlException sqx)
                    {
                        throw sqx;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        sconn.Close();
                        sconn.Dispose();
                        SqlConnection.ClearPool(sconn);
                }
                }
                return null;

            }


            public RvvrbDataTable ReturnSong(int id)
            {
            if (sconn.State == ConnectionState.Closed)
            {
                try
                {
                    //open the connection
                    sconn.Open();

                    DataTable dt = new DataTable();
                    dt.TableName = "Songs";

                    scmd = new SqlCommand("getSongByID", sconn);
                    scmd.Connection = sconn;
                    scmd.CommandType = System.Data.CommandType.StoredProcedure;
                    scmd.Parameters.Add("@sid", SqlDbType.Int).Value = id;


                    SqlDataReader dr = scmd.ExecuteReader();
                    dt.Load(dr);
                    dr.Close();
                    RvvrbDataTable rdt = new RvvrbDataTable(dt);

                    return rdt;
                }
                catch (SqlException sqx)
                {
                    throw sqx;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sconn.Close();
                    sconn.Dispose();
                    SqlConnection.ClearPool(sconn);
                }
            }
                return null;
            }


           public void CreateComment(
                int userID,
                int songID,
                string comment
                )
            {
               if (sconn.State == ConnectionState.Closed)
               {
                   try
                   {
                       //open the connection
                       sconn.Open();

                       scmd = new SqlCommand("createComment", sconn);
                       scmd.Connection = sconn;
                       scmd.CommandType = System.Data.CommandType.StoredProcedure;

                       scmd.Parameters.Add("@userID ", SqlDbType.Int).Value = userID;
                       scmd.Parameters.Add("@comment ", SqlDbType.NVarChar).Value = comment;
                       scmd.Parameters.Add("@trackID ", SqlDbType.Int).Value = songID;

                       //execute the cmd
                       scmd.ExecuteNonQuery();
                   }
                   catch (SqlException sqx)
                   {
                       throw sqx;
                   }
                   catch (Exception ex)
                   {
                       throw ex;
                   }
                   finally
                   {
                       sconn.Close();
                       sconn.Dispose();
                       SqlConnection.ClearPool(sconn);
                }
               }             
            }

        public RvvrbDataTable GetComments(int id)
        {
            if (sconn.State == ConnectionState.Closed)
            {
                try
                {
                    //open the connection
                    sconn.Open();

                    DataTable dt = new DataTable();
                    dt.TableName = "Songs";

                    scmd = new SqlCommand("getComments", sconn);
                    scmd.Connection = sconn;
                    scmd.CommandType = System.Data.CommandType.StoredProcedure;

                    scmd.Parameters.Add("@trackID ", SqlDbType.Int).Value = id;

                    SqlDataReader dr = scmd.ExecuteReader();
                    dt.Load(dr);
                    dr.Close();
                    RvvrbDataTable rdt = new RvvrbDataTable(dt);

                    return rdt;
                }
                catch (SqlException sqx)
                {
                    throw sqx;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sconn.Close();
                    sconn.Dispose();
                    SqlConnection.ClearPool(sconn);
                }
            }
            return null;
        }

        public void CreateSong(
                int userID,
                string songname,
                int trackNum,
                string description,
                string artist,
                string filename
                )
            {
            if (sconn.State == ConnectionState.Closed)
            {
                try
                {
                    //open the connection
                    sconn.Open();
                    scmd = new SqlCommand("createSong", sconn);
                    scmd.Connection = sconn;
                    scmd.CommandType = System.Data.CommandType.StoredProcedure;

                    scmd.Parameters.Add("@userID ", SqlDbType.NVarChar).Value = userID;
                    scmd.Parameters.Add("@songname ", SqlDbType.NVarChar).Value = songname;
                    scmd.Parameters.Add("@songtracknum ", SqlDbType.Int).Value = trackNum;
                    scmd.Parameters.Add("@songdescription ", SqlDbType.NVarChar).Value = description;
                    scmd.Parameters.Add("@songartist ", SqlDbType.NVarChar).Value = artist;
                    scmd.Parameters.Add("@filename ", SqlDbType.NVarChar).Value = filename;

                    //execute the cmd
                    scmd.ExecuteNonQuery();
                }
                catch (SqlException sqx)
                {
                    throw sqx;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sconn.Close();
                    sconn.Dispose();
                    SqlConnection.ClearPool(sconn);
                }
            }
            }
            public void UpdateDbImage(int userID, string filename)
            {
                if (sconn.State == ConnectionState.Closed)
                {
                    try
                    {
                        //open the connection
                        sconn.Open();

                        scmd = new SqlCommand("updatePhoto", sconn);
                        scmd.Connection = sconn;
                        scmd.CommandType = System.Data.CommandType.StoredProcedure;

                        scmd.Parameters.Add("@ID ", SqlDbType.NVarChar).Value = userID;
                        scmd.Parameters.Add("@image", SqlDbType.NVarChar).Value = filename;


                        //execute the cmd
                        scmd.ExecuteNonQuery();
                    }
                    catch (SqlException sqx)
                    {
                        throw sqx;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        sconn.Close();
                        sconn.Dispose();
                        SqlConnection.ClearPool(sconn);
                }
                }
            }
        
        }
    }
    