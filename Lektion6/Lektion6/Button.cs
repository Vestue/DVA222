using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lektion6
{
    internal class Button
    {
        public string Text { get; set; }

        public Button(string text) => Text = text;

        List<IMouseClick> MouseClicks = new List<IMouseClick>();
        public void Add(IMouseClick o) => MouseClicks.Add(o);
        public void Remove(IMouseClick o) => MouseClicks.Remove(o);
        public void FireMouseClick(MouseClickData data)
        {
            foreach (var o in MouseClicks) o.Click(data);
        }
    }
}
