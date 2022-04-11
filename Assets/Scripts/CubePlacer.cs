using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Collections.Generic;



public class CubePlacer : MonoBehaviour
{
    public Dictionary<Vector3, (string type, GameObject Object)> coordinates = new Dictionary<Vector3, (string type, GameObject Object)>();
    //Vector3[] Gehad = new Vector3[0];
    string[] Roads = new string[] {"Weg", "T_splitsing_weg", "doodlopende_weg"};
    string[] Buildings = new string[] { "hoekhuis", "Weg", "Ziekenhuis", "politie", "Brandweer"};
    public Dictionary<int, float> Inwoners = new Dictionary<int, float>();
    private Grid grid;
    public GameObject A;
    private int Mode = 1;
    bool placement = false;
    public Dictionary<int, float> Ziekenhuizen_Score = new Dictionary<int, float>();
    public Dictionary<int, float> Politie_Score = new Dictionary<int, float>();
    public Dictionary<int, float> Brandweer_Score = new Dictionary<int, float>();
    //public float[] Ziekenhuizen_Score = new float[0];
    float Ziekenhuis_Stats = 0.1f;
    float Politie_Stats = 0.1f;
    float Brandweer_Stats = 0.1f;
    public Slider Ziekenhuis_slider;
    public Slider Politie_slider;
    public Slider Brandweer_slider;
    float tijdig;
    float temp_Pos;
    int Wegen = 0;
    int total_Cit;
    (string type, GameObject Object) Adden;




