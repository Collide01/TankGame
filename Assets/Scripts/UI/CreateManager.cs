using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateManager : MonoBehaviour
{
    public enum Vehicle
    {
        Ambulance,
        Delivery,
        DeliveryFlat,
        FireTruck,
        GarbageTruck,
        HatchbackSports,
        Police,
        Race,
        RaceFuture,
        Sedan,
        SedanSports,
        SUV,
        SUVLuxury,
        Taxi,
        Tractor,
        TractorPolice,
        TractorShovel,
        Truck,
        TruckFlat,
        Van
    }
    public Vehicle chosenVehicle;

    public enum Blaster
    {
        BlasterA,
        BlasterB,
        BlasterC,
        BlasterD,
        BlasterE,
        BlasterF,
        BlasterG,
        BlasterH,
        BlasterI,
        BlasterJ,
        BlasterK,
        BlasterL,
        BlasterM,
        BlasterN,
        BlasterO,
        BlasterP,
        BlasterQ,
        BlasterR,
    }
    public Blaster chosenBlaster;

    public enum SpecialShot
    {
        Bouncy,
        Laser,
        Mine
    }

    // These variables are adjusted based on the vehicle chosen
    [HideInInspector] private Vector3 blasterLocation;
    [HideInInspector] private Vector3 firePoint;
    [HideInInspector] private Vector3 specialFirePoint;
    [HideInInspector] private Vector3 minePoint;

    // This variable is adjusted based on the blaster chosen
    [HideInInspector] private float shellLifespan;

    [Header("Menu GameObjects")]
    public GameObject vehicle;
    public GameObject blaster;
    public GameObject vehicleSelection;
    public GameObject blasterSelection;

    [Header("Controllable sliders")]
    public Slider vehicleSlider;
    public Slider blasterSlider;

    [Header("Stat sliders and text")]
    public Slider healthSlider; // Minimum: 1, Maximum: 5
    public Slider speedSlider; // Minimum: 5, Maximum: 20
    public Slider turnSpeedSlider; // Minimum: 50, Maximum: 200
    public Slider bulletSpeedSlider; // Minimum: 500, Maximum: 2000
    public Slider fireRateSlider; // Minimum: 0.5, Maximum: 4
    public TMP_Text specialShotType;

    [Header("Models")]
    public GameObject ambulance;
    public GameObject delivery;
    public GameObject deliveryFlat;
    public GameObject fireTruck;
    public GameObject garbageTruck;
    public GameObject hatchbackSports;
    public GameObject police;
    public GameObject race;
    public GameObject raceFuture;
    public GameObject sedan;
    public GameObject sedanSports;
    public GameObject suv;
    public GameObject suvLuxury;
    public GameObject taxi;
    public GameObject tractor;
    public GameObject tractorPolice;
    public GameObject tractorShovel;
    public GameObject truck;
    public GameObject truckFlat;
    public GameObject van;
    public GameObject blasterA;
    public GameObject blasterB;
    public GameObject blasterC;
    public GameObject blasterD;
    public GameObject blasterE;
    public GameObject blasterF;
    public GameObject blasterG;
    public GameObject blasterH;
    public GameObject blasterI;
    public GameObject blasterJ;
    public GameObject blasterK;
    public GameObject blasterL;
    public GameObject blasterM;
    public GameObject blasterN;
    public GameObject blasterO;
    public GameObject blasterP;
    public GameObject blasterQ;
    public GameObject blasterR;

    // Start is called before the first frame update
    void Start()
    {
        chosenVehicle = Vehicle.Ambulance;
        chosenBlaster = Blaster.BlasterA;
        blasterLocation = new Vector3(0, 0.55f, 1.63f);

        // Set models
        Instantiate(ambulance, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
        Instantiate(blasterA, blaster.transform.position, blaster.transform.rotation, blaster.transform);
        Instantiate(ambulance, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
        Instantiate(blasterA, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVehicle()
    {
        // Using loops here for when the slider loops back around and as a failsafe for other potential bugs
        List<int> childrenToDestroy = new List<int>();
        // Overall tank
        for (int i = 0; i < vehicle.transform.childCount; i++)
        {
            if (vehicle.transform.GetChild(i).gameObject != blaster)
            {
                childrenToDestroy.Add(i);
            }
        }
        for (int i = 0; i < childrenToDestroy.Count; i++)
        {
            Destroy(vehicle.transform.GetChild(childrenToDestroy[i]).gameObject);
        }

        // Tank selection
        childrenToDestroy.Clear();
        for (int i = 0; i < vehicleSelection.transform.childCount; i++)
        {
            childrenToDestroy.Add(i);
        }
        for (int i = 0; i < childrenToDestroy.Count; i++)
        {
            Destroy(vehicleSelection.transform.GetChild(childrenToDestroy[i]).gameObject);
        }

        int value = (int)vehicleSlider.value;
        
        // Loops the slider around to top or bottom
        if (value == 0)
        {
            vehicleSlider.value = 20;
            value = 20;
        }
        if (value == 21)
        {
            vehicleSlider.value = 1;
            value = 1;
        }

        // Subtracts it by 1 because the enums start at 0
        value--;

        switch (value)
        {
            case 0:
                blasterLocation = new Vector3(0, 0.55f, 1.63f);
                Instantiate(ambulance, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(ambulance, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 1:
                blasterLocation = new Vector3(0, 0.55f, 1.63f);
                Instantiate(delivery, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(delivery, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 2:
                blasterLocation = new Vector3(0, 0.55f, 1.63f);
                Instantiate(deliveryFlat, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(deliveryFlat, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 3:
                blasterLocation = new Vector3(0, 0.55f, 1.63f);
                Instantiate(fireTruck, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(fireTruck, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 4:
                blasterLocation = new Vector3(0, 0.55f, 1.72f);
                Instantiate(garbageTruck, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(garbageTruck, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 5:
                blasterLocation = new Vector3(0, 0.55f, 1.41f);
                Instantiate(hatchbackSports, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(hatchbackSports, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 6:
                blasterLocation = new Vector3(0, 0.55f, 1.47f);
                Instantiate(police, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(police, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 7:
                blasterLocation = new Vector3(0, 0.55f, 0.67f);
                Instantiate(race, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(race, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 8:
                blasterLocation = new Vector3(0, 0.55f, 1f);
                Instantiate(raceFuture, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(raceFuture, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 9:
                blasterLocation = new Vector3(0, 0.55f, 1.29f);
                Instantiate(sedan, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(sedan, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 10:
                blasterLocation = new Vector3(0, 0.55f, 1.29f);
                Instantiate(sedanSports, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(sedanSports, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 11:
                blasterLocation = new Vector3(0, 0.55f, 1.29f);
                Instantiate(suv, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(suv, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 12:
                blasterLocation = new Vector3(0, 0.55f, 1.41f);
                Instantiate(suvLuxury, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(suvLuxury, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 13:
                blasterLocation = new Vector3(0, 0.55f, 1.37f);
                Instantiate(taxi, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(taxi, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 14:
                blasterLocation = new Vector3(0, 0.55f, 1.11f);
                Instantiate(tractor, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(tractor, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 15:
                blasterLocation = new Vector3(0, 0.55f, 1.11f);
                Instantiate(tractorPolice, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(tractorPolice, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 16:
                blasterLocation = new Vector3(0, 0.55f, 1.11f);
                Instantiate(tractorShovel, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(tractorShovel, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 17:
                blasterLocation = new Vector3(0, 0.55f, 1.47f);
                Instantiate(truck, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(truck, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 18:
                blasterLocation = new Vector3(0, 0.55f, 1.39f);
                Instantiate(truckFlat, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(truckFlat, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 19:
                blasterLocation = new Vector3(0, 0.55f, 1.39f);
                Instantiate(van, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(van, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
        }
        blaster.transform.localPosition = blasterLocation;
        chosenVehicle = (Vehicle)value;
    }

    public void ChangeBlaster()
    {
        // Using loops here for when the slider loops back around and as a failsafe for other potential bugs
        List<int> childrenToDestroy = new List<int>();
        // Overall tank
        for (int i = 0; i < blaster.transform.childCount; i++)
        {
            childrenToDestroy.Add(i);
        }
        for (int i = 0; i < childrenToDestroy.Count; i++)
        {
            Destroy(blaster.transform.GetChild(childrenToDestroy[i]).gameObject);
        }

        // Tank selection
        childrenToDestroy.Clear();
        for (int i = 0; i < blasterSelection.transform.childCount; i++)
        {
            childrenToDestroy.Add(i);
        }
        for (int i = 0; i < childrenToDestroy.Count; i++)
        {
            Destroy(blasterSelection.transform.GetChild(childrenToDestroy[i]).gameObject);
        }

        int value = (int)blasterSlider.value;

        // Loops the slider around to top or bottom
        if (value == 0)
        {
            blasterSlider.value = 18;
            value = 18;
        }
        if (value == 19)
        {
            blasterSlider.value = 1;
            value = 1;
        }

        // Subtracts it by 1 because the enums start at 0
        value--;

        switch (value)
        {
            case 0:
                Instantiate(blasterA, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterA, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 1:
                Instantiate(blasterB, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterB, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 2:
                Instantiate(blasterC, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterC, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 3:
                Instantiate(blasterD, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterD, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 4:
                Instantiate(blasterE, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterE, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 5:
                Instantiate(blasterF, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterF, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 6:
                Instantiate(blasterG, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterG, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 7:
                Instantiate(blasterH, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterH, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 8:
                Instantiate(blasterI, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterI, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 9:
                Instantiate(blasterJ, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterJ, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 10:
                Instantiate(blasterK, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterK, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 11:
                Instantiate(blasterL, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterL, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 12:
                Instantiate(blasterM, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterM, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 13:
                Instantiate(blasterN, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterN, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 14:
                Instantiate(blasterO, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterO, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 15:
                Instantiate(blasterP, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterP, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 16:
                Instantiate(blasterQ, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterQ, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 17:
                Instantiate(blasterR, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterR, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
        }
        chosenBlaster = (Blaster)value;
    }
}
