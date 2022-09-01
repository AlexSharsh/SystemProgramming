using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

namespace Homework1
{
    public class Unit1 : MonoBehaviour
    {
        private int frameCounter;

        private CancellationTokenSource ctsTask1;
        private CancellationTokenSource ctsTask2;

        private void Await()
        {
            frameCounter = 0;
        }

        private void Start()
        {
            MainTask();
        }

        private void Update()
        {
            frameCounter++;
        }

        private void OnDestroy()
        {
            ctsTask1.Dispose();
            ctsTask2.Dispose();
        }

        async void MainTask()
        {
            ctsTask1 = new CancellationTokenSource();
            ctsTask2 = new CancellationTokenSource();

            CancellationToken cancelTokenTask1 = ctsTask1.Token;
            CancellationToken cancelTokenTask2 = ctsTask2.Token;

            ctsTask1.Cancel();
            ctsTask2.Cancel();

            await Task.WhenAll(Task1(cancelTokenTask1), Task2(cancelTokenTask2));
        }

        async Task Task1(CancellationToken cancelToken)
        {
            await Task.Delay(1000);
            Debug.Log($"TASK 1 FINISHED");
        }

        async Task Task2(CancellationToken cancelToket)
        {
            for (int frameCounterTask = frameCounter; (frameCounterTask + 60) < frameCounter;)
            {
                if (cancelToket.IsCancellationRequested)
                    return;

                await Task.Delay(1);
            }
            Debug.Log($"TASK 2 FINISHED");
        }
    }
}
