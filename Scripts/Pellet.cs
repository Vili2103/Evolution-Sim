using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public float growthRate = 0.01f;
    public Vector3 maxSize = new Vector3(1.75f, 1.75f, 0);
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
            Instantiate(pellet, gameObject.transform.position + offsetList[Random.Range(0,2)]+ 
            new Vector3(offsets[UnityEngine.Random.Range(0,offsets.Length)], offsets[Random.Range(0, offsets.Length)],0f),
            transform.rotation, pellletOrganizer.transform);
        }
            
    }
        
    private IEnumerator growPellet()
    {
        Vector3 stepSize = maxSize - transform.localScale;
        stepSize *= growthRate;

        while (transform.localScale.x < maxSize.x)
        {
            transform.localScale += stepSize * Time.deltaTime * speedUp.gameSpeed;

            yield return null;
        }
        
    }

}
