using System.Windows.Forms;

namespace Bounce
{
	class MainClass
    {
        public Engine Engine // LADES TILL AV CLASS DIAGRAM ??
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
