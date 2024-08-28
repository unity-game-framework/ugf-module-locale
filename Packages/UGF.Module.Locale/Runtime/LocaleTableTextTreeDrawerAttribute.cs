using System;
using UnityEngine;

namespace UGF.Module.Locale.Runtime
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LocaleTableTextTreeDrawerAttribute : PropertyAttribute
    {
        public int RowHeightLines { get; }

        public LocaleTableTextTreeDrawerAttribute(int rowHeightLines = 4)
        {
            if (rowHeightLines < 1) throw new ArgumentOutOfRangeException(nameof(rowHeightLines));

            RowHeightLines = rowHeightLines;
        }
    }
}
