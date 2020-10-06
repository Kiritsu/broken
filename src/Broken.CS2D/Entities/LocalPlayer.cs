using Broken.CS2D.Enums;

namespace Broken.CS2D.Entities
{
    public class LocalPlayer
    {
        /// <summary>
        ///     Gets the id of the player in the game.
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        ///     Gets the team of the player. 
        /// </summary>
        public PlayerTeam Team { get; set; }

        /// <summary>
        ///     Gets the position X of the player.
        /// </summary>
        public float PositionX { get; set; }

        /// <summary>
        ///     Gets the position Y of the player.
        /// </summary>
        public float PositionY { get; set; }
  
        /// <summary>
        ///     Gets the mouse rotation of the player. (from -90 to 270)
        /// </summary>
        public float MouseRotation { get; set; }
        
        /// <summary>
        ///     Gets the weapon of the player.
        /// </summary>
        public PlayerWeapon Weapon { get; set; }
        
        /// <summary>
        ///     Gets the money of the player.
        /// </summary>
        public PlayerMoney Money { get; set; }
        
        /// <summary>
        ///     Gets the health of the player.
        /// </summary>
        public PlayerHealth Health { get; set; }
    }
}