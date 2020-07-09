using System;
using System.Data;
using AltV.Net.Elements.Entities;
using MySql.Data.MySqlClient;

namespace roleplay
{
    class Class_mysql
    {

        #region globals

        private Class_errorhandling eh;
        private string cs;
        private MySqlConnection con;
        private MySqlCommand cmd;

        #endregion

        #region constructor

        public Class_mysql(Class_errorhandling eh)
        {
            this.eh = eh;

            cs = "server=localhost;port=3306;user=root;password=;database=roleplay";
            con = new MySqlConnection(cs);
            cmd = new MySqlCommand();
            cmd.Connection = con;
        }

        #endregion

        #region mysql public

        public bool openCon()
        {
            try
            {
                con.Open();
                if (con.State == ConnectionState.Open) return true;
            }
            catch (Exception exc) { eh.errorHandling(exc); }
            return false;
        }

        public bool closeCon()
        {
            try
            {
                if (con.State != ConnectionState.Open) return false;
                con.Close();
                return true;
            }
            catch (Exception exc) { eh.errorHandling(exc); }
            return false;
        }

        public bool checkCon()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Mysql was needed but not opened");
                }
            }
            catch (Exception exc) { eh.errorHandling(exc); }
            return false;
        }

        #endregion

        #region mysql private

        private int executeNQ()
        {
            try
            {
                if (checkCon())
                {
                    int affected = cmd.ExecuteNonQuery();
                    return affected;
                }
            }
            catch (Exception ex) { eh.errorHandling(ex); }
            return -1;
        }

        private object executeS()
        {
            try
            {
                if (checkCon())
                {
                    object result = cmd.ExecuteScalar();
                    return result;
                }
            }
            catch (Exception ex) { eh.errorHandling(ex); }
            return null;
        }

        private DataTable getDataTable(string sqlstr)
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sqlstr, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex) { eh.errorHandling(ex); }
            return null;
        }

        #endregion

        #region mysql player

        public bool isPlayerInDB(IPlayer player)
        {
            try
            {
                if (!checkCon()) return false;
                cmd.CommandText = "SELECT id FROM user WHERE socialid = @socialid";
                cmd.Parameters.AddWithValue("@socialid", player.SocialClubId);
                if(executeS() != null) return true;
            }
            catch (Exception exc) { eh.errorHandling(exc); }
            return false;
        
        }

        public bool createPlayerInDB(IPlayer player)
        {
            try
            {
                if (!checkCon()) return false;
                cmd.CommandText = "INSERT INTO user(socialid,name,password) VALUES(@socialid, @name, @password)";
                cmd.Parameters.AddWithValue("@socialid", player.SocialClubId);
                cmd.Parameters.AddWithValue("@name", player.Name);
                cmd.Parameters.AddWithValue("@password", "ayayay");
                if (executeNQ() > 0) { return true; }
            }
            catch (Exception exc) { eh.errorHandling(exc); }
            return false;
        }

        public bool savePlayerToDB(IPlayer player)
        {
            try
            {
                if (!checkCon()) return false;

            }
            catch (Exception exc) { eh.errorHandling(exc); }
            return false;
        }

        public DataTable loadPlayerFromDB(IPlayer player)
        {
            try
            {
                if (!checkCon()) return null;
                string sql = "SELECT * FROM user WHERE socialid = " + player.SocialClubId;
                DataTable dt = getDataTable(sql);
                if (dt.Rows.Count > 0) return dt;
            }
            catch (Exception exc) { eh.errorHandling(exc); }
            return null;
        }

        #endregion

    }
}
