using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour {

    public ParticleSystem shape, trail, burst;
    public float deathCountdown = -1f;

    private Player player;

    private void Awake()
    {
        player = transform.root.GetComponent<Player>();
    }

    private void Update()
    {
        if (deathCountdown >= 0f)
        {
            deathCountdown -= Time.deltaTime;
            if(deathCountdown <= 0f)
            {
                deathCountdown = -1;
                shape.EnableEmission(true);
                trail.EnableEmission(true);
                player.Die();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (deathCountdown < 0f)
        {
            shape.EnableEmission(false);
            trail.EnableEmission(false);
            burst.Emit(burst.main.maxParticles);
            deathCountdown = burst.main.startLifetime.constantMax;
        }
    }

}
