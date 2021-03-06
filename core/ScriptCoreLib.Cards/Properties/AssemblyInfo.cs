﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using ScriptCoreLib;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ScriptCoreLib")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("neutronic")]
[assembly: AssemblyProduct("ScriptCoreLib")]
[assembly: AssemblyCopyright("Copyright © neutronic 2006")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM componenets.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("ad497913-358e-4054-b2e3-98d7bd1e0031")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("3.0.*")]
[assembly: AssemblyFileVersion("3.0.*")]

[assembly: Script(IsCoreLib = true)]

[assembly: ScriptTypeFilter(ScriptType.JavaScript, "*.JavaScript.Cards")]
[assembly: ScriptTypeFilter(ScriptType.JavaScript, "*.Shared.Cards")]

[assembly: ScriptTypeFilter(ScriptType.JavaScript, "ScriptCoreLib.Shared.Cards")]

/*
[assembly: ScriptTypeFilter(ScriptType.PHP, "*.*.PHP")]
[assembly: ScriptTypeFilter(ScriptType.PHP, "*.*.Shared")]
*/

namespace ScriptCoreLib.Shared.Cards
{
    [Script]
    public class AssemblyInfo : ScriptCoreLib.Shared.IAssemblyInfo
    {
        public static AssemblyInfo Current = new AssemblyInfo();

        #region BuildDateTimeString
        /// <summary>
        /// date when library was compiled
        /// </summary>
        public string BuildDateTimeString
        {
            [Script(
                UseCompilerConstants = true,
                OptimizedCode = @"return '{BuildDate} UTC';"
                )]
            get
            {
                return default(string);
            }
        }
        #endregion

        #region ModuleName
        public string ModuleName
        {
            [Script(
                UseCompilerConstants = true,
                OptimizedCode = @"return '{Module.Name}';"
                )]
            get
            {
                return default(string);
            }
        }
        #endregion
    }
}
