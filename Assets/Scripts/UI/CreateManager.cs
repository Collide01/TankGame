using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateManager : MonoBehaviour
{
    // Instance of CreateManager singleton
    [HideInInspector] public static CreateManager instance;

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
    [HideInInspector] public Vehicle chosenVehicle;

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
    [HideInInspector] public Blaster chosenBlaster;

    // These variables are adjusted based on the vehicle chosen
    [HideInInspector] public float health;
    [HideInInspector] public float speed;
    [HideInInspector] public float turnSpeed;
    [HideInInspector] public Vector3 blasterLocation;
    [HideInInspector] public Vector3 firePoint;
    [HideInInspector] public Vector3 specialFirePoint;
    [HideInInspector] public Vector3 minePoint;

    // These variables are adjusted based on the blaster chosen
    [HideInInspector] public float bulletSpeed;
    [HideInInspector] public float fireRate;
    [HideInInspector] public Pawn.SpecialShotType specialShot;

    [Header("Menu GameObjects")]
    public GameObject vehicle;
    public GameObject blaster;
    public GameObject vehicleSelection;
    public GameObject blasterSelection;
    public GameObject vehicleArrowLeft;
    public GameObject vehicleArrowRight;
    public GameObject blasterArrowLeft;
    public GameObject blasterArrowRight;

    [Header("Controllable sliders")]
    public Slider vehicleSlider;
    public Slider blasterSlider;

    [Header("Stat sliders and text")]
    public Slider healthSlider; // Minimum: 1, Maximum: 5
    public Slider speedSlider; // Minimum: 5, Maximum: 20
    public Slider turnSpeedSlider; // Minimum: 50, Maximum: 200
    public Slider bulletSpeedSlider; // Minimum: 500, Maximum: 2000
    public Slider fireRateSlider; // Minimum: 0.5, Maximum: 4
    public TMP_Text specialShotText;

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

    // Awake is called when the object is first created - before even Start can run!
    private void Awake()
    {
        // If the instance doesn't exist yet...
        if (instance == null)
        {
            // This is the instance
            instance = this;
            //Don't destroy it if we load a new scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Otherwise, there is already an instance, so destroy this gameObject
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        chosenVehicle = Vehicle.Ambulance;
        chosenBlaster = Blaster.BlasterA;

        // Set models
        Instantiate(ambulance, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
        Instantiate(blasterA, blaster.transform.position, blaster.transform.rotation, blaster.transform);
        Instantiate(ambulance, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
        Instantiate(blasterA, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
        blasterLocation = ambulance.GetComponent<VehicleData>().blasterLocation;

        // Set visible stats
        health = ambulance.GetComponent<VehicleData>().health; // Minimum: 1, Maximum: 5
        speed = ambulance.GetComponent<VehicleData>().speed; // Minimum: 5, Maximum: 20
        turnSpeed = ambulance.GetComponent<VehicleData>().turnSpeed; // Minimum: 50, Maximum: 200
        bulletSpeed = blasterA.GetComponent<BlasterData>().bulletSpeed; // Minimum: 500, Maximum: 2000
        fireRate = blasterA.GetComponent<BlasterData>().fireRate; // Minimum: 0.5, Maximum: 4
        specialShot = blasterA.GetComponent<BlasterData>().specialShot;

        UpdateVisualStats();

        vehicleArrowLeft.SetActive(false);
        vehicleArrowRight.SetActive(false);
        blasterArrowLeft.SetActive(false);
        blasterArrowRight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        blaster.transform.localPosition = blasterLocation;
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
        GameObject selectedVehicle = null;

        switch (value)
        {
            case 0:
                selectedVehicle = Instantiate(ambulance, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(ambulance, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 1:
                selectedVehicle = Instantiate(delivery, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(delivery, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 2:
                selectedVehicle = Instantiate(deliveryFlat, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(deliveryFlat, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 3:
                selectedVehicle = Instantiate(fireTruck, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(fireTruck, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 4:
                selectedVehicle = Instantiate(garbageTruck, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(garbageTruck, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 5:
                selectedVehicle = Instantiate(hatchbackSports, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(hatchbackSports, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 6:
                selectedVehicle = Instantiate(police, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(police, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 7:
                selectedVehicle = Instantiate(race, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(race, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 8:
                selectedVehicle = Instantiate(raceFuture, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(raceFuture, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 9:
                selectedVehicle = Instantiate(sedan, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(sedan, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 10:
                selectedVehicle = Instantiate(sedanSports, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(sedanSports, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 11:
                selectedVehicle = Instantiate(suv, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(suv, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 12:
                selectedVehicle = Instantiate(suvLuxury, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(suvLuxury, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 13:
                selectedVehicle = Instantiate(taxi, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(taxi, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 14:
                selectedVehicle = Instantiate(tractor, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(tractor, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 15:
                selectedVehicle = Instantiate(tractorPolice, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(tractorPolice, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 16:
                selectedVehicle = Instantiate(tractorShovel, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(tractorShovel, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 17:
                selectedVehicle = Instantiate(truck, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(truck, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 18:
                selectedVehicle = Instantiate(truckFlat, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(truckFlat, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
            case 19:
                selectedVehicle = Instantiate(van, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(van, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                break;
        }
        blasterLocation = selectedVehicle.GetComponent<VehicleData>().blasterLocation;
        health = selectedVehicle.GetComponent<VehicleData>().health; // Minimum: 1, Maximum: 5
        speed = selectedVehicle.GetComponent<VehicleData>().speed; // Minimum: 5, Maximum: 20
        turnSpeed = selectedVehicle.GetComponent<VehicleData>().turnSpeed; // Minimum: 50, Maximum: 200
        UpdateVisualStats();
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
        GameObject selectedBlaster = new GameObject();

        switch (value)
        {
            case 0:
                selectedBlaster = Instantiate(blasterA, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterA, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 1:
                selectedBlaster = Instantiate(blasterB, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterB, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 2:
                selectedBlaster = Instantiate(blasterC, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterC, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 3:
                selectedBlaster = Instantiate(blasterD, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterD, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 4:
                selectedBlaster = Instantiate(blasterE, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterE, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 5:
                selectedBlaster = Instantiate(blasterF, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterF, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 6:
                selectedBlaster = Instantiate(blasterG, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterG, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 7:
                selectedBlaster = Instantiate(blasterH, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterH, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 8:
                selectedBlaster = Instantiate(blasterI, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterI, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 9:
                selectedBlaster = Instantiate(blasterJ, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterJ, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 10:
                selectedBlaster = Instantiate(blasterK, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterK, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 11:
                selectedBlaster = Instantiate(blasterL, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterL, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 12:
                selectedBlaster = Instantiate(blasterM, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterM, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 13:
                selectedBlaster = Instantiate(blasterN, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterN, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 14:
                selectedBlaster = Instantiate(blasterO, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterO, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 15:
                selectedBlaster = Instantiate(blasterP, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterP, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 16:
                selectedBlaster = Instantiate(blasterQ, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterQ, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
            case 17:
                selectedBlaster = Instantiate(blasterR, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterR, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                break;
        }
        bulletSpeed = selectedBlaster.GetComponent<BlasterData>().bulletSpeed; // Minimum: 500, Maximum: 2000
        fireRate = selectedBlaster.GetComponent<BlasterData>().fireRate; // Minimum: 0.5, Maximum: 4
        specialShot = selectedBlaster.GetComponent<BlasterData>().specialShot;
        UpdateVisualStats();
        chosenBlaster = (Blaster)value;
    }

    public void ToggleVehicleArrows()
    {
        if (vehicleArrowLeft.activeSelf)
        {
            vehicleArrowLeft.SetActive(false);
            vehicleArrowRight.SetActive(false);
        }
        else
        {
            vehicleArrowLeft.SetActive(true);
            vehicleArrowRight.SetActive(true);
        }
    }

    public void ToggleBlasterArrows()
    {
        if (blasterArrowLeft.activeSelf)
        {
            blasterArrowLeft.SetActive(false);
            blasterArrowRight.SetActive(false);
        }
        else
        {
            blasterArrowLeft.SetActive(true);
            blasterArrowRight.SetActive(true);
        }
    }

    private void UpdateVisualStats()
    {
        healthSlider.value = health;
        speedSlider.value = speed;
        turnSpeedSlider.value = turnSpeed;
        bulletSpeedSlider.value = bulletSpeed;
        fireRateSlider.value = fireRate;
        switch (specialShot)
        {
            case Pawn.SpecialShotType.BouncyShot:
                specialShotText.text = "Bouncy Shot";
                break;
            case Pawn.SpecialShotType.LaserBeam:
                specialShotText.text = "Laser Beam";
                break;
            case Pawn.SpecialShotType.Mine:
                specialShotText.text = "Mine";
                break;
        }
    }
}
