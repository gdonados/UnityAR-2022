using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Force { Friendly, Hostile, Neutral, Unknown };
public enum Label { Land, Air, Space, Activity, Installation, Submarine };
public class OverlayAssign : MonoBehaviour
{

    
    public Sprite[] overlaySheet; //Sliced sheet.

    //Friendlies-------------------------------------------------------
    public static Sprite FriendlyLandUnitFilled;
    public static Sprite FriendlyLandUnitDashed;
    public static Sprite FriendlyLandEquipmentFilled;
    public static Sprite FriendlyLandEquipmentDashed;
    public static Sprite FriendlyAirEquipmentFilled;
    public static Sprite FriendlyAirEquipmentDashed;
    public static Sprite FriendlySpaceEquipmentFilled;
    public static Sprite FriendlySpaceEquipmentDashed;
    public static Sprite FriendlyActivityFilled;
    public static Sprite FriendlyActivityDashed;
    public static Sprite FriendlyInstallationFilled;
    public static Sprite FriendlyInstallationDashed;
    public static Sprite FriendlySubmarineFilled;
    public static Sprite FriendlySubmarineDashed;

    //Hostiles----------------------------------------------------------
    public static Sprite HostileLandUnitFilled;//Hostile Land and Hostile Land Equipment share an icon
    public static Sprite HostileLandUnitDashed;
    public static Sprite HostileAirEquipmentFilled;
    public static Sprite HostileAirEquipmentDashed;
    public static Sprite HostileSpaceEquipmentFilled;
    public static Sprite HostileSpaceEquipmentDashed;
    public static Sprite HostileActivityFilled;
    public static Sprite HostileActivityDashed;
    public static Sprite HostileInstallationFilled;
    public static Sprite HostileInstallationDashed;
    public static Sprite HostileSubmarineFilled;
    public static Sprite HostileSubmarineDashed;

    //Neutrals-----------------------------------------------------------
    public static Sprite NeutralLandUnitFilled;//Neutral Land and Hostile Land Equipment share an icon
    public static Sprite NeutralAirEquipmentFilled;//No dashed version for neutral
    public static Sprite NeutralSpaceEquipmentFilled;
    public static Sprite NeutralActivityFilled;
    public static Sprite NeutralInstallationFilled;
    public static Sprite NeutralSubmarineFilled;

    //Unknowns----------------------------------------------------------
    public static Sprite UnknownLandUnitFilled;//Unknown Land and Hostile Land Equipment share an icon
    public static Sprite UnknownLandUnitDashed;
    public static Sprite UnknownAirEquipmentFilled;
    public static Sprite UnknownAirEquipmentDashed;
    public static Sprite UnknownSpaceEquipmentFilled;
    public static Sprite UnknownSpaceEquipmentDashed;
    public static Sprite UnknownActivityFilled;
    public static Sprite UnknownActivityDashed;
    public static Sprite UnknownInstallationFilled;
    public static Sprite UnknownInstallationDashed;
    public static Sprite UnknownSubmarineFilled;
    public static Sprite UnknownSubmarineDashed;