    private Quaternion Tijdig_Orientatie;
    private float orientation;
    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    private void Update()
    {
        foreach (KeyValuePair<int, float> entry in Ziekenhuizen_Score)
        {
            tijdig += entry.Value;
        }
        Ziekenhuis_Stats = tijdig / Ziekenhuizen_Score.Count;
        Ziekenhuis_slider.value = (int)Math.Round(Ziekenhuis_Stats,0);
        tijdig = 0;


        foreach (KeyValuePair<int, float> entry in Politie_Score)
        {
            tijdig += entry.Value;
        }
        Politie_Stats = tijdig / Politie_Score.Count;
        Politie_slider.value = (int)Math.Round(Politie_Stats,0);
        tijdig = 0;

        foreach (KeyValuePair<int, float> entry in Brandweer_Score)
        {
            tijdig += entry.Value;
        }
        Brandweer_Stats = tijdig / Brandweer_Score.Count;
        Brandweer_slider.value = (int)Math.Round(Brandweer_Stats,0);
        tijdig = 0;

        foreach (KeyValuePair<int, float> entry in Inwoners)
        {
            tijdig += entry.Value;
        }
        tijdig = (int)Math.Round(tijdig, 0);
        GameObject.Find("Aantal inwoners").GetComponent<Text>().text = "Total citizens: " + tijdig;
        tijdig = 0;
        


            if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                //print(hitInfo.point);
                PlaceCubeNear(hitInfo.point);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hitInfo))
            {
                var finalposition = grid.GetNearestPointOnGrid(hitInfo.transform.position);
                finalposition.y = 1;
                if(hitInfo.collider.tag == "Weg"){
                    Road_Check(finalposition, true);
                }
                if(hitInfo.collider.tag == "Hoekhuis")
                {
                    print("Hij is gevonden");
                    Brandweer_Score.Remove(hitInfo.collider.GetComponent<Huizen_Script>().ID_NUMMER);
                    Politie_Score.Remove(hitInfo.collider.GetComponent<Huizen_Script>().ID_NUMMER);
                    Ziekenhuizen_Score.Remove(hitInfo.collider.GetComponent<Huizen_Script>().ID_NUMMER);
                    Inwoners.Remove(hitInfo.collider.GetComponent<Huizen_Script>().ID_NUMMER);
                    Destroy(hitInfo.transform.gameObject);
                }
                coordinates.Remove(finalposition);
                if (hitInfo.collider.tag != "Floor")
                {
                    Destroy(hitInfo.transform.gameObject);
                }
            }

        }
        
        if (Input.GetKey(KeyCode.Alpha1)){
            Mode = 1;
        }
        if(Input.GetKey(KeyCode.Alpha2)){
            Mode = 2;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            Mode = 3;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            Mode = 4;
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            Mode = 5;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject.Find("Placeable_Elements").transform.Rotate(0, 90, 0);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Wegen += 1;
        }
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        
        //GameObject hoekhuis_coderclass_samenwerk_opdracht;
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        if (coordinates.ContainsKey(finalPosition) || finalPosition.y >= 1.5f)
        {
            print("Panick attack");
        }
        else
        {
                Road_Check(finalPosition, false);
                if(Mode != 2){
                    if (placement)
                    {
                        temp_Pos = finalPosition.y;
                        finalPosition.y = GameObject.Find(Buildings[Mode - 1]).transform.position.y;
                        A = Instantiate(GameObject.Find(Buildings[Mode - 1]), finalPosition, GameObject.Find(Buildings[Mode - 1]).transform.rotation);
                        A.gameObject.name = Buildings[Mode - 1] + "_Clone";
                        finalPosition.y = temp_Pos;
                        Adden = (Buildings[Mode - 1], A);
                        coordinates.Add(finalPosition, Adden);
                    }
                }
                else{
                    temp_Pos = finalPosition.y;
                    finalPosition.y = GameObject.Find(Roads[Wegen%Roads.Count()]).transform.position.y;
                    A = Instantiate(GameObject.Find(Roads[Wegen % Roads.Count()]), finalPosition, GameObject.Find(Roads[Wegen % Roads.Count()]).transform.rotation);
                    A.gameObject.name = "Weg_Clone";
                    finalPosition.y = temp_Pos;
                    Adden = (Buildings[Mode - 1], A);
                    coordinates.Add(finalPosition, Adden);
                }
                

            //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
            //print("Er is een object geplaatst");
            
            //print(coordinates);

        }


        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = nearPoint;
    }
    private void Road_Check(Vector3 Position, bool Verwijderen)
    {
        
        Position.x -= 1;
        if (coordinates.ContainsKey(Position))
        {
            if (coordinates[Position].type == "Weg")
            {
                placement = true;
                return;
            }
            else
            {
                if(Verwijderen){
                    if (coordinates[Position].Object.tag == "Hoekhuis")
                    {
                        Brandweer_Score.Remove(coordinates[Position].Object.GetComponent<Huizen_Script>().ID_NUMMER);
                        Politie_Score.Remove(coordinates[Position].Object.GetComponent<Huizen_Script>().ID_NUMMER);
                        Ziekenhuizen_Score.Remove(coordinates[Position].Object.GetComponent<Huizen_Script>().ID_NUMMER);
                        Inwoners.Remove(coordinates[Position].Object.GetComponent<Huizen_Script>().ID_NUMMER);
                    }
                    Destroy(coordinates[Position].Object);
                     coordinates.Remove(Position);
                 }
                placement = false;
            }
        }
        Position.x += 2;
        if (coordinates.ContainsKey(Position))
        {
             if (coordinates[Position].type == "Weg")
             {
                    placement = true;
                    return;
             }
             else
             {
                 if(Verwijderen){
                    if (coordinates[Position].Object.tag == "Hoekhuis")
                    {
                        Brandweer_Score.Remove(coordinates[Position].Object.GetComponent<Huizen_Script>().ID_NUMMER);
                        Politie_Score.Remove(coordinates[Position].Object.GetComponent<Huizen_Script>().ID_NUMMER);
                        Ziekenhuizen_Score.Remove(coordinates[Position].Object.GetComponent<Huizen_Script>().ID_NUMMER);
                        Inwoners.Remove(coordinates[Position].Object.GetComponent<Huizen_Script>().ID_NUMMER);
                    }
                    Destroy(coordinates[Position].Object);
                     coordinates.Remove(Position);
                 }
                 placement = false;
             }
        }
        Position.x -= 1;
        Position.z += 1;
        if (coordinates.ContainsKey(Position))
        {
           if (coordinates[Position].type == "Weg")
           {
                    placement = true;
                    return;
           }
           else
           {
               if(Verwijderen){
                    if (coordinates[Position].Object.tag == "Hoekhuis")
                    {
                        Brandweer_Score.Remove(coordinates[Position].Object.GetComponent<Huizen_Script>().ID_NUMMER);
                        Politie_Score.Remove(coordinates[Position].Object.GetComponent<Huizen_Script>().ID_NUMMER);
                        Ziekenhuizen_Score.Remove(coordinates[Position].Object.GetComponent<Huizen_Script>().ID_NUMMER);
                        Inwoners.Remove(coordinates[Position].Object.GetComponent<Huizen_Script>().ID_NUMMER);
                    }
                    Destroy(coordinates[Position].Object);
                     coordinates.Remove(Position);
                 }
                    placement = false;
           }
        }
        Position.z -= 2;
        if (coordinates.ContainsKey(Position))
        {
            if (coordinates[Position].type == "Weg")
            {
                placement = true;
                return;
            }
            else
            {
                if(Verwijderen){
                    if(coordinates[Position].Object.tag == "Hoekhuis")
                    {
                        Brandweer_Score.Remove(coordinates[Position].Object.GetComponent<Huizen_Script>().ID_NUMMER);
                        Politie_Score.Remove(coordinates[Position].Object.GetComponent<Huizen_Script>().ID_NUMMER);
                        Ziekenhuizen_Score.Remove(coordinates[Position].Object.GetComponent<Huizen_Script>().ID_NUMMER);
                        Inwoners.Remove(coordinates[Position].Object.GetComponent<Huizen_Script>().ID_NUMMER);
                    }
                     Destroy(coordinates[Position].Object);
                     coordinates.Remove(Position);
                 }
                placement = false;
            }
        }
        else
        {
            print("It doesn't contain");
            placement = false;
        }
    }
}
