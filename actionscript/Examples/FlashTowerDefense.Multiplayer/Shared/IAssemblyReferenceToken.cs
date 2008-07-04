﻿using System;
using System.Collections.Generic;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.Shared;

namespace FlashTowerDefense.Shared
{
    /// <summary>
    /// Without this class some assemblies are not referenced as they only contain
    /// type mappings but no real type usage.
    /// </summary>
    public interface IAssemblyReferenceToken :
        ScriptCoreLib.Shared.Query.IAssemblyReferenceToken,
        ScriptCoreLib.Shared.IAssemblyReferenceToken
        //ScriptCoreLib.Nonoba.IAssemblyReferenceToken
    {
    }
}
