using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform targetPos;
    private Vector3 resultPos;
    
    private void OnEnable()
    {
        Vector3 offset = new Vector3(Random.Range(-20f, 20f), Random.Range(-5f, 15f), 0);
        resultPos = transform.localPosition + offset;
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, resultPos, 3f * Time.deltaTime);
    }
}
