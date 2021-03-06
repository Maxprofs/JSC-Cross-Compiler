﻿using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.DataTableCollection))]
    internal class __DataTableCollection : __InternalDataCollectionBase
    {
        public List<DataTable> InternalList = new List<DataTable>();

        public override global::System.Collections.IEnumerable GetInternalList()
        {
            return InternalList;
        }


        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201311/20131107/assetslibrary

        public void Add(DataTable table)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView\DataGridView.DataSource.cs

          
            this.InternalList.Add(table);
        }

        public static implicit operator DataTableCollection(__DataTableCollection e)
        {
            return (DataTableCollection)(object)e;
        }

        public DataTable this[string name]
        {
            get
            {
                // tested by
                // X:\jsc.svn\examples\javascript\WebGL\WebGLGoldDropletTransactions\WebGLGoldDropletTransactions\Application.cs

                return this.InternalList.FirstOrDefault(k => k.TableName == name);
            }
        }

        public DataTable this[int index]
        {
            get
            {
                return this.InternalList[index];
            }
        }

    }
}
