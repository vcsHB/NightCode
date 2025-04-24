using System;
using UnityEngine;

namespace Cafe
{
    public class CafeMaid : CafeEntity
    {
        private CafeMaidSO _maidInfo;
        private CafeSit _targetSit;
        private bool _isDoService;

        public bool IsDoService => _isDoService;


        public void Init(CafeMaidSO maidInfo)
        {
            _maidInfo = maidInfo;
        }

        internal void AssignWork(CafeSit sit)
        {
            _isDoService = true;
            _targetSit = sit;
        }
    }
}
