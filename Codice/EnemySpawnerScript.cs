using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
//indico cosa deve generare
    public GameObject enemyPrefabL;
    public GameObject enemyPrefabR;
    public float respawnTime = 4.0f;
    float seconds;
    int side;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(EnemyWawe());
    }

    //nuova funzione che si occupa dello spawn

    private void SpawnEnemy(int side) {
        //crea un nuovo GameObject basato su enemyPrefab
        if(side == 0) {
           
            //spawn a sinistra
            GameObject l = Instantiate(enemyPrefabL) as GameObject;
        }else if(side == 1)
        {
            //spawn a destra
           
            GameObject r = Instantiate(enemyPrefabR) as GameObject;
        }else{
           
            GameObject l = Instantiate(enemyPrefabL) as GameObject;
            GameObject r = Instantiate(enemyPrefabR) as GameObject;
        }
    } 

    IEnumerator EnemyWawe(){
        while(true){
            //genero casualmente numero di secondi dopo i quali spawna e da quale lato appariranno
            seconds = Random.Range(1.0f,5.0f); //aggiungo dagli 1 ai 3 secondi al tempo di respawn standard
            side = Random.Range(0,3); //decido casualmente dove spawna
            yield return new WaitForSeconds(respawnTime + seconds - (Time.time * 0.01f));
            SpawnEnemy(side);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }


}
