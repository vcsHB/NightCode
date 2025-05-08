using System.Collections.Generic;
using UnityEngine;

namespace Base.Cafe
{
    public class CafeMaidManager : MonoSingleton<CafeMaidManager>
    {
        public List<CafeMaidSO> maidInfoList;
        private List<CafeMaid> _maidInstanceList;

        public void Init()
        {
            _maidInstanceList = new List<CafeMaid>();
            maidInfoList.ForEach(info =>
            {
                CafeMaid cafeMaid = Instantiate(info.maidPf, transform);
                cafeMaid.Init(info);

                _maidInstanceList.Add(cafeMaid);
            });
        }

        public bool TryAssignWorkToMaid(CafeSit sit)
        {
            bool canAssignWork = false;

            _maidInstanceList.ForEach(maid =>
            {
                if (maid.IsDoService == false)
                {
                    canAssignWork = true;
                    maid.AssignWork(sit);
                    return;
                }
            });

            return canAssignWork;
        }
    }
}
