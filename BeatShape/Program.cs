namespace BeatShape
{
    class Program
    {
        static void Main(string[] args)
        {
            using(Game game = new Game(1280,768))
            {
                game.Run(60, 60);
            }
        }
    }
}
