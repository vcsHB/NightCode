using DG.Tweening;
using UnityEngine;

namespace Dialog.Animation
{
    [CreateAssetMenu(menuName = "SO/TextAnim/SizeAnimation")]
    public class SizeAnimationSO : TextAnimationSO
    {
        public float duration = 1f;
        public float amplitude;

        public override void ApplyEffortToCharacter(CharacterData characterData, TMP_AnimationPlayer player)
        {
            for (int i = 0; i < characterData.current.positions.Length; i++)
            {
                Vector3 middlePos = (characterData.current.positions[0] + characterData.current.positions[2] ) / 2;
                Vector3 current = characterData.current.positions[i];

                characterData.current.positions[i] = 
                    Vector3.LerpUnclamped(current, 
                    middlePos, 
                    Mathf.Pow((1 - (characterData.timer / duration)),2) * amplitude);
            }
        }

        public override bool SetParameter(string parameters)
        {
            return true;
        }
    }
}
