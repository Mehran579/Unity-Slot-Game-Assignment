using UnityEngine;
using System;
using System.Security.Cryptography;
public static class SlotRandomNumberGenerator
{
    public static int range(int min, int max)
    {
        if (min >= max)
        {
            throw new ArgumentOutOfRangeException(nameof(max), "the minimum should be lower than the max");
        }
        byte[] randomNumber = new byte[4];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        int value = BitConverter.ToInt32(randomNumber, 0) & int.MaxValue;
        int range = max - min;
        return min + (value % range);              // Returns a random number between the minimum and maximum values passed (the max is exclusive)
    }
}
