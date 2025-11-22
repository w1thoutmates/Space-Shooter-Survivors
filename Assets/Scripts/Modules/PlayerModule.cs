using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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
        List<Module> newModules = allModules.FindAll(m => m != null && !ownedModules.Exists(inst => inst.module == m)); 

        List<Module> availableModules = new List<Module>();
        if (ownedModules.Count >= maxModules || newModules.Count == 0)
        {
            foreach (var inst in ownedModules)
            {
                if (inst.module != null) availableModules.Add(inst.module); 
            }
        }
        else
        {
            availableModules.AddRange(newModules.Where(m => m != null));
            foreach (var inst in ownedModules)
            {
                if (inst.module != null) availableModules.Add(inst.module);
            }
        }

        while (choices.Count < 3 && availableModules.Count > 0)
        {
            int index = Random.Range(0, availableModules.Count);
            Module selected = availableModules[index];
            availableModules.RemoveAt(index);
            if (selected != null) choices.Add(selected); 
        }
        return choices;
    }


    public void AddModule(Module module)
    {
        Modules instance = ownedModules.Find(m => m.module == module);
        if (instance == null && ownedModules.Count < maxModules)
        {
            instance = new Modules
            {
                module = module,
                currentLevel = 1,
                totalBonus = 0f,
                quality = ModuleQuality.Common 
            };
            ownedModules.Add(instance);

            float bonusThisUpgrade = module.bonusPerLevel * ModuleQualityMultiplier.Get(ModuleQuality.Common);
            instance.totalBonus += bonusThisUpgrade;
            module.Apply(PlayerController.instance, ModuleQuality.Common);
        }
        else if (instance != null)
        {
            instance.currentLevel++;

            float bonusThisUpgrade = module.bonusPerLevel * ModuleQualityMultiplier.Get(instance.quality);
            instance.totalBonus += bonusThisUpgrade;

            module.Apply(PlayerController.instance, instance.quality);
        }
    }

    public Modules GetInstance(Module module)
    {
        if (module == null)
        {
            Debug.LogError("GetInstance called with null module!");
            return null; 
        }

        Modules instance = ownedModules.Find(m => m.module == module);
        if (instance != null) return instance;

        return new Modules { module = module, currentLevel = 0, totalBonus = 0f, quality = ModuleQuality.Common }; 
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