    // Start is called before the first frame update
    void Start()
    {
        FriendlyLandUnitFilled       = overlaySheet[0];
        FriendlyLandUnitDashed       = overlaySheet[1];
        FriendlyLandEquipmentFilled  = overlaySheet[2];
        FriendlyLandEquipmentDashed  = overlaySheet[3];
        FriendlyAirEquipmentFilled   = overlaySheet[4];
        FriendlyAirEquipmentDashed   = overlaySheet[5];
        FriendlySpaceEquipmentFilled = overlaySheet[6];
        FriendlySpaceEquipmentDashed = overlaySheet[7];
        FriendlyActivityFilled       = overlaySheet[8];
        FriendlyActivityDashed       = overlaySheet[9];
        FriendlyInstallationFilled   = overlaySheet[10];
        FriendlyInstallationDashed   = overlaySheet[11];
        FriendlySubmarineFilled      = overlaySheet[12];
        FriendlySubmarineFilled      = overlaySheet[13];

        HostileLandUnitFilled        = overlaySheet[14];
        HostileLandUnitDashed        = overlaySheet[15];
        HostileAirEquipmentFilled    = overlaySheet[16];
        HostileAirEquipmentDashed    = overlaySheet[17];
        HostileSpaceEquipmentFilled  = overlaySheet[18];
        HostileSpaceEquipmentDashed  = overlaySheet[19];
        HostileActivityFilled        = overlaySheet[20];
        HostileActivityDashed        = overlaySheet[21];
        HostileInstallationFilled    = overlaySheet[22];
        HostileInstallationDashed    = overlaySheet[23];
        HostileSubmarineFilled       = overlaySheet[24];
        HostileSubmarineDashed       = overlaySheet[25];

        NeutralLandUnitFilled        = overlaySheet[26];
        NeutralAirEquipmentFilled    = overlaySheet[27];
        NeutralSpaceEquipmentFilled  = overlaySheet[28];
        NeutralActivityFilled        = overlaySheet[29];
        NeutralInstallationFilled    = overlaySheet[30];
        NeutralSubmarineFilled       = overlaySheet[31];

        UnknownLandUnitFilled        = overlaySheet[32];
        UnknownLandUnitDashed        = overlaySheet[33];
        UnknownAirEquipmentFilled    = overlaySheet[34];
        UnknownAirEquipmentDashed    = overlaySheet[35];
        UnknownSpaceEquipmentFilled  = overlaySheet[36];
        UnknownSpaceEquipmentDashed  = overlaySheet[37];
        UnknownActivityFilled        = overlaySheet[38];
        UnknownActivityDashed        = overlaySheet[39];
        UnknownInstallationFilled    = overlaySheet[40];
        UnknownInstallationDashed    = overlaySheet[41];
        UnknownSubmarineFilled       = overlaySheet[42];
        UnknownSubmarineDashed       = overlaySheet[43];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Sprite GetActiveIcon(Force force, Label label, float confidence)
    {
        if(force == Force.Friendly)
        {
            if(label == Label.Land)
            {
                if(confidence >= 90f)
                {
                    return FriendlyLandUnitFilled;
                }
                else
                {
                    return FriendlyLandUnitDashed;
                }
            }
            else if(label == Label.Air)
            {
                if (confidence >= 90f)
                {
                    return FriendlyAirEquipmentFilled;
                }
                else
                {
                    return FriendlyAirEquipmentDashed;
                }
            }
            else if (label == Label.Space)
            {
                if (confidence >= 90f)
                {
                    return FriendlySpaceEquipmentFilled;
                }
                else
                {
                    return FriendlySpaceEquipmentDashed;
                }
            }
            else if (label == Label.Activity)
            {
                if (confidence >= 90f)
                {
                    return FriendlyActivityFilled;
                }
                else
                {
                    return FriendlyActivityDashed;
                }
            }
            else if (label == Label.Installation)
            {
                if (confidence >= 90f)
                {
                    return FriendlyInstallationFilled;
                }
                else
                {
                    return FriendlyInstallationDashed;
                }
            }
            else if (label == Label.Submarine)
            {
                if (confidence >= 90f)
                {
                    return FriendlySubmarineFilled;
                }
                else
                {
                    return FriendlySubmarineDashed;
                }
            }
        }
        else if(force == Force.Hostile)
        {
            if (label == Label.Land)
            {
                if (confidence >= 90f)
                {
                    return HostileLandUnitFilled;
                }
                else
                {
                    return HostileLandUnitDashed;
                }
            }
            else if (label == Label.Air)
            {
                if (confidence >= 90f)
                {
                    return HostileAirEquipmentFilled;
                }
                else
                {
                    return HostileAirEquipmentDashed;
                }
            }
            else if (label == Label.Space)
            {
                if (confidence >= 90f)
                {
                    return HostileSpaceEquipmentFilled;
                }
                else
                {
                    return HostileSpaceEquipmentDashed;
                }
            }
            else if (label == Label.Activity)
            {
                if (confidence >= 90f)
                {
                    return HostileActivityFilled;
                }
                else
                {
                    return HostileActivityDashed;
                }
            }
            else if (label == Label.Installation)
            {
                if (confidence >= 90f)
                {
                    return HostileInstallationFilled;
                }
                else
                {
                    return HostileInstallationDashed;
                }
            }
            else if (label == Label.Submarine)
            {
                if (confidence >= 90f)
                {
                    return HostileSubmarineFilled;
                }
                else
                {
                    return HostileSubmarineDashed;
                }
            }
        }
        else if (force == Force.Neutral)
        {
            if (label == Label.Land)
            {

                return NeutralLandUnitFilled;

            }
            else if (label == Label.Air)
            {

                return NeutralAirEquipmentFilled;

            }
            else if (label == Label.Space)
            {
                return NeutralSpaceEquipmentFilled;
            }
            else if (label == Label.Activity)
            {
                return NeutralActivityFilled;
            }
            else if (label == Label.Installation)
            {
                return NeutralInstallationFilled;
            }
            else if (label == Label.Submarine)
            {
                return NeutralSubmarineFilled;
            }
        }
        else if (force == Force.Unknown)
        {
            if (label == Label.Land)
            {
                if (confidence >= 90f)
                {
                    return UnknownLandUnitFilled;
                }
                else
                {
                    return UnknownLandUnitDashed;
                }
            }
            else if (label == Label.Air)
            {
                if (confidence >= 90f)
                {
                    return UnknownAirEquipmentFilled;
                }
                else
                {
                    return UnknownAirEquipmentDashed;
                }
            }
            else if (label == Label.Space)
            {
                if (confidence >= 90f)
                {
                    return UnknownSpaceEquipmentFilled;
                }
                else
                {
                    return UnknownSpaceEquipmentDashed;
                }
            }
            else if (label == Label.Activity)
            {
                if (confidence >= 90f)
                {
                    return UnknownActivityFilled;
                }
                else
                {
                    return UnknownActivityDashed;
                }
            }
            else if (label == Label.Installation)
            {
                if (confidence >= 90f)
                {
                    return UnknownInstallationFilled;
                }
                else
                {
                    return UnknownInstallationDashed;
                }
            }
            else if (label == Label.Submarine)
            {
                if (confidence >= 90f)
                {
                    return UnknownSubmarineFilled;
                }
                else
                {
                    return UnknownSubmarineDashed;
                }
            }
        }
        return UnknownAirEquipmentDashed;
    }
}
