using AlliedTionOOP.Engine;
using Microsoft.Xna.Framework.Graphics;

namespace AlliedTionOOP
{
    public class MainClass
    {
        public const int WindowWidth = 1280;
        public const int WindowHeight = 720;

        public const string GameWindowTitle = "Magic University v1.0";
        public const string LauncherWindowTitle = "Magic University Launcher";

        public const string Music = "../../../Content/Sound/valkyries.mp3";
        public const string GotItem = "../../../Content/Sound/successful2.mp3";
        public const string MapCoordinates = "../../../Content/map-coordinates.txt";
        public const string KillEnemy = "../../../Content/Sound/secret.mp3";

        public static readonly int CurrentScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static readonly int CurrentScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        private static void Main()
        {
            var engine = new GameEngine();

            using (engine)
            {
                engine.Run();
            }

        }
    }
}
