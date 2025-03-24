using Combat.PlayerTagSystem;
namespace Dialog.SituationControl
{

    public class PlayerMovementStoper : SituationElement
    {
        public override void StartSituation()
        {
            PlayerManager.Instance.StopPlayer();
        }
        
        public override void EndSituation()
        {

        }
    }
}