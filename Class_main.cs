using System;
using System.Collections.Generic;

using AltV;
using AltV.Net.Elements.Entities;
using AltV.Net.Data;
using AltV.Net;
using AltV.Net.Enums;
using AltV.Net.Async;

namespace roleplay
{
    public class Server : Resource
    {

        #region globals

        Class_errorhandling eh;
        Class_mysql mysql;
        List<Class_player> playerlist;

        #endregion

        #region overrides

        public override void OnStart()
        {
            try
            {
                Console.WriteLine("Roleplay Loading...");

                eh = new Class_errorhandling();
                mysql = new Class_mysql(eh);
                mysql.openCon();

                if (mysql.checkCon())
                {
                    Console.WriteLine("Mysql Connection was successful");
                }
                else
                {
                    Console.WriteLine("Mysql Connection was not successful");
                }

                playerlist = new List<Class_player>();

                Alt.OnPlayerConnect += Alt_OnPlayerConnect;
                Alt.OnPlayerDisconnect += Alt_OnPlayerDisconnect;

                Console.WriteLine("Roleplay Loaded");
            }
            catch (Exception exc) { eh.errorHandling(exc); }
        }

        public override void OnStop()
        {
            try
            {
                mysql.closeCon();
                Console.WriteLine("Roleplay Stopped");
            }
            catch (Exception exc) { eh.errorHandling(exc); }
        }

        public override void OnTick()
        {
            try
            {
                //Alt.Log("ontick");
            }
            catch (Exception exc) { eh.errorHandling(exc); }
            
        }

        #endregion

        #region onplayerstuff

        private void Alt_OnPlayerConnect(IPlayer player, string reason)
        {
            try
            {
                if (!mysql.isPlayerInDB(player))
                {
                    player.Emit("client:showregister");
                }
                else
                {
                    player.Emit("client:showlogin");
                }
            }
            catch (Exception exc) { eh.errorHandling(exc); }
        }

        private void Alt_OnPlayerDisconnect(IPlayer player, string reason)
        {
            try
            {
                mysql.savePlayerToDB(player);
            }
            catch (Exception exc) { eh.errorHandling(exc); }
        }

        #endregion

    }
}
