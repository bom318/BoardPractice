using BoardPractice.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BoardPractice.DB
{
    public class DBManager
    {
        public const string _connectionString = "Server=172.30.1.12; Database=BOARD_PRACTICE; uid=sa; pwd=qwe123!@#";

        public UserInfo GetUserInfo(string userId, string userPW)
        {
            UserInfo result = null;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_GET_USER_INFO", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@userPw", userPW);

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            result = new UserInfo();
                            result.userName = reader["userName"].ToString();
                            result.userId = reader["userId"].ToString();
                        }

                    }
                    con.Close();
                    con.Dispose();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GerUser : " + ex.Message);
            }


            return result;

        }
        public List<Board> GetBoardList()
        {
            List<Board> result = new List<Board>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_GET_BOARD_LIST", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Board board = new Board();
                            board.num = int.Parse(reader["NUM"].ToString());
                            board.title = reader["TITLE"].ToString();
                            board.content = reader["CONTENT"].ToString();
                            board.userId = reader["USERID"].ToString();

                            DateTime date = new DateTime();
                            if (DateTime.TryParse(reader["REGDATE"].ToString(), out date))
                            {
                                board.iDate = date;
                            }

                            result.Add(board);
                        }

                    }
                    con.Close();
                    con.Dispose();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetBoardList : " + ex.Message);
            }

            return result;

        }
        public bool CreateBoard(string userId, string title, string content)
        {
            bool result = false;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_INSERT_BOARD", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@content", content);

                        var excuteResult = cmd.ExecuteNonQuery();

                        if (excuteResult > 0)
                        {
                            result = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CreateBoard : " + ex.Message);
            }

            return result;
        }
        public Board BoardDetail(int num)
        {
            Board board = null;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_GET_BOARD_DETAILS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NUM", num);

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            board = new Board();
                            board.num = int.Parse(reader["NUM"].ToString());
                            board.title = reader["TITLE"].ToString();
                            board.content = reader["CONTENT"].ToString();
                            board.userId = reader["USERID"].ToString();

                            DateTime date = new DateTime();
                            if (DateTime.TryParse(reader["REGDATE"].ToString(), out date))
                            {
                                board.iDate = date;
                            }

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("BoardDetail : " + ex.Message);
            }

            return board;
        }

        public bool DeleteBoard(int num)
        {
            bool result = false;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_GET_BOARD_DELETE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NUM", num);

                        var excuteResult = cmd.ExecuteNonQuery();

                        if (excuteResult > 0)
                        {
                            result = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CreateBoard : " + ex.Message);
            }

            return result;
        }
        public bool UpdateBoard(string userId, string title, string content,int num)
        {
            bool result = false;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_UPDATE_BOARD", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@USERID", userId);
                        cmd.Parameters.AddWithValue("@TITLE", title);
                        cmd.Parameters.AddWithValue("@CONTENT", content);
                        cmd.Parameters.AddWithValue("@NUM", num);

                        var excuteResult = cmd.ExecuteNonQuery();

                        if (excuteResult > 0)
                        {
                            result = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateBoard : " + ex.Message);
            }

            return result;
        }
        public bool CreateUser(string userId, string userPw, string name)
        {
            bool result = false;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_INSERT_USER", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@USERID", userId);
                        cmd.Parameters.AddWithValue("@USERPW", userPw);
                        cmd.Parameters.AddWithValue("@USERNAME", name);

                        var excuteResult = cmd.ExecuteNonQuery();

                        if (excuteResult > 0)
                        {
                            result = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CreateUser : " + ex.Message);
            }

            return result;
        }
        public List<Board> GetSearchBoard(string text)
        {
            List<Board> result = new List<Board>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_SEARCH_BOARD", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TEXT", text);

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Board board = new Board();
                            board.num = int.Parse(reader["NUM"].ToString());
                            board.title = reader["TITLE"].ToString();
                            board.content = reader["CONTENT"].ToString();
                            board.userId = reader["USERID"].ToString();

                            DateTime date = new DateTime();
                            if (DateTime.TryParse(reader["REGDATE"].ToString(), out date))
                            {
                                board.iDate = date;
                            }

                            result.Add(board);
                        }

                    }
                    con.Close();
                    con.Dispose();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetSearchBoard : " + ex.Message);
            }

            return result;

        }

    }
}