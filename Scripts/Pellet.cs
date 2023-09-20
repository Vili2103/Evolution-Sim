using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public float growthRate = 0.01f;
    public Vector3 maxSize = new Vector3(5f, 5f, 0);
    [SerializeField] private GameObject pellletOrganizer;
    float[] offsets = { 0.5f, -0.5f, 1f, -1f, 1.5f, -1.5f };
    List<Vector3> offsetList = new List<Vector3>();
 
    public GameObject pellet;

    private void Start()
    {
        offsetList.Add(maxSize/ 2);
        offsetList.Add(-maxSize/ 2);
    }

    public void FixedUpdate()
    {
        if (transform.localScale.x < maxSize.x)
            StartCoroutine(growPellet());
        else
        {
            transform.localScale = new Vector3(maxSize.x / 2, maxSize.y / 2, maxSize.z / 2);
            Instantiate(pellet, gameObject.transform.position + offsetList[UnityEngine.Random.Range(0,2)]+ 
            new Vector3(offsets[UnityEngine.Random.Range(0,offsets.Length)], offsets[UnityEngine.Random.Range(0, offsets.Length)],0f),
            transform.rotation, pellletOrganizer.transform);
        }
            
    }
        
    private IEnumerator growPellet()
    {
        while(transform.localScale.x < 5f)
        {
            Vector3 newSize = transform.localScale + Vector3.one * growthRate * Time.deltaTime;
            newSize = Vector3.Min(newSize, maxSize);
            transform.localScale = newSize;

            yield return null;
        }
        
    }

}
