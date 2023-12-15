using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudCode;
using Unity.Services.Core;
using UnityEngine;

namespace Game
{
    public class Prueba : MonoBehaviour
    {
        async void Start()
        {
            try
            {
                await UnityServices.InitializeAsync();

                // Check that scene has not been unloaded while processing async wait to prevent throw.
                if (this == null) return;

                if (!AuthenticationService.Instance.IsSignedIn)
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                    if (this == null) return;
                }

                Debug.Log($"Player id:{AuthenticationService.Instance.PlayerId}");

                var a = await PruebaCall();
                Debug.Log(a);
                if (this == null) return;
                Debug.Log("Initialization and signin complete.");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public async Task<Dice> PruebaCall()
        {
            try
            {
                var updatedState = await CloudCodeService.Instance.CallEndpointAsync<Dice>(
                    "Chess",
                    new Dictionary<string, object>());

                return updatedState;
            }
            catch (CloudCodeException e)
            {
                Debug.LogError("Error" + e);
            }
            Debug.Log("Error");
            return new Dice(0,0);
        }
    }

    public struct Dice
    {
        public int m_numberOfSize;
        public int m_roll;

        public Dice(int mNumberOfSize, int mRoll)
        {
            m_numberOfSize = mNumberOfSize;
            m_roll = mRoll;
        }

        public override string ToString()
        {
            return $"{m_numberOfSize} {m_roll}";
        }
    }
}
