using UnityEngine;

[CreateAssetMenu(fileName = "AttackDrone", menuName = "Items/Attack Drone")]
public class AttackDrone : Item
{
    public override void Use(PlayerController player, int stackLevel)
    {
        DroneController droneController = player.GetComponentInChildren<DroneController>();
        if(droneController == null)
        {
            Debug.Log("Drone Controller не найден.");
            return;
        }

        droneController.AddDrone(R.instance.attackDrone, DroneType.Attack);
    }

    public override void OnRemove(PlayerController player, int stackLevel)
    {
        DroneController droneController = player.GetComponentInChildren<DroneController>();
        if (droneController != null)
        {
            droneController.RemoveLastDrone(DroneType.Attack);
        }
    }
}
