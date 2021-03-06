﻿using AmeisenBotCore;
using AmeisenBotData;
using AmeisenBotFSM.Interfaces;
using System;
using static AmeisenBotFSM.Objects.Delegates;

namespace AmeisenBotFSM.Actions
{
    public class ActionIdle : IAction
    {
        private string[] randomEmoteList = {
            "dance",
            "shrug",
            "laugh",
            "train",
            "joke",
            "fart",
            "bravo",
            "chicken"
        };

        public Start StartAction { get { return Start; } }
        public DoThings StartDoThings { get { return DoThings; } }
        public Exit StartExit { get { return Stop; } }
        private long TickCountToExecuteRandomEmote { get; set; }
        private AmeisenDataHolder AmeisenDataHolder { get; set; }

        public ActionIdle(AmeisenDataHolder ameisenDataHolder)
        {
            AmeisenDataHolder = ameisenDataHolder;
            TickCountToExecuteRandomEmote = Environment.TickCount + new Random().Next(60000, 600000);
        }

        public void DoThings()
        {
            if (AmeisenDataHolder.IsAllowedToDoRandomEmotes)
            {
                DoRandomEmote();
            }
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }

        private void DoRandomEmote()
        {
            if (Environment.TickCount >= TickCountToExecuteRandomEmote)
            {
                AmeisenCore.LuaDoString($"DoEmote(\"{randomEmoteList[new Random().Next(randomEmoteList.Length)]}\");");
                TickCountToExecuteRandomEmote = Environment.TickCount + new Random().Next(60000, 600000);
            }
        }
    }
}