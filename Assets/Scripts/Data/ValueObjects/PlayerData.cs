using System;
namespace Data.ValueObjects
{
    [Serializable]
    public struct PlayerData
    {
        public MovementData MovementData;
        public CannonData CannonData;
        public RelentlessData RelentlessData;
        public InvulnerabilityData InvulnerabilityData;
    }

    [Serializable]
    public struct MovementData
    {
        public float ForwardSpeed;
        public float SidewaysSpeed;

        public MovementData(float forwardSpeed, float sidewaysSpeed)
        {
            ForwardSpeed = forwardSpeed;
            SidewaysSpeed = sidewaysSpeed;
        }
    }

    [Serializable]
    public struct CannonData
    {
        public float CannonSpeed;
        public float CannonReloadDuration;

        public CannonData(float cannonSpeed, float cannonReloadDuration)
        {
            CannonSpeed = cannonSpeed;
            CannonReloadDuration = cannonReloadDuration;
        }
    }

    [Serializable]
    public struct RelentlessData
    {
        public float RelentlessSpeed;

        public RelentlessData(float relentlessSpeed)
        {
            RelentlessSpeed = relentlessSpeed;
        }
    }

    [Serializable]

    public struct InvulnerabilityData
    {
        public float InvulnerabilityDuration;

        public InvulnerabilityData(float ınvulnerabilityDuration)
        {
            InvulnerabilityDuration = ınvulnerabilityDuration;
        }
    }
}
