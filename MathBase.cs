using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyExamApp.Pages.Subjects
{
    class MathBase : Constructor
    {
        public static ushort[] Time = { 3, 30 };

        public static WrapPanel CreateVariant()
        {
            var panel = new WrapPanel();

            return panel;
        }
    }
}
