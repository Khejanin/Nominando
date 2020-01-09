using System;
using UnityEngine;

namespace BrunoMikoski.TextJuicer
{
    [Serializable]
    public struct CharacterData
    {
        public float Progress { get; private set; }

        private readonly float startingTime;
        private readonly float totalAnimationTime;
        private readonly bool playForever;
        private float lastTime;
        private float playingTime;

        public int MaterialIndex { get; }

        public int VertexIndex { get; }

        public int Index { get; }

        public CharacterData(int targetIndex, float startTime, float targetAnimationTime,
            bool isPlayForever, int targetMaterialIndex, int targetVertexIndex)
        {
            Index = targetIndex;
            Progress = 0.0f;
            startingTime = startTime;
            totalAnimationTime = targetAnimationTime;
            playForever = isPlayForever;
            VertexIndex = targetVertexIndex;
            MaterialIndex = targetMaterialIndex;
            lastTime = 0;
            playingTime = 0;
        }

        public void UpdateTime(float time)
        {
            if (lastTime == 0.0f)
            {
                playingTime = totalAnimationTime - startingTime % totalAnimationTime;
                lastTime = time;
                return;
            }

            /*if ( time < startingTime )
            {
                progress = 0;
                return;
            }*/

            //float currentProgress = (time - startingTime) / totalAnimationTime;
            playingTime += time - lastTime;
            var currentProgress = playingTime / totalAnimationTime;

            if (!playForever)
                currentProgress = Mathf.Clamp01(currentProgress);

            Progress = currentProgress;
            if (playingTime >= totalAnimationTime) playingTime = 0;
            lastTime = time;
        }
    }
}