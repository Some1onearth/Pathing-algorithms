using System;
using UnityEngine;
using UnityEditor;

public class PixelMappaEditor : EditorWindow
{
    #region Variables
    //Image
    [SerializeField] public Texture2D levelMap; //serializing can be made private but because it is serialized it is still public.
    //Mappings
    [Serializable]
    public struct PixelColourToObject
    {
        //Colour
        public Color pixelColour;
        //Object
        public GameObject prefab;
    }
    //Mapping array
    [SerializeField]
    PixelColourToObject[] _pixelMapping;
    private SerializedObject sObj;
    Color _pixelColour;
    #endregion
    #region Setup and Display
    [MenuItem("Tools/Tool Window/Pixel Map Generator")]
    static void OpenWindow()
    {
        //GetWindow comes from EditorWindow
        GetWindow(typeof(PixelMappaEditor));
    }

    private void OnEnable()   //use this if you need to update or reference it
    {
        sObj = new SerializedObject(this);
    }

    private void OnGUI()
    {
        //label to name the window
        GUILayout.Label("Spawn level from Pixel image");

        //ObjectField for pixel map
        levelMap = EditorGUILayout.ObjectField("Mapping Texture", levelMap, typeof(Texture2D), false) as Texture2D;
        //PropertyField for []
        EditorGUILayout.PropertyField(sObj.FindProperty("_pixelMapping"), true);
        //Button to generate if there is an image placed
        if (levelMap != null)
        {
            if (GUILayout.Button("Generate Level"))
            {
                GenerateLevel();
            }
        }
    }
    #endregion

    #region Node Detection

    #endregion

    #region Generation methods
    void GenerateObject(int x, int y)
    {
        //Read Pixel Colour
        _pixelColour = levelMap.GetPixel(x, y);
        if (_pixelColour.a == 0)
        {
            //there is no colour, do nothing
            Debug.Log("this pixel is empty, SKIP");
            return;
        }

        foreach (PixelColourToObject colourMapping in _pixelMapping)
        {
            Debug.Log("Check Colour Match: " + _pixelColour + " - " + colourMapping.pixelColour);
            //Scan pixel colour mappings for matching colour
            if (colourMapping.pixelColour.Equals(_pixelColour))
            {
                Debug.Log("Colour Match");
                //turn the pixel x and y into vector2 position
                Vector2 pos = new Vector2(x, y);
                //Spawn object that matches pixel colour at pixel position
                Instantiate(colourMapping.prefab, pos, Quaternion.identity);
            }
        }
    }
    void GenerateLevel()
    {
        for (int x = 0; x < levelMap.width; x++)
        {
            for (int y = 0; y < levelMap.height; y++)
            {
                //sets prefabs into the map
                GenerateObject(x, y);
            }
        }
    }
    #endregion
}
