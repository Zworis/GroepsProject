using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Huizen_Script : MonoBehaviour
{
    float afstand;
    bool Loop = true;
    public int ID_NUMMER;
    float stats;
    float Total;
    float inwoners = 2;
    // Start is called before the first frame update
    void Start()
    {

        while (Loop)
        {
            ID_NUMMER = Random.Range(1, 90000);
            if (GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Ziekenhuizen_Score.ContainsKey(ID_NUMMER))
            {
                Loop = true;
            }
            else
            {
                Loop = false;
            }
        }
        if (!transform.name.Contains("Clone"))
        {
            ID_NUMMER = 999999999;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ID_NUMMER != 999999999)
        {
            stats = 100 - (Vector3.Distance(FindClosestTarget("Ziekenhuis").transform.position, transform.position) * 2) + 5;
            stats = Round_Off(stats);
            Total += stats;
            if (GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Ziekenhuizen_Score.ContainsKey(ID_NUMMER))
            {
                if(GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Ziekenhuizen_Score[ID_NUMMER] != stats){
                    GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Ziekenhuizen_Score[ID_NUMMER] = stats;
                }
            }
            else
            {
                GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Ziekenhuizen_Score.Add(ID_NUMMER, stats);
            }


            stats = 100 - (Vector3.Distance(FindClosestTarget("Politie").transform.position, transform.position) * 2) + 5;
            stats = Round_Off(stats);
            Total += stats;
            if (GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Politie_Score.ContainsKey(ID_NUMMER))
            {
                if(GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Politie_Score[ID_NUMMER] != stats){
                    GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Politie_Score[ID_NUMMER] = stats;
                }
            }
            else
            {
                GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Politie_Score.Add(ID_NUMMER, stats);
            }



            stats = 100 - (Vector3.Distance(FindClosestTarget("Brandweer").transform.position, transform.position) * 2) + 5;
            stats = Round_Off(stats);
            Total += stats;
            if (GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Brandweer_Score.ContainsKey(ID_NUMMER))
            {
                if(GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Brandweer_Score[ID_NUMMER] != stats){
                    GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Brandweer_Score[ID_NUMMER] = stats;
                }
            }
            else
            {
                GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Brandweer_Score.Add(ID_NUMMER, stats);
            }
            Total = Total / 3;
            inwoners = 2 + 2 * (2 * (0.01f * Total));
            if (GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Inwoners.ContainsKey(ID_NUMMER))
            {
                if (GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Inwoners[ID_NUMMER] != inwoners)
                {
                    GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Inwoners[ID_NUMMER] = inwoners;
                }
            }
            else
            {
                GameObject.Find("CubePlacer").GetComponent<CubePlacer>().Inwoners.Add(ID_NUMMER, inwoners);
            }
        }
        

    }
    GameObject FindClosestTarget(string trgt)
    {
        Vector3 position = transform.position;
        return GameObject.FindGameObjectsWithTag(trgt).OrderBy(o => (o.transform.position - position).sqrMagnitude).FirstOrDefault();
        
    }

    float Round_Off(float stats){
            if (stats <= 0)
            {
                return 0;
            }
            else if (stats >= 100)
            {
                return 100;
            }
            else{
                return stats;
            }
    }
}
