using UnityEngine;
using System.Linq;

public class CubePlacer : MonoBehaviour
{
    Vector3[] Gehad = new Vector3[0];
    private Grid grid;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                PlaceCubeNear(hitInfo.point);
            }
        }
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        if (Gehad.Contains(finalPosition) || finalPosition.y >= 1.5f)
        {
            print("Panick attack");
        }
        else
        {
            GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
            print("Er is een object geplaatst");
        }
        Gehad = Gehad.Concat(new[] { finalPosition }).ToArray();
        foreach(Vector3 item in Gehad)
        {
            print(item);
        }


        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = nearPoint;
    }
}
