﻿using ScriptCoreLib.Query.Experimental;
using System;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            select Tuple.Create(x.field1, x.Tag)

        ).FirstOrDefault();

        //var z = f.x.field1;

    }
}
