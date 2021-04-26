using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public bool CanMove = false;
    public bool InView = false;
    public int MoveCost = 0;
    public bool HasPossibleEntity = false;
    public float SpawnChance = 0;
    public GameObject[] EntitySpawnList;


    private GameObject _ExtraEntity;
    private bool IsAnimating = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (HasPossibleEntity && Random.value <= SpawnChance)
        {
            _ExtraEntity = Instantiate(EntitySpawnList[Random.Range(0, EntitySpawnList.Length)], transform.up / 2 + transform.position, transform.rotation, transform);
            _ExtraEntity.transform.Rotate(Vector3.up, Random.Range(0, 360));
        }
    }

    public void Hovering()
    {
        GetComponent<Renderer>().material.SetInt("_IsHovered", 1);
    }

    public void NotHovering()
    {
        GetComponent<Renderer>().material.SetInt("_IsHovered", 0);
    }

    public void SetToMoveable()
    {
        CanMove = true;
        GetComponent<Renderer>().material.SetInt("_IsInMoveRange", 1);
    }

    public void SetToNotMoveable()
    {
        CanMove = false;
        GetComponent<Renderer>().material.SetInt("_IsInMoveRange", 0);
        MoveCost = 0;
    }

    public void SetOutOfView()
    {
        InView = false;
        StartCoroutine(AnimateBlockDown(.05f));
        if (_ExtraEntity != null) _ExtraEntity.GetComponent<MoveTile>().SetOutOfView();
    }

    private IEnumerator AnimateBlockUp(float animtionTime)
    {
        yield return new WaitUntil(() => !IsAnimating);

        int smoothness = 30;
        float position;
        IsAnimating = true;
        for (int i=0; i<smoothness; i++)
        {
            position = Vector3.Lerp(transform.up * -3, transform.position, (float)i / smoothness).y;

            foreach (Material mat in GetComponent<MeshRenderer>().materials)
                mat.SetVector("_HeightOfBlock", new Vector4(0, position, 0));

            yield return new WaitForSeconds(animtionTime / smoothness); 
        }

        foreach (Material mat in GetComponent<MeshRenderer>().materials)
            mat.SetVector("_HeightOfBlock", new Vector4(0, 0, 0));

        IsAnimating = false;
    }

    private IEnumerator AnimateBlockDown(float animtionTime)
    {
        yield return new WaitUntil(() => !IsAnimating);

        int smoothness = 30;
        float position;
        IsAnimating = true;
        for (int i = 0; i < smoothness; i++)
        {
            position = Vector3.Lerp(transform.position, transform.up * -3, (float)i / smoothness).y;
            foreach (Material mat in GetComponent<MeshRenderer>().materials)
                mat.SetVector("_HeightOfBlock", new Vector4(0, position, 0));
            yield return new WaitForSeconds(animtionTime / smoothness);
        }

        foreach (Material mat in GetComponent<MeshRenderer>().materials)
            mat.SetVector("_HeightOfBlock", new Vector4(0, (transform.up * -3).y, 0));
        GetComponent<Renderer>().enabled = false;
        IsAnimating = false;
    }

    public void SetInView()
    {
        InView = true;
        GetComponent<Renderer>().enabled = true;
        StartCoroutine(AnimateBlockUp(.05f));

        if (_ExtraEntity != null) _ExtraEntity.GetComponent<MoveTile>().SetInView();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!GetComponent<Renderer>().enabled) SetInView();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (InView) SetOutOfView();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!GetComponent<Renderer>().enabled) SetInView();
    }
}

[CustomEditor(typeof(MoveTile))]
public class MoveTileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myScript = target as MoveTile;

        EditorGUILayout.LabelField("Movement and & Range");
        myScript.CanMove = GUILayout.Toggle(myScript.CanMove, "Can Move");
        myScript.InView = GUILayout.Toggle(myScript.InView, "In View");
        myScript.MoveCost = EditorGUILayout.IntField("Move Cost", myScript.MoveCost);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Other");
        myScript.HasPossibleEntity = GUILayout.Toggle(myScript.HasPossibleEntity, "Has Possible Entity");

        if (myScript.HasPossibleEntity)
        {
            myScript.SpawnChance = EditorGUILayout.FloatField("Spawn Chance", myScript.SpawnChance);
            SerializedProperty test = serializedObject.FindProperty("EntitySpawnList");
            EditorGUILayout.PropertyField(test, new GUIContent("Entity List : "));
        }

        serializedObject.ApplyModifiedProperties();
    }
}

