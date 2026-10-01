using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SndLib
{
    static class MessageUtil
    {
        public static void Error(string message)
        {
            MessageBox.Show(message, "Deu zica", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Warning(string message)
        {
            MessageBox.Show(message, "Deu zica", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
