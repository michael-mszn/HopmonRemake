namespace Entities.InteractableObject.EnergyBall
{
    /*
     * A list of all entity tags where the energy ball will disappear if it
     * collides with it. Preserves Open-Closed principle of EnergyBall.cs in case
     * future entities get added that the energyball can not go through nor destroy.
     */
    public enum NonPenetrables
    {
        Wall
    }
}