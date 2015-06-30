using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestAppEngineApplicationId
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201506/20150625/appengine


        public XElement title;

        public async Task yield()
        {
            title.Value = "hi";

            // https://code.google.com/p/googleappengine/source/browse/trunk/java/src/main/com/google/appengine/api/utils/SystemProperty.java?r=219
            // https://developers.google.com/appengine/docs/adminconsole/performancesettings

            //var environment = com.google.appengine.api.utils.SystemProperty.environment.value().value();

            var applicationId = com.google.appengine.api.utils.SystemProperty.applicationId.get();
            var applicationVersion = com.google.appengine.api.utils.SystemProperty.applicationVersion.get();


            title.Value = new
            {
                applicationId,
                applicationVersion
                //, environment 
            }.ToString();

        //    Y:\TestAppEngineApplicationId.ApplicationWebService\staging.java\web\java\__AnonymousTypes__TestAppEngineApplica
        //return __String.Format(null, "{{ applicationId = {0}, applicationVersion = {1} }}", objectArray4);

            //return "".AsResult();
        }
    }
}
