﻿using ScriptCoreLib.Query.Experimental;

class Program
{
    static void Main(string[] args)
    {
        var f = (
            from x in new xTable()

            let c = (
                 from z in new xTable()
                 //where z.field1 == x.field1
                 select z
             )


             // um. data layer would need to ask each element of the row right?
            let cc = c.FirstOrDefault()

            select cc

        ).FirstOrDefault();

    }
}