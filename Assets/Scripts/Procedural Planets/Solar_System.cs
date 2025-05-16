using System.IO;
using System.Collections.Generic;
using UnityEngine;


public class Solar_System : MonoBehaviour
{
    public TextAsset solar_system;
    
[System.Serializable]
public class SolarSystem
{
    public Star sun;
    public List<Planet> planets;
}
[System.Serializable]
public class Planet
{
    public string name;
    public string type;
    public float size;
    public float distanceFromSun;
    public float orbitalSpeed;
    public float orbitTiltAngle;
    public Material GetPlanetMaterial()
    {
        switch (type.ToLower())
        {
            case "terrestrial":
                return Resources.Load<Material>("Materials/Terrestrial");
            case "lava":
                return Resources.Load<Material>("Materials/Lava");
            case "ice":
                return Resources.Load<Material>("Materials/Ice");
            case "gas_giant":
                return Resources.Load<Material>("Materials/GasGiant");
            default:
                return Resources.Load<Material>("Materials/Default");
        }
    }
}
[System.Serializable]
public class Star
{
    public string name;
    public float size;
    public string color;
    public Color GetColor()
    {
        if (ColorUtility.TryParseHtmlString(color, out Color unityColor))
        {
            return unityColor;
        }
        return Color.yellow; 
    }
}

    void Start()
    {
        SolarSystem solarSystem = JsonUtility.FromJson<SolarSystem>(solar_system.text);

        GenerateWorld(solarSystem);
    }

    private void GenerateWorld(SolarSystem solarSystem)
    {
        if(solarSystem.sun == null)
        {
            Debug.Log("yes sun' name is null");
        }
        GameObject sunGO = CreateStar(solarSystem.sun);
        
        // Create planets
        foreach (Planet planet in solarSystem.planets)
        {
            CreatePlanet(planet, sunGO.transform);
        }    
    }

    GameObject CreateStar(Star star)
    {
        GameObject starGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        starGO.name = star.name;
        starGO.transform.localScale = Vector3.one * star.size;
        
        Renderer renderer = starGO.GetComponent<Renderer>();
        renderer.material.color = star.GetColor();
        renderer.material.SetColor("_EmissionColor", star.GetColor());
        renderer.material.EnableKeyword("_EMISSION");
        
        // Add light component
        Light light = starGO.AddComponent<Light>();
        light.type = LightType.Point;
        light.range = 50f; // Adjust based on your solar system scale
        light.color = star.GetColor();
        
        return starGO;
    }
    void CreatePlanet(Planet planet, Transform sunTransform)
    {
        GameObject planetGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        planetGO.name = planet.name;
        planetGO.transform.localScale = Vector3.one * planet.size;
        planetGO.transform.position = sunTransform.position + new Vector3(planet.distanceFromSun, 0, 0);
        
        // Set material based on planet type
        planetGO.GetComponent<Renderer>().material = planet.GetPlanetMaterial();
        
        // // Add orbit behavior
        // PlanetOrbit orbit = planetGO.AddComponent<PlanetOrbit>();
        // orbit.Initialize(sunTransform, planet.orbitalSpeed, planet.distanceFromSun, planet.orbitTiltAngle);
    }
}

