using Broken.CS2D.Enums;
using Broken.Enums;

namespace Broken.Entities
{
    public class PlayerWeapon
    {
        /// <summary>
        ///     Gets the weapon worn by the player.
        /// </summary>
        public Weapon WeaponId { get; init; }
        
        /// <summary>
        ///     Gets the max amount of available ammo.
        /// </summary>
        public int MaxAmmo { get; init; }
        
        /// <summary>
        ///     Gets the current amount of ammo.
        /// </summary>
        public int CurrentAmmo { get; init; }

        /// <summary>
        ///     Gets the current mode of the weapon. (zoom / silencer, etc.)
        /// </summary>
        public int WeaponMode { get; init; }
    }
}