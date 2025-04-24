using UnityEngine;

namespace Dialog.Animation
{
    public abstract class TextAnimationSO : ScriptableObject
    {
        public string TagID;

        public abstract bool SetParameter(string parameters);

        public abstract void ApplyEffortToCharacter(CharacterData characterData, TMP_AnimationPlayer player);
    }
}
