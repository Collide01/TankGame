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

    // This variable is adjusted based on the blaster chosen
    [HideInInspector] public float bulletSpeed;
    [HideInInspector] public float shellLifespan;
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

        // Set visible stats
        health = 4; // Minimum: 1, Maximum: 5
        speed = 7; // Minimum: 5, Maximum: 20
        turnSpeed = 78; // Minimum: 50, Maximum: 200
        bulletSpeed = 950; // Minimum: 500, Maximum: 2000
        fireRate = 3.2f; // Minimum: 0.5, Maximum: 4
        specialShot = Pawn.SpecialShotType.BouncyShot;

        UpdateVisualStats();

        vehicleArrowLeft.SetActive(false);
        vehicleArrowRight.SetActive(false);
        blasterArrowLeft.SetActive(false);
        blasterArrowRight.SetActive(false);
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
                health = 4; // Minimum: 1, Maximum: 5
                speed = 7; // Minimum: 5, Maximum: 20
                turnSpeed = 78; // Minimum: 50, Maximum: 200
                break;
            case 1:
                blasterLocation = new Vector3(0, 0.55f, 1.63f);
                Instantiate(delivery, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(delivery, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 4; // Minimum: 1, Maximum: 5
                speed = 8; // Minimum: 5, Maximum: 20
                turnSpeed = 70; // Minimum: 50, Maximum: 200
                break;
            case 2:
                blasterLocation = new Vector3(0, 0.55f, 1.63f);
                Instantiate(deliveryFlat, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(deliveryFlat, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 3; // Minimum: 1, Maximum: 5
                speed = 8.5f; // Minimum: 5, Maximum: 20
                turnSpeed = 75; // Minimum: 50, Maximum: 200
                break;
            case 3:
                blasterLocation = new Vector3(0, 0.55f, 1.63f);
                Instantiate(fireTruck, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(fireTruck, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 5; // Minimum: 1, Maximum: 5
                speed = 7; // Minimum: 5, Maximum: 20
                turnSpeed = 65; // Minimum: 50, Maximum: 200
                break;
            case 4:
                blasterLocation = new Vector3(0, 0.55f, 1.72f);
                Instantiate(garbageTruck, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(garbageTruck, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 5; // Minimum: 1, Maximum: 5
                speed = 6.5f; // Minimum: 5, Maximum: 20
                turnSpeed = 82; // Minimum: 50, Maximum: 200
                break;
            case 5:
                blasterLocation = new Vector3(0, 0.55f, 1.41f);
                Instantiate(hatchbackSports, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(hatchbackSports, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 2; // Minimum: 1, Maximum: 5
                speed = 13; // Minimum: 5, Maximum: 20
                turnSpeed = 112; // Minimum: 50, Maximum: 200
                break;
            case 6:
                blasterLocation = new Vector3(0, 0.55f, 1.47f);
                Instantiate(police, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(police, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 2; // Minimum: 1, Maximum: 5
                speed = 12; // Minimum: 5, Maximum: 20
                turnSpeed = 120; // Minimum: 50, Maximum: 200
                break;
            case 7:
                blasterLocation = new Vector3(0, 0.55f, 0.67f);
                Instantiate(race, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(race, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 1; // Minimum: 1, Maximum: 5
                speed = 16; // Minimum: 5, Maximum: 20
                turnSpeed = 143; // Minimum: 50, Maximum: 200
                break;
            case 8:
                blasterLocation = new Vector3(0, 0.55f, 1f);
                Instantiate(raceFuture, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(raceFuture, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 1; // Minimum: 1, Maximum: 5
                speed = 18; // Minimum: 5, Maximum: 20
                turnSpeed = 121; // Minimum: 50, Maximum: 200
                break;
            case 9:
                blasterLocation = new Vector3(0, 0.55f, 1.29f);
                Instantiate(sedan, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(sedan, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 3; // Minimum: 1, Maximum: 5
                speed = 10.5f; // Minimum: 5, Maximum: 20
                turnSpeed = 94; // Minimum: 50, Maximum: 200
                break;
            case 10:
                blasterLocation = new Vector3(0, 0.55f, 1.29f);
                Instantiate(sedanSports, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(sedanSports, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 2; // Minimum: 1, Maximum: 5
                speed = 12; // Minimum: 5, Maximum: 20
                turnSpeed = 93; // Minimum: 50, Maximum: 200
                break;
            case 11:
                blasterLocation = new Vector3(0, 0.55f, 1.29f);
                Instantiate(suv, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(suv, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 3; // Minimum: 1, Maximum: 5
                speed = 10; // Minimum: 5, Maximum: 20
                turnSpeed = 100; // Minimum: 50, Maximum: 200
                break;
            case 12:
                blasterLocation = new Vector3(0, 0.55f, 1.41f);
                Instantiate(suvLuxury, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(suvLuxury, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 3; // Minimum: 1, Maximum: 5
                speed = 11; // Minimum: 5, Maximum: 20
                turnSpeed = 89; // Minimum: 50, Maximum: 200
                break;
            case 13:
                blasterLocation = new Vector3(0, 0.55f, 1.37f);
                Instantiate(taxi, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(taxi, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 2; // Minimum: 1, Maximum: 5
                speed = 12; // Minimum: 5, Maximum: 20
                turnSpeed = 78; // Minimum: 50, Maximum: 200
                break;
            case 14:
                blasterLocation = new Vector3(0, 0.55f, 1.11f);
                Instantiate(tractor, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(tractor, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 4; // Minimum: 1, Maximum: 5
                speed = 5.5f; // Minimum: 5, Maximum: 20
                turnSpeed = 90; // Minimum: 50, Maximum: 200
                break;
            case 15:
                blasterLocation = new Vector3(0, 0.55f, 1.11f);
                Instantiate(tractorPolice, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(tractorPolice, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 4; // Minimum: 1, Maximum: 5
                speed = 6; // Minimum: 5, Maximum: 20
                turnSpeed = 83; // Minimum: 50, Maximum: 200
                break;
            case 16:
                blasterLocation = new Vector3(0, 0.55f, 1.11f);
                Instantiate(tractorShovel, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(tractorShovel, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 5; // Minimum: 1, Maximum: 5
                speed = 5; // Minimum: 5, Maximum: 20
                turnSpeed = 120; // Minimum: 50, Maximum: 200
                break;
            case 17:
                blasterLocation = new Vector3(0, 0.55f, 1.47f);
                Instantiate(truck, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(truck, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 3; // Minimum: 1, Maximum: 5
                speed = 9.5f; // Minimum: 5, Maximum: 20
                turnSpeed = 102; // Minimum: 50, Maximum: 200
                break;
            case 18:
                blasterLocation = new Vector3(0, 0.55f, 1.39f);
                Instantiate(truckFlat, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(truckFlat, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 3; // Minimum: 1, Maximum: 5
                speed = 11; // Minimum: 5, Maximum: 20
                turnSpeed = 84; // Minimum: 50, Maximum: 200
                break;
            case 19:
                blasterLocation = new Vector3(0, 0.55f, 1.39f);
                Instantiate(van, vehicle.transform.position, vehicle.transform.rotation, vehicle.transform);
                Instantiate(van, vehicleSelection.transform.position, vehicleSelection.transform.rotation, vehicleSelection.transform);
                health = 4; // Minimum: 1, Maximum: 5
                speed = 9.5f; // Minimum: 5, Maximum: 20
                turnSpeed = 92; // Minimum: 50, Maximum: 200
                break;
        }
        UpdateVisualStats();
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
                bulletSpeed = 950; // Minimum: 500, Maximum: 2000
                fireRate = 3.2f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.BouncyShot;
                break;
            case 1:
                Instantiate(blasterB, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterB, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 1150; // Minimum: 500, Maximum: 2000
                fireRate = 1.8f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.BouncyShot;
                break;
            case 2:
                Instantiate(blasterC, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterC, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 1250; // Minimum: 500, Maximum: 2000
                fireRate = 1.4f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.Mine;
                break;
            case 3:
                Instantiate(blasterD, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterD, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 860; // Minimum: 500, Maximum: 2000
                fireRate = 3.6f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.BouncyShot;
                break;
            case 4:
                Instantiate(blasterE, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterE, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 1900; // Minimum: 500, Maximum: 2000
                fireRate = 0.5f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.LaserBeam;
                break;
            case 5:
                Instantiate(blasterF, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterF, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 1725; // Minimum: 500, Maximum: 2000
                fireRate = 0.8f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.LaserBeam;
                break;
            case 6:
                Instantiate(blasterG, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterG, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 1200; // Minimum: 500, Maximum: 2000
                fireRate = 1.7f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.BouncyShot;
                break;
            case 7:
                Instantiate(blasterH, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterH, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 1450; // Minimum: 500, Maximum: 2000
                fireRate = 1.2f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.LaserBeam;
                break;
            case 8:
                Instantiate(blasterI, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterI, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 1600; // Minimum: 500, Maximum: 2000
                fireRate = 1.9f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.Mine;
                break;
            case 9:
                Instantiate(blasterJ, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterJ, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 1250; // Minimum: 500, Maximum: 2000
                fireRate = 2.3f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.Mine;
                break;
            case 10:
                Instantiate(blasterK, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterK, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 1720; // Minimum: 500, Maximum: 2000
                fireRate = 0.9f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.LaserBeam;
                break;
            case 11:
                Instantiate(blasterL, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterL, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 1800; // Minimum: 500, Maximum: 2000
                fireRate = 0.8f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.BouncyShot;
                break;
            case 12:
                Instantiate(blasterM, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterM, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 1000; // Minimum: 500, Maximum: 2000
                fireRate = 2f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.Mine;
                break;
            case 13:
                Instantiate(blasterN, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterN, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 1320; // Minimum: 500, Maximum: 2000
                fireRate = 2.3f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.Mine;
                break;
            case 14:
                Instantiate(blasterO, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterO, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 760; // Minimum: 500, Maximum: 2000
                fireRate = 4f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.BouncyShot;
                break;
            case 15:
                Instantiate(blasterP, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterP, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 950; // Minimum: 500, Maximum: 2000
                fireRate = 3.7f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.Mine;
                break;
            case 16:
                Instantiate(blasterQ, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterQ, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 1950; // Minimum: 500, Maximum: 2000
                fireRate = 0.6f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.LaserBeam;
                break;
            case 17:
                Instantiate(blasterR, blaster.transform.position, blaster.transform.rotation, blaster.transform);
                Instantiate(blasterR, blasterSelection.transform.position, blasterSelection.transform.rotation, blasterSelection.transform);
                bulletSpeed = 1780; // Minimum: 500, Maximum: 2000
                fireRate = 1f; // Minimum: 0.5, Maximum: 4
                specialShot = Pawn.SpecialShotType.LaserBeam;
                break;
        }
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
