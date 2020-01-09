using System;
using System.Collections;
using UnityEngine;

namespace BrunoMikoski.TextJuicer
{
    public sealed class TextJuicerSequence : MonoBehaviour
    {
        private Coroutine playCoroutine;

        [SerializeField] private bool playOnStart;

        [SerializeField] private TextJuicerSequenceItemData[] sequence;

        private void Start()
        {
            if (playOnStart)
                Play();
        }

        public void Play()
        {
            if (playCoroutine != null)
                StopCoroutine(playCoroutine);
            playCoroutine = StartCoroutine(PlayEnumerator());
        }

        private IEnumerator PlayEnumerator()
        {
            var playedItems = 0;
            while (playedItems < sequence.Length)
            {
                var textJuicerSequenceItemData = sequence[playedItems];

                textJuicerSequenceItemData.TextJuicer.Play();
                yield return textJuicerSequenceItemData.TextJuicer.WaitForCompletionEnumerator();
                yield return new WaitForSeconds(textJuicerSequenceItemData.AfterInterval);
                playedItems++;
            }
        }

        [Serializable]
        private struct TextJuicerSequenceItemData
        {
            [field: SerializeField] public TMP_TextJuicer TextJuicer { get; }

            [field: SerializeField] public float AfterInterval { get; }
        }
    }
}