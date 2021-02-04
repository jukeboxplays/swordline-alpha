using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    //NÃO ESTÁ EM USO 26-07
    //NÃO ESTÁ EM USO 26-07
    //NÃO ESTÁ EM USO 26-07
    //NÃO ESTÁ EM USO 26-07

    Transform thisSword;
    public Transform enemySword;

    // Start is called before the first frame update
    void Start()
    {
        thisSword = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        thisSword.transform.position = enemySword.transform.position;
        thisSword.transform.rotation = enemySword.transform.rotation;
    }

}
