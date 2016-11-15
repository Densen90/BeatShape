namespace BeatShape
{
    class Program
    {
        static void Main(string[] args)
        {
            using(Game game = new Game(800,600))
            {
                game.Run(60, 60);
            }
        }
    }
}
