using System;
using Components;
using Components.Spawn;

namespace Creatures.CreatureVFX
{
    [Serializable]
    public struct ParticleEntry
    {
        public ParticleType type;
        public SpawnComponent component;
    }
}