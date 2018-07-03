using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableStar : MonoBehaviour {

    
    public bool IveBeenClicked { get; set; }
    public bool HasBeenFound { get; set; }
    public bool IsConnected { get; set; }


    public void UpdateStatus()
    {
        ParticleSystem particles = GetComponent<ParticleSystem>();

        if (HasBeenFound)
        {
            var main = particles.main;

            main.maxParticles = 2;
        }

        // TODO Connect a line to all stars which have a Found Neighbor
    }
}
