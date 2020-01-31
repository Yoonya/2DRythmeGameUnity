using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //각 노트 10개씩 리스트, 이중 리스트 사용

    public List<GameObject> Notes;
    private List<List<GameObject>> poolsOfNotes;
    public int noteCount = 10;
    private bool more = true;


    // Start is called before the first frame update
    void Start()
    {
        poolsOfNotes = new List<List<GameObject>>();
        for (int i = 0; i < Notes.Count; i++) //4번반복
        {
            poolsOfNotes.Add(new List<GameObject>());
            for (int n = 0; n < noteCount; n++)//10번 반복
            {
                GameObject obj = Instantiate(Notes[i]);
                obj.SetActive(false);
                poolsOfNotes[i].Add(obj);
            }
        }
    }

    public GameObject getObject(int noteType)
    {
        foreach (GameObject obj in poolsOfNotes[noteType - 1])
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        if(more) // 만약 풀링으로 지정한 40개를 넘어갔을 때, 노트 추가
        {
                GameObject obj = Instantiate(Notes[noteType - 1]);
                poolsOfNotes[noteType - 1].Add(obj);
                return obj;
        }
        return null;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
