using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Events;

public class EnemySpawner_Events : MonoBehaviour
{
    public UnityEvent events;
    public JSONNode waves;
    public int round;

    public Transform shojiL_L;
    public Transform shojiL_R;
    public Transform shojiR_L;
    public Transform shojiR_R;

    //CHAMA METODOS BASEADO EM ESCOLHA NO EDITOR
    public void EventManager(JSONNode _waves, int _round)
    {
        waves = _waves;
        round = _round;

        events.Invoke();
    }

    public void OpenShoji()
    {
        StartCoroutine("IOpenShoji", 0.0f);
    }

    public IEnumerator IOpenShoji()
    {
        int side = waves["Waves"][round]["side"];

        Transform[] slideLeft = new Transform[] { shojiL_L, shojiR_L };
        Transform[] slideRight = new Transform[] { shojiL_R, shojiR_R };
        Vector3 offset = new Vector3(0.05f, 0.0f, 0.0f);

        float seconds = waves["Waves"][round]["count"]; //count (qtd. inimigos)
        seconds = (seconds / (float)Math.Sqrt(seconds)) + 2.0f; // (count / sqrt(count)) + GORDURA DE TEMPO         ##### ACRESCENTAR SPEED DO INIMIGO NA EQUACAO ????? ##### 

        for (int j = 0; j < 2; j++)
        {
            float sideEqualizer;
            if (j % 2 == 0) { sideEqualizer = 1.0f; } // EVEN / PAR
            else { sideEqualizer = -1.0f; } // ODDS / IMPAR

            for (int i = 0; i < 20; i++)
            {
                slideLeft[side].position += offset * sideEqualizer;
                slideRight[side].position -= offset * sideEqualizer;
                yield return new WaitForSeconds(0.03f);
            }

            yield return new WaitForSeconds(seconds);

        }

    }

}


/*
 * EM CASO DE USAR ESTE TRECHO DE CODIGO, COMENTE O CONTEUDO ACIMA, MENOS AS DIRETIVAS DE "using", exceto "using UnityEngine.Events;"
 * 
 * 
 *
//MAPEAR EVENTOS (usa nome do metodo)
public enum EventToPlay
{
    OpenShoji,
    Test
}

[CustomEditor(typeof(EnemySpawner_Events))]
public class EnemySpawner_Events_Editor : Editor
{
    override public void OnInspectorGUI()
    {
        EnemySpawner_Events enemySpawner_Events = target as EnemySpawner_Events;
        enemySpawner_Events.eventToPlay = (EventToPlay)EditorGUILayout.EnumPopup("Event To Play", enemySpawner_Events.eventToPlay);

        //MAPEAR EVENTOS PARA MOSTRAR EM EDITOR
        switch (enemySpawner_Events.eventToPlay)
        {
            case EventToPlay.OpenShoji:
                enemySpawner_Events.shojiL_L = (Transform)EditorGUILayout.ObjectField("Shoji L-L", enemySpawner_Events.shojiL_L, typeof(Transform), true);
                enemySpawner_Events.shojiL_R = (Transform)EditorGUILayout.ObjectField("Shoji L-R", enemySpawner_Events.shojiL_R, typeof(Transform), true);

                enemySpawner_Events.shojiR_L = (Transform)EditorGUILayout.ObjectField("Shoji R-L", enemySpawner_Events.shojiR_L, typeof(Transform), true);
                enemySpawner_Events.shojiR_R = (Transform)EditorGUILayout.ObjectField("Shoji R-R", enemySpawner_Events.shojiR_R, typeof(Transform), true);
                break;
            
            case EventToPlay.Test:
                break;
        }
    }
}


public class EnemySpawner_Events : MonoBehaviour
{
    public EventToPlay eventToPlay;
    public JSONNode waves;
    public int round;

    public Transform shojiL_L;
    public Transform shojiL_R;
    public Transform shojiR_L;
    public Transform shojiR_R;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    //CHAMA METODOS BASEADO EM ESCOLHA NO EDITOR
    public void EventManager(JSONNode _waves, int _round)
    {
        waves = _waves;
        round = _round;

        Invoke(eventToPlay.ToString(), 0.0f);
    }



    //---------------------------------------------------------
    //MODEL EVENTS (methods)


    void OpenShoji()
    {
        StartCoroutine(IOpenShoji().ToString(),0.0f);
    }

    IEnumerator IOpenShoji()
    {
        int side = waves["Waves"][round]["side"];

        Transform[] slideLeft = new Transform[] { shojiL_L, shojiR_L };
        Transform[] slideRight = new Transform[] { shojiL_R, shojiR_R };
        //Vector3 offset = new Vector3(0.05f, 0.0f, 0.0f);

        //slideLeft[side].position += offset;
        //slideRight[side].position -= offset;
        for (int i = 0; i < 20; i++)
        {
            slideLeft[side].position += new Vector3(0.05f, 0.0f, 0.0f);
            slideRight[side].position -= new Vector3(0.05f, 0.0f, 0.0f);
            yield return new WaitForSeconds(0.03f);
        }
    }


    void Test()
    {
        
    }


}
*/
