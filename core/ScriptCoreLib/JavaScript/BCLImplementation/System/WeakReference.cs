using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/weakreference.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/WeakReference.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/WeakReference.cs

    [Script(Implements = typeof(global::System.WeakReference))]
    internal class __WeakReference
    {
        // how would this work for cross device threads?
        // how would we be able to implement it for other VMs ?

        // http://www.nczonline.net/blog/2012/11/06/ecmascript-6-collections-part-3-weakmaps/?utm_source=feedburner&utm_medium=feed&utm_campaign=Feed%3A+nczonline+%28NCZOnline+-+The+Official+Web+Site+of+Nicholas+C.+Zakas%29

        public __WeakReference(object e)
        {
            // weak reference not supported
        }
    }
}
