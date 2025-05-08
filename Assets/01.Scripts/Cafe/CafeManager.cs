namespace Base.Cafe
{
    public class CafeManager : MonoSingleton<CafeManager>
    {
        public CafeSO cafeSO;
        public BaseInput input;
        public MSGText msgText;
        public OmeletRiceMiniGame omeletRiceMiniGame;
        public Cafe cafe;

        public bool IsCafeOpen { get; private set; }
        public float CurrentTime { get; private set; }


        public void Init(CafeSO cafeInfo)
        {
            input.DisableInput();
            cafe.Init(cafeInfo);
        }


        #region CafeFlow

        public void StartCafe()
        {
            IsCafeOpen = true;
            cafe.StartCustomerWave();
            input.EnableInput();
        }

        #endregion
    }
}
