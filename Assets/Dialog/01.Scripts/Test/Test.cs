using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private string _testTxt;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                TestTagParse();
            }
        }

        private void TestTagParse()
        {
            List< TagAnimation> anims = TagParser.ParseAnimation(ref _testTxt);
            Debug.Log(_testTxt);

            foreach (var anim in anims)
            {
                Debug.Log(anim.tagType);
                Debug.Log(anim.animStartPos + " " + anim.animLength + " " + anim.Param);
            }
        }
    }
}
