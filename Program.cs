namespace Insomnia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Insomnia";
            var write = new Write();
            write.Setup();
            Thread.Sleep(5000);
            write.Execute();
        }
    }
}