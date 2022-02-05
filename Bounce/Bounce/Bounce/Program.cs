using System.Windows.Forms;

namespace Bounce
{
	class MainClass
    {
        public Engine Engine
        {
            get => default;
            set
            {
            }
        }

        public static void Main(string[] args)
		{
			Engine engine = new Engine();
			engine.Run();
        }
    }
}
