using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HoldingMoney : MonoBehaviour
{
    private GlobalEventManager gem;

    public GameObject rightArm;
    public GameObject leftArm;
    public GameObject vault;

    //Sound
    public AudioClip stoleMoney;
    private AudioSource playerAudio;

    private void Awake()
    {
        List<MonoBehaviour> deps = new List<MonoBehaviour>
        {
            (gem = FindObjectOfType(typeof(GlobalEventManager)) as GlobalEventManager),
        };
        if (deps.Contains(null))
        {
            throw new Exception("Could not find dependency");
        }
    }

    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        gem.StartListening("StoleVault", VaultStolen);
    }
    private void OnDestroy()
    {
        gem.StopListening("StoleVault", VaultStolen);
    }

    private void VaultStolen(GameObject target, List<object> parameters)
    {
        if (target != gameObject)
        {
            return;
        }
        playerAudio.PlayOneShot(stoleMoney, 0.005f);
        rightArm.SetActive(true);
        leftArm.SetActive(true);
        vault.SetActive(true);
    }
}
