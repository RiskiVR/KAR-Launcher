using System.Collections;
using UnityEngine;

public class ListObjects : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    void OnEnable() => StartCoroutine(List());
    IEnumerator List()
    {
        int i = 0;
        foreach (GameObject g in objects) g.SetActive(false);
        foreach (GameObject g in objects)
        {
            yield return new WaitForSeconds(0.1f);
            objects[i].SetActive(true);
            i++;
        }
    }
}
