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

        protected override void Awake()
        {
            base.Awake();
            input.DisableInput();
            cafe.Init(cafeSO);
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
