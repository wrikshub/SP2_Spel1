using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effects", menuName = "Effects/Splashy Effects", order = 1)]
public class Effects : ScriptableObject
{
    [SerializeField] private List<GameObject> effects = new List<GameObject>();
    
    
    public GameObject SpawnEffect(Vector3 position, float rotation, float scale)
    {
        var effect = Instantiate(FindEffect(name), position, Quaternion.Euler(0, 0, rotation));
        effect.transform.localScale = Vector2.one * scale;
        Destroy(effect, 5f);
        return effect;
    }

    public GameObject SpawnEffect(Vector3 position, float rotation, float scale, Transform parent)
    {
        var effect = Instantiate(FindEffect(name), position, Quaternion.Euler(0, 0, rotation), parent);
        effect.transform.localScale = Vector2.one * scale;
        Destroy(effect, 5f);
        return effect;
    }
    
    private GameObject FindEffect(string name)
    {
        
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].name == name)
            {
                return effects[i];
            }
        }
        Debug.Log($"Could not find effect '{name}'");
        return null;
    }
}
