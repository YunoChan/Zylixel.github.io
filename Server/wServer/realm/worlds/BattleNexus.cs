﻿namespace wServer.realm.worlds
{
    public class BattleNexus : World
    {
        public BattleNexus()
        {
            Name = "BattleNexus";
            ClientWorldName = "Battle for the Nexus";
            Background = 0;
            AllowTeleport = false;
            Difficulty = 5;
        }

        protected override void Init()
        {
            LoadMap("wServer.realm.worlds.maps.BattlefortheNexus.jm", MapType.Json);
        }
    }
}
