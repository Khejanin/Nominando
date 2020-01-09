using UnityEngine;

namespace BrunoMikoski.TextJuicer
{
    public sealed class TextJuicer_WaitForCompletion : CustomYieldInstruction
    {
        private readonly TMP_TextJuicer textJuicer;

        public TextJuicer_WaitForCompletion(TMP_TextJuicer targetTextJuicer)
        {
            textJuicer = targetTextJuicer;
        }

        public override bool keepWaiting => textJuicer.IsPlaying && textJuicer.Progress < 1;
    }
}