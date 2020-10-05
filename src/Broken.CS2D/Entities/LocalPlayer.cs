using Broken.CS2D.Enums;

namespace Broken.CS2D.Entities
{
    public class LocalPlayer
    {
        /// <summary>
        ///     Gets the id of the player in the game.
        /// </summary>
        public int PlayerId { get; init; }

        /// <summary>
        ///     Gets the team of the player. 
        /// </summary>
        public PlayerTeam Team { get; init; }

        /// <summary>
        ///     Gets the position X of the player.
        /// </summary>
        public float PositionX { get; init; }

        /// <summary>
        ///     Gets the position Y of the player.
        /// </summary>
        public float PositionY { get; init; }
  
        /// <summary>
        ///     Gets the mouse rotation of the player. (from -90 to 270)
        /// </summary>
        public float MouseRotation { get; init; }
        
        /// <summary>
        ///     Gets the weapon of the player.
        /// </summary>
        public PlayerWeapon Weapon { get; init; }
        
        /// <summary>
        ///     Gets the money of the player.
        /// </summary>
        public PlayerMoney Money { get; init; }
        
        /// <summary>
        ///     Gets the health of the player.
        /// </summary>
        public PlayerHealth Health { get; init; }
    }
}