using EFT;
using EFT.Interactive;
using System;

namespace UnhandledException
{
    public class Types
    {
        public static Type ObserverCorpse = new ObservedCorpse().GetType();
        public static Type Corpse = new Corpse().GetType();
        public static Type ObservedLootItem = new ObservedLootItem().GetType();
        public static Type LootItem = new LootItem().GetType();
        public static Type Player = new Player().GetType();
        // public static Type Throwable = new Throwable().GetType(); // no interface for this
    }
}
