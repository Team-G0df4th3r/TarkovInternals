using UnityEngine;

namespace UnhandledExceptionHandler
{
    public class Statics
    {
        public class Colors
        {
            public static Color White = new Color(1f, 1f, 1f, 1f);
            public static Color Black = new Color(0f, 0f, 0f, 1f);
            public static Color Red = new Color(1f, 0f, 0f, 1f);
            public class ESP
            {
                public static Color[] player = new Color[2] { new Color(1.0f, 0f, 0f, 1.0f), new Color(1.0f, 0f, 1.0f, 1.0f) };
                public static Color npc = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                public static Color group = new Color(0f, 0.8f, 1.0f, 1.0f);
                public static Color scav_player = new Color(1f, 0.75f, 0.0f, 1.0f);
                public static Color grenades = new Color(0f, 1.0f, 0f, 1.0f);
                public static Color bodies = new Color(0.9f, 0.9f, 0.9f, 1.0f);
                public static Color items = new Color(1f, 1f, 1f, .7f);
                public static Color hider = new Color(0f, 0f, 0f, 1f);
            }
        }
    }
}
