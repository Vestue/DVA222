namespace Snake_DVA222
{
    internal static class Program
    {
        /// <summary>
        ///  Snake made by Group 22:
        ///  - Oscar Einarsson
        ///  - Casper Norén
        ///  - Ragnar Winblad von Walter
        ///  Casper 
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            
            Engine _engine = new Engine();
            _engine.Run();
        }
    }
}