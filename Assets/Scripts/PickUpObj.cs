using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObj : MonoBehaviour
{
    public bool isEat;
    public bool isXPCoins;
    public bool isDecore;
    public bool isGroundObj;
    public bool isCanChangeColor;
    public int eat_Lvl = 1;
    public int xp_Lvl = 1;
    public GameObject particlePrefab;

    public int rangeSpawnObj; // Значение, в диапазон которого будут раскидываться случайно предметы
    private void Start()
    {
        if (isCanChangeColor)
            transform.GetChild(0).GetComponent<Renderer>().material.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);

        if (isDecore) //Random.Range(0, 360)
            transform.Rotate(Vector3.up * Random.Range(0, 360));


        if (isEat || isXPCoins)
        {
            int lvlEatXP = Random.Range(1, 1000);
            if (lvlEatXP >= 600 && lvlEatXP <= 700)
            {
                eat_Lvl = 2;
                xp_Lvl = 2;
                GetComponentInChildren<Renderer>().material.color = new Color32(43, 255, 0, 255); // Зелёный
            }
            if (lvlEatXP > 700 && lvlEatXP <= 800)
            {
                eat_Lvl = 3;
                xp_Lvl = 3;
                GetComponentInChildren<Renderer>().material.color = new Color32(155, 75, 75, 255); // Коричневый
            }
            if (lvlEatXP > 800 && lvlEatXP <= 900)
            {
                eat_Lvl = 4;
                xp_Lvl = 4;
                GetComponentInChildren<Renderer>().material.color = new Color32(0, 255, 255, 255); // Синий
            }

            if (lvlEatXP == 999)
            {
                eat_Lvl = 10;
                xp_Lvl = 10;
                if (isEat)
                    transform.GetComponent<Renderer>().material.color = new Color32(255, 255, 0, 255); // Желтый
                if (isXPCoins)
                    transform.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 255); // Красный
            }
        }


        rangeSpawnObj = GameObject.FindGameObjectWithTag("Player").GetComponent<SpawnObjects>().rangeSpawnObj;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PickUpObj>())
            if (other.GetComponent<PickUpObj>().isEat || other.GetComponent<PickUpObj>().isXPCoins)
                other.transform.position = new Vector3(Random.Range(-rangeSpawnObj, rangeSpawnObj), 0.3f, Random.Range(-rangeSpawnObj, rangeSpawnObj));

        if (!isDecore)
            if (other.tag == "Player")
            {
                if (isEat)
                {
                    other.GetComponent<GameProcess>().hungry += 5 * eat_Lvl + (GameObject.FindGameObjectWithTag("Player").GetComponent<GameProcess>().eatLvlUpg * 2);
                    particlePrefab.GetComponent<ParticleSystem>().startColor = transform.GetComponent<Renderer>().material.color;
                }

                if (isXPCoins)
                {
                    other.GetComponent<GameProcess>().xpCurrent += 5 * xp_Lvl + (GameObject.FindGameObjectWithTag("Player").GetComponent<GameProcess>().coinsLvlUpg * 2);
                    particlePrefab.GetComponent<ParticleSystem>().startColor = transform.GetComponentInChildren<Renderer>().material.color;
                }

                if (isEat || isXPCoins)
                {
                    Instantiate(particlePrefab, transform.position, transform.rotation);
                }


                transform.position = new Vector3(Random.Range(-rangeSpawnObj, rangeSpawnObj), 0.3f, Random.Range(-rangeSpawnObj, rangeSpawnObj));
            }
    }
}
