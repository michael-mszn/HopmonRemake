namespace Entities.PlayableCharacter
{
    /*
     * Some Tiles count as walkable tiles, but either not consistently or not in a specific movement context.
     * To accomodate for this during a movement input so the player can not step on such tile, this 
     * enum lists all conflicting tile tags. Therefore, the Open-Closed Principle of the Movement.cs
     * class is preserved in case future walkable tiles get added that need to be ignored during movement. 
     */
    public enum TilesToIgnore
    {
        DeactivatedTile
    }
}