
internal class Program
{
    private static void Main(string[] args)
    {
        using var game = new PONG.Game1();
        game.Run();
    }
}