using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

using UnityEngine;

using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.Unity
{
    public class UnityTicker : Ticker
    {
        private IEnumerator coroutine = null;

        protected override void DisableTimer()
        {
            if (coroutine == null)
            {
                return;
            }

            CoroutineManager.Instance.StopCoroutine(coroutine);
            coroutine = null;
        }
        
        protected override void EnableTimer()
        {
            if (coroutine != null)
            {
                return;
            }

            coroutine = TimerCoroutine();
            CoroutineManager.Instance.StartCoroutine(coroutine);
        }

        private IEnumerator TimerCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.01f);
                SendSignals();
            }
        }
    }
}