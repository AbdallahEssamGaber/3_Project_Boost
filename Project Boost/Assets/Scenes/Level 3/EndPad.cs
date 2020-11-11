using System.Collections;
using UnityEditor;
using UnityEngine;

public class EndPad : MonoBehaviour
{
    static public EndPad multi;     //For making it public in other Scripts
    public string[] colors = { "#2A8C8A", "#8a8c8a", "#bab8b8", "#35634f" };
    public int randomColor;    
    Renderer cubeRenderer;

    // Start is called before the first frame update
    void Start()
    {
        multi = this;   //assigning it to this script
        cubeRenderer = GetComponent<Renderer>();    
        StartCoroutine(genrateAndPick());
    }


    IEnumerator genrateAndPick()
    {
        yield return new WaitForSeconds(3);
        Color color;

        randomColor = Random.Range(0, colors.Length);
        ColorUtility.TryParseHtmlString(colors[randomColor], out color);
        cubeRenderer.material.SetColor("_Color", color);
        onRepeat();
    }

    void onRepeat()
    {
        StartCoroutine(genrateAndPick());
    }
}
