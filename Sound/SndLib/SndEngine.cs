using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SndLib
{
    public static class SndEngine
    {
        // Internal vars
        internal static FMOD.System _system = null;
        internal static List<SndAudio> _soundList = null;

        // Internal methods
        internal static void ERRCHECK(FMOD.RESULT result)
        {
            if (result != FMOD.RESULT.OK)
            {
                MessageUtil.Error("[FMOD ERROR], {" + result + "} - " + FMOD.Error.String(result));
                Environment.Exit(-1);
            }
        }

        /// <summary>
        /// Inicia a engine de som.
        /// </summary>
        public static void VaiSom()
        {
            if (_system != null)
            {
                MessageUtil.Error("Inicia só uma vez carai!");
                return;
            }
            _soundList = new List<SndAudio>();
            uint version = 0;
            FMOD.RESULT result;

            /*
                Global Settings
            */
            result = FMOD.Factory.System_Create(ref _system);
            ERRCHECK(result);

            result = _system.getVersion(ref version);
            ERRCHECK(result);
            if (version < FMOD.VERSION.number)
            {
                MessageUtil.Error("Error!  You are using an old version of FMOD " + version.ToString("X") + ".  This program requires " + FMOD.VERSION.number.ToString("X") + ".");
                Application.Exit();
            }

            result = _system.init(1, FMOD.INITFLAGS.NORMAL, (IntPtr)null);
            ERRCHECK(result);
        }

        /// <summary>
        /// Termina a engine de som
        /// </summary>
        public static void DeuDeSom()
        {
            FMOD.RESULT result;
            if (_system != null)
            {
                result = _system.close();
                ERRCHECK(result);
                result = _system.release();
                ERRCHECK(result);
            }
        }

        /// <summary>
        /// Atualiza a Thread de som
        /// </summary>
        public static void ContinuaTocandoPorra()
        {
            if (_system != null)
                _system.update();
        }
    }
}
