using SDG.Unturned;
using System;
using System.Reflection;
using UnityEngine;
using uScript.API.Attributes;
using uScript.Core;
using uScript.Module.Main.Classes;
using uScript.Unturned;

namespace onSalvageEventModule
{
    public class Main
    {
        [ScriptEvent("onBarricadeSalvaged", "player, barricade, *cancel")]
        public class BarricadeSalvagedEvent : ScriptEvent
        {
            public override EventInfo EventHook(out object instance)
            {
                instance = null;
                return typeof(BarricadeDrop).GetEvent("OnSalvageRequested_Global", BindingFlags.Public | BindingFlags.Static);
            }

            [ScriptEventSubscription]
            public void BarricadeSalvaged(BarricadeDrop barricadeDrop, SteamPlayer instigatorClient, ref bool shouldAllow)
            {
                var args = new ExpressionValue[]
                {
                    ExpressionValue.CreateObject(new PlayerClass(PlayerTool.getPlayer(instigatorClient.playerID.steamID))),
                    ExpressionValue.CreateObject(new BarricadeClass(barricadeDrop.model)),
                    !shouldAllow
                };

                RunEvent(this, args);
                shouldAllow = !args[2];
            }
        }

        [ScriptEvent("onStructureSalvaged", "player, structure, *cancel")]
        public class StructureSalvagedEvent : ScriptEvent
        {
            public override EventInfo EventHook(out object instance)
            {
                instance = null;
                return typeof(StructureDrop).GetEvent("OnSalvageRequested_Global", BindingFlags.Public | BindingFlags.Static);
            }

            [ScriptEventSubscription]
            public void StructureSalvaged(StructureDrop structureDrop, SteamPlayer instigatorClient, ref bool shouldAllow)
            {
                var args = new ExpressionValue[]
                {
                    ExpressionValue.CreateObject(new PlayerClass(PlayerTool.getPlayer(instigatorClient.playerID.steamID))),
                    ExpressionValue.CreateObject(new StructureClass(structureDrop.model)),
                    !shouldAllow
                };

                RunEvent(this, args);
                shouldAllow = !args[2];
            }
        }
    }
}
