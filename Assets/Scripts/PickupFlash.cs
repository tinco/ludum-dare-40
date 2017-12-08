using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFlash : MonoBehaviour {

    public Light lightFlash;
    public ParticleSystem particleFlash;

    private float time;
    private bool isActive = false;

    public void ActivateFlash()
    {
        time = 0;

        particleFlash.enableEmission = true;
        particleFlash.Simulate(0.0f, true, true);
        particleFlash.Play();
        lightFlash.enabled = true;
        isActive = true;
    }

    // Update is called once per frame
    void Update() {
        if (isActive) {
            time += Time.deltaTime;

            lightFlash.intensity = 1*Mathf.Cos(time*1.5F - 0.4F);

            if (time > 1.5F)
            {
                lightFlash.enabled = false;
                isActive = false;
            }
        }
    }
}
