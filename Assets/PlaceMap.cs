using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlaceMap : MonoBehaviour
{
    public GameObject[] ObjectsToPlace;
    public GameObject Destination;
    public GameObject StartPos;
    GameObject Map;
    NavMeshBuildSource source;
    Bounds b;
    NavMeshBuildSettings buildSettings;
    List<NavMeshBuildSource> sources;

    // Start is called before the first frame update
    void Awake()
    {
        Map = Instantiate(ObjectsToPlace[2], new Vector3(10, 0, 10), Quaternion.identity);
        sources = new List<NavMeshBuildSource>();
        
        buildSettings = new NavMeshBuildSettings();

        source = BoxSource();
        sources.Add(source);
        


    }
    void Start()
    {
       
        b = new  Bounds(Map.transform.position, Map.transform.localScale);

        //UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
        NavMeshBuilder.BuildNavMeshData(buildSettings, sources, b, Map.transform.position, Map.transform.rotation);

        Destination = GameObject.FindGameObjectWithTag("Destination");
        StartPos = GameObject.FindGameObjectWithTag("StartPos");
        Instantiate(ObjectsToPlace[1], StartPos.transform.position, Quaternion.identity);
    }

    public NavMeshBuildSource BoxSource()
    {
        var src = new NavMeshBuildSource();
        src.transform = transform.localToWorldMatrix;
        src.shape = NavMeshBuildSourceShape.Box;
        src.size = new Vector3(GameObject.FindGameObjectWithTag("MapFloor").transform.localScale.x, GameObject.FindGameObjectWithTag("MapFloor").transform.localScale.y, GameObject.FindGameObjectWithTag("MapFloor").transform.localScale.z);
        return src;
    }
}
