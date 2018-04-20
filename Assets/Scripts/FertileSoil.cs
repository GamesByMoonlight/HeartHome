using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertileSoil : MonoBehaviour {

    public GameObject ThrownSeedsPrefab;
    public GameObject SplashPrefab;

    bool seedsPlanted = false;
    GameObject seeds;
    GameObject splash;
    FlowerMaker flowerMaker;

    private void Awake()
    {
        flowerMaker = GetComponent<FlowerMaker>();
    }

    public void PlantSeeds()
    {
        seedsPlanted = true;
        seeds = Instantiate(ThrownSeedsPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f), transform);

    }

    internal void WaterSoil()
    {
        if (!seedsPlanted)
            return;

        splash = Instantiate(SplashPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f), transform);
        StartCoroutine(WaitForSplash(splash.GetComponent<Animator>()));
    }

    IEnumerator WaitForSplash(Animator animator)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        var anim = seeds.GetComponent<Animator>();
        anim.SetTrigger("Shrink");
        StartCoroutine(WaitForShrink(anim));
    }

    IEnumerator WaitForShrink(Animator animator)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        //Instantiate(FlowerPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f));
        flowerMaker.MakeFlowerAt(transform.position);
        Destroy(seeds);
        Destroy(splash);
    }
}
