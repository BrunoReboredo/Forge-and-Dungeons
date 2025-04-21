using UnityEngine;
using UnityEngine.AI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] string targetTag = "Player";
    NavMeshAgent agent;
    Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);

        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta " + targetTag);
        }
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}