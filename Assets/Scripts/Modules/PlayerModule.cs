using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerModule : MonoBehaviour
{
    public static PlayerModule instance;

    public int maxModules = 3;
    public List<Module> ownedModules = new List<Module>();
    public List<Module> allModules;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public List<Module> GetModuleChoices()
    {
        List<Module> choices = new List<Module>();

        List<Module> newModules = new List<Module>();
        foreach(var m in allModules)
        {
            if (!ownedModules.Contains(m))
            {
                newModules.Add(m);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            Module selected = null;

            if (ownedModules.Count == 0 && newModules.Count == 0)
                break;

            if (ownedModules.Count > maxModules || newModules.Count == 0)
            {
                if (ownedModules.Count > 0)
                    selected = ownedModules[Random.Range(0, ownedModules.Count)];
            }
            else
            {
                if(Random.value < 0.5f && newModules.Count > 0)
                {
                    selected = newModules[Random.Range(0, newModules.Count)];
                    newModules.Remove(selected);
                }
                else if(ownedModules.Count > 0)
                {
                    selected = ownedModules[Random.Range(0, ownedModules.Count)];
                }
            }

            if (selected != null && !choices.Contains(selected))
            {
                choices.Add(selected);
            }
        }

        return choices;
    }

    public void AddModule(Module module)
    {
        if (ownedModules.Contains(module))
        {
            module.currentLevel++;
        }
        else
        {
            if (ownedModules.Count < maxModules)
                ownedModules.Add(module);
        }

        module.Apply(PlayerController.instance, module.currentLevel);
    }
}
