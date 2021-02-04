using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR;

public class EnemyManager : MonoBehaviour
{
    public float enemyLife = 100.0f;
    public GameObject healthMesh;
    private Renderer healthRenderer;
    public bool scriptActive = true;

    EnemySpawner enemySpawner;

    NavMeshAgent agent;
    private float searchRad = 150.0f;
    float differenceZ;

    private Transform player;
    //public bool isDead = false;

    float distance;
    Vector3 directionToPlayer;
    Quaternion rotationToPlayer;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = SteamVR_Render.Top().head;
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        healthRenderer = healthMesh.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        healthRenderer.material.SetColor("_EmissionColor", Color.Lerp(Color.red, Color.green, enemyLife/100));

        distance = Vector3.Distance(player.position, this.transform.position);
        if (Mathf.Abs(this.transform.position.z - differenceZ) == 0)
        {
            animator.SetBool("Walk", false);
        }

        else
        {
            animator.SetBool("Walk", true);
        }

        if (distance <= searchRad)
        {
            agent.SetDestination(player.position);

            if (distance <= agent.stoppingDistance + 1.0f)
            {
                //ATTACK
                animator.SetBool("Attack", true);
                //GO TO PLAYER
                TowardsPlayer();
            }
            else
            {
                animator.SetBool("Attack", false);
            }
        }

        differenceZ = this.transform.position.z;

        if (enemyLife <= 0.0f)
        {
            enemySpawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>(); //REFATORAR P/ QUE APONTE SPAWNER ESPECIFICO, DENTRE OUTROS SPAWNERS
            enemySpawner.DeathChecker();

            Destroy(healthMesh);
            Destroy(animator);
            Destroy(agent);

            Destroy(this.GetComponent<EnemyManager>());

        }
    }

    void TowardsPlayer()
    {
        directionToPlayer = (player.position - this.transform.position).normalized;
        rotationToPlayer = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotationToPlayer, Time.deltaTime);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, searchRad);
    }


}
