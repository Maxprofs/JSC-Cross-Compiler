﻿using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var z = new { u = new { n = "c" } };

        var f = (
            from x in new xTable()

                //  where Contains()
            where x.Tag.ToLower().Contains(z.u.n)

            select x.field3
        ).Average();

    }
}
