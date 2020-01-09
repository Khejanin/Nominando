using TMPro;
using UnityEngine;

namespace BrunoMikoski.TextJuicer.Modifiers
{
    [AddComponentMenu("UI/Text Juicer Modifiers/Per Character Color", 11)]
    public sealed class TextJuicerPerCharacterColorModifier : TextJuicerVertexModifier
    {
        [SerializeField] private Color[] colors;

        [SerializeField] private AnimationCurve curve = new AnimationCurve(new Keyframe(0, 0));

        private Color32[] newVertexColors;

        public override bool ModifyGeometry => false;

        public override bool ModifyVertex => true;


        public override void ModifyCharacter(CharacterData characterData, TMP_Text textComponent, TMP_TextInfo textInfo,
            float progress, TMP_MeshInfo[] meshInfo)
        {
            if (colors == null || colors.Length == 0)
                return;

            var materialIndex = characterData.MaterialIndex;

            newVertexColors = textInfo.meshInfo[materialIndex].colors32;

            var vertexIndex = characterData.VertexIndex;


            var targetColor = colors[characterData.Index % colors.Length];
            Color currentColor = newVertexColors[0];


            targetColor = targetColor * curve.Evaluate(characterData.Progress);
            currentColor = currentColor * (1f - curve.Evaluate(characterData.Progress));

            newVertexColors[vertexIndex + 0] = currentColor + targetColor;
            newVertexColors[vertexIndex + 1] = currentColor + targetColor;
            newVertexColors[vertexIndex + 2] = currentColor + targetColor;
            newVertexColors[vertexIndex + 3] = currentColor + targetColor;
        }
    }
}