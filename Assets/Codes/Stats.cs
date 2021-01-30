﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public bool isDead;

    public bool hasDeadAnim;

    public float scoreOnDeath;


    private void Start()
    {
    }

    public void TakeDmg(float dmg)
    {
        //particle effect/flash
        if (isDead)
            return;

        if(GetComponent<PlayerController>())
        {
            SoundManager.PlayASource("TakeDamage");
            PlayerController pc = GetComponent<PlayerController>();
        }

        health -= dmg;
        if (health <= 0 && !isDead)
            Dead();

        if (GetComponent<Enemy>())
            GetComponent<Enemy>().FlashHealthBar();
        else if (GetComponent<PlayerController>())
            GetComponent<PlayerController>().UpdateHealthImage();
    }

    public void Dead()
    {
        isDead = true;

        if (!GetComponent<PlayerController>())
            StartCoroutine(WaitDestroy());
        else
        {
            GetComponent<PlayerController>().Dead();
        }
    }

    IEnumerator WaitDestroy()
    {
        if (GetComponentInChildren<Animator>())
        {
            if(hasDeadAnim)
                GetComponentInChildren<Animator>().SetTrigger("Death");
        }
        

        //TODO: Spawn atte blood here

        if (GetComponent<Enemy>())
        {
            GetComponent<Enemy>().AllowMovement = false;
            GetComponent<Enemy>().healthBar.gameObject.SetActive(false);
            GetComponent<Enemy>().isEnabled = false;
            GetComponent<Enemy>().isEatable = true;

            if(GetComponentInChildren<Light>())
                GetComponentInChildren<Light>().gameObject.SetActive(false);


            FindObjectOfType<PlayerController>().score += scoreOnDeath;

            if(!GetComponent<Enemy>().isRanged)
                SoundManager.PlayASource("EnemyDeath");
            else
                SoundManager.PlayASource("EnemyDeath2");
        }  

        if (GetComponent<Gun>())
            GetComponent<Gun>().enabled = false;

        if (GetComponentInChildren<Sword>())
            GetComponentInChildren<Sword>().enabled = false;

        if(hasDeadAnim)
            yield return new WaitForSeconds(4);
        else
            yield return new WaitForSeconds(0.1f);

        //Destroy(gameObject);

        //make it eatable
    }
}
