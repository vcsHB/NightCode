
#if UNITY_EDITOR
using UnityEngine.UIElements;

namespace Dialog
{
    public class SplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<SplitView, TwoPaneSplitView.UxmlTraits> { }
        public new class UxmlTraits : TwoPaneSplitView.UxmlTraits { }
    }
}


#endif