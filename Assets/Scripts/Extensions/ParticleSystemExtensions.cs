using UnityEngine;

public static class ParticleSystemExtensions {

    public static void EnableEmission(this ParticleSystem particleSystem, bool enabled)
    {
        var emission = particleSystem.emission;
        emission.enabled = enabled;
    }
}
