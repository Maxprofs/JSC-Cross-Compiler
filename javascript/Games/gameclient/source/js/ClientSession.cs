using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Net;
using ScriptCoreLib.JavaScript.Runtime;

namespace gameclient.source.js
{
    using shared;

    [Script]
    public partial class ClientSession : ClientToServerBase, Message.IClient
    {

        public Controls.DemoControl Control;

        public void CreateExplosionByServer(int x, int y, string text)
        {
            Control.DrawExplosion(x, y);

            Console.WriteLine("create the damn explosion at " + x + ", " + y);
        }

        public void DisplayNotification(string text, int color)
        {
            Control.DisplayNotification(text, (Color)color);
        }

        public void ForceReload()
        {
            Console.WriteLine("server told us to reload!");

            Native.Document.location.reload();
        }

    }
}
