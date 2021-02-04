using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//using System.IO;
using SimpleJSON;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    Vector3 offset;

    string json;
    public TextAsset WaveConfigJSON;
    JSONNode waves;
    int round = 0;
	
	int countDead;
    
    // Start is called before the first frame update
    void Start()
    {
        //json = File.ReadAllText(Application.dataPath + "/Maps/Configs/EnemySpawner.json");
        json = WaveConfigJSON.text;
        waves = JSON.Parse(json);
        offset = new Vector3(0.0f, 0.0f, -0.4f);

        SpawnEnemy();   //MUDAR QUANDO SPAWN FOR SOMENTE ATIVADO POR RETIRADA DA ESPADA
    }

    // Update is called once per frame (por isso, gasta bastante)
    //void Update(){}

    void SpawnEnemy()
    {
        IEnumerable<Transform> auxPos = this.GetComponentsInChildren<Transform>().Where(x => x != this.transform);
        Transform[] pos = auxPos.ToArray();
        auxPos = null;

        int side = waves["Waves"][round]["side"];
		
        for (int e = 0; e < waves["Waves"][round]["count"]; e++)
        {
            pos[side].position = pos[side].position + offset;
            Instantiate(enemy, pos[side].position, pos[side].rotation);
            offset.z -= offset.z;
        }

        if(this.gameObject.GetComponent<EnemySpawner_Events>() != null)
        {
            EnemySpawner_Events enemySpawner_Events = this.gameObject.GetComponent<EnemySpawner_Events>();
            enemySpawner_Events.EventManager(waves, round);
        }

    }

    //Chamado por EnemyManager quando é morto
    public void DeathChecker()
    {
        countDead++;

        if (waves["Waves"][round]["count"] == countDead)
        {
            if(round < waves["Waves"].Count - 1)    //EVITA CHAMAR SPAWNENEMY QUANDO ACABA O ARRAY DE INIMIGOS
            {
                round++;
                countDead = 0;
                SpawnEnemy();
            }
        }
    }
}