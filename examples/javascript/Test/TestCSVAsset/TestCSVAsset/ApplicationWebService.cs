using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestCSVAsset
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public  class ApplicationWebService
    {
        public async Task<DataTable> GetFoo()
        {
            return TestCSVAsset.Data.foo.GetDataTable();
        }

    }
}
