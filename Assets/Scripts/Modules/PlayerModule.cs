using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerModule : MonoBehaviour
{
    public static PlayerModule instance;

    public int maxModules = 3;
    public List<Modules> ownedModules = new List<Modules>();
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

        List<Module> newModules = allModules.FindAll(m =>
            !ownedModules.Exists(inst => inst.module == m));

        List<Module> availableModules = new List<Module>();

        if (ownedModules.Count >= maxModules || newModules.Count == 0)
        {
            foreach (var inst in ownedModules) availableModules.Add(inst.module);
        }
        else
        {
            availableModules.AddRange(newModules);
            foreach (var inst in ownedModules) availableModules.Add(inst.module);
        }

        while (choices.Count < 3 && availableModules.Count > 0)
        {
            int index = Random.Range(0, availableModules.Count);
            Module selected = availableModules[index];
            availableModules.RemoveAt(index);
            choices.Add(selected);
        }

        return choices;
    }


    public void AddModule(Module module)
    {
        Modules instance = ownedModules.Find(m => m.module == module);

        if (instance != null)
        {
            instance.currentLevel++;
            module.Apply(PlayerController.instance, instance.currentLevel);
        }
        else if (ownedModules.Count < maxModules)
        {
            instance = new Modules { module = module, currentLevel = 1 };
            ownedModules.Add(instance);
            module.Apply(PlayerController.instance, instance.currentLevel);
        }
    }

    public Modules GetInstance(Module module)
    {
        Modules instance = ownedModules.Find(m => m.module == module);
        if (instance != null) return instance;

        return new Modules { module = module, currentLevel = 0 };
    }

    public bool HasModule(Module m)
    {
        foreach (var inst in ownedModules)
        {
            if (inst.module == m)
                return true;
        }
        return false;
    }

}
