using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SndLib
{
    public class SndAudio : IDisposable
    {
        internal FMOD.Sound _sound;
        internal static FMOD.Channel _channel = null;

        private void liberaMemoriaAi()
        {
            if (_sound == null)
                return;
            _sound.release();
            _sound = null;
        }

        public SndAudio()
        {
            SndEngine._soundList.Add(this);
        }
        public void Dispose()
        {
            SndEngine._soundList.Remove(this);
            liberaMemoriaAi();
        }

        public void CarregaAiDJ(string filename)
        {
            // Validação e integridade
            liberaMemoriaAi();
            if (!File.Exists(filename))
            {
                MessageUtil.Error("PORQUE TU QUER CARREGAR ALGO QUE NAO EXISTE????");
                return;
            }
            // Carregar
            SndEngine._system.createSound(filename, (FMOD.MODE._2D | FMOD.MODE.HARDWARE | FMOD.MODE.CREATESTREAM), ref _sound);
        }

        public void SomNaCaixaDJ()
        {
            if (_sound == null)
                return;
            SndEngine._system.playSound(FMOD.CHANNELINDEX.FREE, _sound, false, ref _channel);
        }

        public bool Loop
        {
            get
            {
                if (_sound != null)
                    return false;
                FMOD.MODE m = 0;
                _sound.getMode(ref m);
                return (m & FMOD.MODE.LOOP_NORMAL) == FMOD.MODE.LOOP_NORMAL;
            }
            set
            {
                _sound.setMode(value ? FMOD.MODE.LOOP_NORMAL : FMOD.MODE.LOOP_OFF);
            }
        }
    }
}
