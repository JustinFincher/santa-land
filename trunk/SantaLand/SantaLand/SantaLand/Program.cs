using System;

namespace SantaLand
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SantaLand game = new SantaLand())
            {
                game.Run();
            }
        }
    }
#endif
}

