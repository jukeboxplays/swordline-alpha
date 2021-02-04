using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    
    EnemyManager enemy;

    bool onCollision = false;

    Dictionary<string, Dictionary<string, float>> dmgDict = new Dictionary<string, Dictionary<string, float>>();
    
    void Start()
    {
        //MUDAR PARA JSON
        //MUDAR PARA JSON
        //MUDAR PARA JSON
        //MUDAR PARA JSON
        //MUDAR PARA JSON

        dmgDict.Add("mixamorig:Head", new Dictionary<string, float>());
        dmgDict["mixamorig:Head"].Add("Head_", 100f);

        dmgDict.Add("mixamorig:LeftArm", new Dictionary<string, float>());
        dmgDict["mixamorig:LeftArm"].Add("L_Shoulder_Tie", 20f); //PEDIR P/ NICOLAS DIVIDIR ANTEBRAÇO NO MESH P/ ADD AQUI

        dmgDict.Add("mixamorig:LeftForeArm", new Dictionary<string, float>());
        dmgDict["mixamorig:LeftForeArm"].Add("L_Arm_Tie", 25f);

        dmgDict.Add("mixamorig:RightArm", new Dictionary<string, float>());
        dmgDict["mixamorig:RightArm"].Add("R_Shoulder_Tie", 20f); //PEDIR P/ NICOLAS DIVIDIR ANTEBRAÇO NO MESH P/ ADD AQUI

        dmgDict.Add("mixamorig:RightForeArm", new Dictionary<string, float>());
        dmgDict["mixamorig:RightForeArm"].Add("R_Arm_Tie", 25f);
 
        dmgDict.Add("mixamorig:Spine2", new Dictionary<string, float>());
        dmgDict["mixamorig:Spine2"].Add("Torso_", 40f);

        dmgDict.Add("mixamorig:Hips", new Dictionary<string, float>());
        dmgDict["mixamorig:Hips"].Add("Torso_Tie", 50f);

        dmgDict.Add("mixamorig:LeftUpLeg", new Dictionary<string, float>());
        dmgDict["mixamorig:LeftUpLeg"].Add("Body", 20f); //PEDIR P/ NICOLAS DIVIDIR ANTEBRAÇO NO MESH P/ ADD AQUI

        dmgDict.Add("mixamorig:LeftLeg", new Dictionary<string, float>());
        dmgDict["mixamorig:LeftLeg"].Add("L_Leg_Tie", 25f);

        dmgDict.Add("mixamorig:RightUpLeg", new Dictionary<string, float>());
        dmgDict["mixamorig:RightUpLeg"].Add("Body", 20f); //PEDIR P/ NICOLAS DIVIDIR ANTEBRAÇO NO MESH P/ ADD AQUI

        dmgDict.Add("mixamorig:RightLeg", new Dictionary<string, float>());
        dmgDict["mixamorig:RightLeg"].Add("R_Leg_Tie", 25f);

    }

    //Fazer metodo para pegar DE-PARA de Bone e Mesh por Skinned Mesh Renderer

    private void OnCollisionEnter(Collision collision)
    //private void OnTriggerEnter(Collider collision)
    {

        enemy = collision.transform.root.GetComponent<EnemyManager>();

        if (enemy != null)
        {
            if (dmgDict.ContainsKey(collision.gameObject.name) && !onCollision)
            {
                onCollision = true;


                var bonePart = dmgDict[collision.gameObject.name];
                var bodyPartName = bonePart.ElementAt(0).Key;
                var bodyPart = enemy.transform.Find("Mesh/" + bodyPartName).gameObject;


                var hitScore = bonePart.ElementAt(0).Value;

                //enemy.enemyLife = enemy.enemyLife - (30 * (hitScore/100));    //só para a demo
                enemy.enemyLife = enemy.enemyLife - hitScore;
                //Debug.Log("SD: " + enemy.name + ": " + enemy.enemyLife);


                //Destroy(bodyPart);
                //dmgDict.Remove(collision.gameObject.name);

                //Material partMaterial =  bodyPart.GetComponent<Renderer>().material;
                //partMaterial.SetColor("_Color", Color.green);
            }
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (dmgDict.ContainsKey(collision.gameObject.name) && onCollision)
        {
            onCollision = false;
        }
    }

}
