using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using System;

namespace roleplay
{
    class Class_player
    {

        #region globals

        private Class_errorhandling eh;

        public IPlayer player { get; }
        public bool admin { get; set; }

        #endregion

        #region constructor

        public Class_player(Class_errorhandling eh, IPlayer player)
        {
            this.eh = eh;
            this.player = player;
            this.admin = false;
        }

        #endregion

        #region playerstuff

        public void spawnPlayer(IPlayer player)
        {
            try
            {
                player.Model = (uint)PedModel.PrologueMournMale01;
                player.Spawn(new Position(0, 0, 72));
            }
            catch (Exception exc) { eh.errorHandling(exc); }
        }

        #endregion

    }
}
