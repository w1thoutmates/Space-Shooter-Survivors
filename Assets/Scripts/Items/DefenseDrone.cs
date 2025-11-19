using UnityEngine;

[CreateAssetMenu(fileName = "DefenseDrone", menuName = "Items/Defense Drone")]
public class DefenseDrone : Item
{
    public override void Use(PlayerController player, int stackLevel)
    {
        DroneController droneController = player.GetComponentInChildren<DroneController>();
        if (droneController == null)
        {
            Debug.Log("Drone Controller не найден.");
            return;
        }

        droneController.AddDrone(R.instance.defenseDrone, DroneType.Defense);
    }

    public override void OnRemove(PlayerController player, int stackLevel)
    {
        DroneController droneController = player.GetComponentInChildren<DroneController>();
        if (droneController != null)
        {
            droneController.RemoveLastDrone(DroneType.Defense);
        }
    }
}