using System.Collections.Generic;
using UnityEngine;
namespace DefaultNamespace
{
    [CreateAssetMenu]
    public class IslandPaths : ScriptableObject
    {
        public Dictionary<Island, ShipPath> IslandSplines = new Dictionary<Island, ShipPath>();

        public void EnableVisualsForIsland(Island _island)
        {
            IslandSplines[_island].EnableVisuals();
        }

        public void DisableVisualsForIsland(Island _island)
        {
            IslandSplines[_island].DisableVisuals();
        }
    }
}
