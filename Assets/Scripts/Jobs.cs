using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Collections;

namespace Homework2
{
    public class Jobs : MonoBehaviour
    {
        NativeArray<int> intArray;
        void Start()
        {
            NativeArray<int> intArray = new NativeArray<int>(new int[] { 1, 23, 4, 5, 17, 3 }, Allocator.Persistent);
            MyJob myJob = new MyJob()
            {
                intArr = intArray,
            };

            JobHandle jobHandle = myJob.Schedule();
            jobHandle.Complete();

            Debug.Log($"----------------- IJobs OUT ARRAY:-----------------");
            for (int i = 0; i < intArray.Length; i++)
                Debug.Log(intArray[i]);
            Debug.Log($"---------------------------------------------------");

            intArray.Dispose();
        }

        private void OnDestroy()
        {
            if(intArray != null)
                intArray.Dispose();
        }

        public struct MyJob : IJob
        {
            public NativeArray<int> intArr;
            public void Execute()
            {
                for (int i = 0; i < intArr.Length; i++)
                {
                    if(intArr[i] > 10)
                        intArr[i] = 0;
                }
            }
        }
    }
}
