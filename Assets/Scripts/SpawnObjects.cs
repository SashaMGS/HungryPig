using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public int countEat1Obj = 5;
    public int countXP1Obj = 5;
    public int countDecoreObj = 5;

    public int rangeSpawnObj; // Значение, в диапазон которого будут раскидываться случайно предметы
    public GameObject prefEat1;
    public GameObject prefXP1;
    public GameObject[] PrefabsDecore;
    public GameObject prefPlane;
    public GameObject prefPlane1;



    private void Awake()
    {
        for (int i = 0; i < countEat1Obj; i++) // Создаём еду
            Instantiate(prefEat1, new Vector3(Random.Range(-rangeSpawnObj, rangeSpawnObj), 0.3f, Random.Range(-rangeSpawnObj, rangeSpawnObj)), gameObject.transform.rotation);
        for (int i = 0; i < countXP1Obj; i++) // Создаём монеты с опытом
            Instantiate(prefXP1, new Vector3(Random.Range(-rangeSpawnObj, rangeSpawnObj), 0.3f, Random.Range(-rangeSpawnObj, rangeSpawnObj)), gameObject.transform.rotation);
        for (int i1 = 0; i1 < PrefabsDecore.Length; i1++) // Создаём Обьекты декора
            if (PrefabsDecore[i1].GetComponent<PickUpObj>().isGroundObj) // Если обьект лежит на земле
                for (int i = 0; i < countDecoreObj*5; i++) // Создаём его в 4 раза больше, чем обычных декор обьектов
                    Instantiate(PrefabsDecore[i1], new Vector3(Random.Range(-rangeSpawnObj, rangeSpawnObj), 0f, Random.Range(-rangeSpawnObj, rangeSpawnObj)), gameObject.transform.rotation);
            else // Иначе
                for (int i = 0; i < countDecoreObj; i++) // Создаём заданное количество раз данные обьекты
                    Instantiate(PrefabsDecore[i1], new Vector3(Random.Range(-rangeSpawnObj, rangeSpawnObj), 0f, Random.Range(-rangeSpawnObj, rangeSpawnObj)), gameObject.transform.rotation);
    }



}
