using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToGoToDungeon01: MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                //Replace this with whatever logic you want to use to validate the objects you want to click on
                if (hit.collider.gameObject.name == "WarpCrystal")
                {
                    SceneManager.LoadScene("TrainingGrounds");
                }
            }
        }

    }

}