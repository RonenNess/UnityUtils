UnityConstants is a code generator that produces a single file called `UnityConstants.cs` containing values for items you may modify in the editor. This tool currently generates constants for the following:

- Tags
- Sorting Layers
- Layers
- Scenes in the build configuration

You can run this tool by choosing `Generate UnityConstants.cs` from the `Edit` menu. When you first run the command it will prompt you for a directory in which to save `UnityConstants.cs`. Each time after this, it will find that file and replace it, making it very quick to re-run the process.

This is an example of what you'll see in `UnityConstants.cs`:

    namespace UnityConstants
    {
        public static class Tags
        {
            public const string Untagged = "Untagged";
            public const string Respawn = "Respawn";
            public const string Finish = "Finish";
            public const string EditorOnly = "EditorOnly";
            public const string MainCamera = "MainCamera";
            public const string Player = "Player";
            public const string GameController = "GameController";
            public const string Test = "Test";
            public const string AnotherTest = "Another Test";
            public const string _3Testing = "3 Testing";
        }

        public static class SortingLayers
        {
            public const int Default = 0;
            public const int Second = 1;
            public const int NewLayer3 = 3;
            public const int Another = 2;
            public const int NewLayer5 = 5;
            public const int Foreground = 7;
        }

        public static class Layers
        {
            public const int Default = 0;
            public const int TransparentFX = 1;
            public const int IgnoreRaycast = 2;
            public const int Water = 4;
            public const int Layer8 = 8;
            public const int Layer12 = 12;
        }

        public static class Scenes
        {
            public const int TestScene = 0;
            public const int TestScene2 = 1;
            public const int GameSaveSystemDemo = 2;
        }
    }
