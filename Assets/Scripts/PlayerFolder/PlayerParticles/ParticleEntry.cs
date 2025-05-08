using System;
using Components;

namespace PlayerFolder.PlayerParticles
{
    [Serializable]
    public struct ParticleEntry
    {
        public ParticleType type;
        public SpawnComponent component;
    }
}