namespace Combat.SubWeaponSystem
{

    public class GrenadeBombWeapon : WeaponObjectPoolingWeapon
    {
        
        public override void UseWeapon(SubWeaponControlData data)
        {
            data.direction.y += 5f;
            base.UseWeapon(data);
            
        }
    }
}