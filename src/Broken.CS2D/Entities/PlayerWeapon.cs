using Broken.CS2D.Enums;

namespace Broken.CS2D.Entities
{
    public class PlayerWeapon
    {
        /// <summary>
        ///     Gets the weapon worn by the player.
        /// </summary>
        public Weapon WeaponId { get; set; }
        
        /// <summary>
        ///     Gets the max amount of available ammo.
        /// </summary>
        public int MaxAmmo { get; set; }
        
        /// <summary>
        ///     Gets the current amount of ammo.
        /// </summary>
        public int CurrentAmmo { get; set; }

        /// <summary>
        ///     Gets the current mode of the weapon. (zoom / silencer, etc.)
        /// </summary>
        public int WeaponMode { get; set; }
    }
}